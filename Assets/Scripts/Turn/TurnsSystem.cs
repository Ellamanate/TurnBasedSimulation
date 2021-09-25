using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using MainGame.Units;

namespace MainGame.Turns
{
    public class TurnsSystem : MonoBehaviour
    {
        public event Action OnMoveNext;
        public event Action<UnitData> OnAdded;
        public event Action<UnitData> OnRemove;

        [SerializeField] 
        private UnitData[] _unitTurns;

        [SerializeField]
        private int _maxTurns;

        private List<UnitData> _availableTurns = new List<UnitData>();
        private List<UnitData> _generalQueue = new List<UnitData>();

        private int _turnNumber;
        private int _currentTurnIndex;

        public UnitData CurrentTurn => _generalQueue.FirstOrDefault();

        private void Awake()
        {
            _availableTurns.AddRange(Sort(_unitTurns, _turnNumber));

            FillQueue();
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

            _currentTurnIndex = _currentTurnIndex != 0 ? _currentTurnIndex - 1 : _currentTurnIndex;

            _availableTurns.Remove(unit);
            _generalQueue.RemoveAll(x => x == unit);

            OnRemove?.Invoke(unit);

            FillQueue();
        }

        private void FillQueue()
        {
            if (_availableTurns.Count == 0) return;

            while (_generalQueue.Count < _maxTurns)
            {
                var turn = _availableTurns[_currentTurnIndex];

                _generalQueue.Add(turn);

                if (_currentTurnIndex >= _availableTurns.Count - 1)
                {
                    _turnNumber++;
                    _currentTurnIndex = 0;
                    _availableTurns = new List<UnitData>(Sort(_availableTurns.ToArray(), _turnNumber));
                }
                else
                {
                    _currentTurnIndex++;
                }

                OnAdded?.Invoke(turn);
            }
        }

        protected virtual UnitData[] Sort(UnitData[] turns, int turnNumber)
        {
            return turns;
        }
    }
}