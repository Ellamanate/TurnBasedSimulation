using System.Linq;
using System.Collections.Generic;

using MainGame.Units;

namespace MainGame.Turns
{
    public class TurnsSystem
    {
        private List<UnitData> _availableTurns;
        private List<UnitData> _generalQueue;
        private List<UnitData> _turnQueue;

        private int _maxTurns;

        public UnitData CurrentTurn => _generalQueue.FirstOrDefault();

        protected int TurnNumber { get; private set; }

        public TurnsSystem(UnitData[] unitTurns, int maxTurns)
        {
            _maxTurns = maxTurns;

            _availableTurns = new List<UnitData>(Sort(unitTurns, GetPriority()));
            _turnQueue = new List<UnitData>(_availableTurns);
            _generalQueue = new List<UnitData>();
        }

        public void GetNext()
        {
            if (_generalQueue.Count == 0) return;

            OnMoveNext();

            _generalQueue.RemoveAt(0);

            FillQueue();
        }

        public void Remove(int index)
        {
            if (_generalQueue.Count == 0 || index >= _generalQueue.Count) return;

            var unit = _generalQueue[index];

            _turnQueue.Remove(unit);
            _availableTurns.Remove(unit);
            _generalQueue.RemoveAll(x => x == unit);

            OnRemove(unit);

            FillQueue();
        }

        public void FillQueue()
        {
            if (_availableTurns.Count == 0) return;

            while (_generalQueue.Count < _maxTurns)
            {
                if (_turnQueue.Count == 0)
                {
                    TurnNumber++;
                    _turnQueue = new List<UnitData>(Sort(_availableTurns.ToArray(), GetPriority()));

                    OnNextTurn();
                }

                var turn = _turnQueue.First();
                _turnQueue.RemoveAt(0);

                _generalQueue.Add(turn);

                OnAdding(turn);
            }
        }

        protected virtual ArmyType GetPriority()
        {
            return TurnNumber % 2 == 0 ? ArmyType.Red : ArmyType.Blue;
        }

        protected virtual void OnMoveNext() { }
        protected virtual void OnNextTurn() { }
        protected virtual void OnAdding(UnitData data) { }
        protected virtual void OnRemove(UnitData data) { }

        private UnitData[] Sort(UnitData[] turns, ArmyType priority)
        {
            return turns.OrderBy(x => x, new UnitsComparer(priority)).ToArray();
        }
    }
}