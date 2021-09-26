using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using MainGame.Units;

using Modules.Spawn;

namespace MainGame.Turns.Visualization
{
    public class TurnsVisualization : MonoBehaviour
    {
        [SerializeField]
        private TurnUI _turnPrefab;

        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private Transform _pool;

        private TurnsSystem _turnsSystem;
        private DefaultPooler<TurnUI, UnitData> _pooler;

        private List<TurnUI> _turns = new List<TurnUI>();

        private void Awake()
        {
            _pooler = new DefaultPooler<TurnUI, UnitData>(new TurnUIFactory(_parent, _turnPrefab), _parent, _pool);
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        public void SetTurnsSystem(TurnsSystem turnsSystem)
        {
            Unsubscribe();

            _turnsSystem = turnsSystem;

            Subscribe();
        }

        private void Subscribe()
        {
            if (_turnsSystem != null)
            {
                _turnsSystem.OnMoveNext += MoveNext;
                _turnsSystem.OnAdded += AddTurn;
                _turnsSystem.OnRemove += RemoveTurn;
            }
        }

        private void Unsubscribe()
        {
            if (_turnsSystem != null)
            {
                _turnsSystem.OnMoveNext -= MoveNext;
                _turnsSystem.OnAdded -= AddTurn;
                _turnsSystem.OnRemove -= RemoveTurn;
            }
        }

        private void MoveNext()
        {
            var turn = _turns.FirstOrDefault(x => x.Data == _turnsSystem.CurrentTurn);

            if (turn != null)
            {
                _turns.Remove(turn);
                _turns.Add(turn);

                turn.Despawn();
            }
        }

        private void AddTurn(UnitData data)
        {
            var turn = _pooler.Get(data);

            if (!_turns.Contains(turn))
            {
                _turns.Add(turn);
            }
        }

        private void RemoveTurn(UnitData data)
        {
            foreach (var turn in _turns.Where(x => x.Data == data))
            {
                turn.Despawn();
            }
        }
    }
}