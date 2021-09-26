using System;
using System.Linq;
using System.Collections.Generic;

using MainGame.Units;

namespace MainGame.Turns
{
    public class TurnsSystem
    {
        public event Action OnMoveNext;
        public event Action<UnitData> OnAdded;
        public event Action<UnitData> OnRemove;

        private List<UnitData> _availableTurns;
        private List<UnitData> _generalQueue;
        private List<UnitData> _turnQueue;

        private int _maxTurns;
        private int _turnNumber;

        public UnitData CurrentTurn => _generalQueue.FirstOrDefault();

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

            OnMoveNext?.Invoke();

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

            OnRemove?.Invoke(unit);

            FillQueue();
        }

        public void FillQueue()
        {
            if (_availableTurns.Count == 0) return;

            while (_generalQueue.Count < _maxTurns)
            {
                if (_turnQueue.Count == 0)
                {
                    _turnNumber++;
                    _turnQueue = new List<UnitData>(Sort(_availableTurns.ToArray(), GetPriority()));
                }

                var turn = _turnQueue.First();
                _turnQueue.RemoveAt(0);

                _generalQueue.Add(turn);

                OnAdded?.Invoke(turn);
            }
        }

        protected virtual ArmyType GetPriority()
        {
            return _turnNumber % 2 == 0 ? ArmyType.Red : ArmyType.Blue;
        }

        private UnitData[] Sort(UnitData[] turns, ArmyType priority)
        {
            return turns.OrderBy(x => x, new UnitsComparer(priority)).ToArray();
        }
    }
}