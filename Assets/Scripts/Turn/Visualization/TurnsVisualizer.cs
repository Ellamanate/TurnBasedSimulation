using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using MainGame.Units;

using Modules.Spawn;

namespace MainGame.Turns.Visualization
{
    public class TurnsVisualizer : MonoBehaviour, ITurnsVisualizing
    {
        [SerializeField]
        private TurnUI _turnPrefab;

        [SerializeField]
        private EndTurnUI _endTurnPrefab;

        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private Transform _pool;

        private DefaultPooler<TurnUI, UnitData> _pooler;
        private List<TurnUI> _turns = new List<TurnUI>();
        private List<Transform> _endTurnUIs = new List<Transform>();

        private void Awake()
        {
            _pooler = new DefaultPooler<TurnUI, UnitData>(new TurnUIFactory(_parent, _turnPrefab), _parent, _pool);
        }

        public void AddTurnEnd(int nextTurnNumber)
        {
            var endTurnUI = Instantiate(_endTurnPrefab, _parent);
            endTurnUI.SetNumber(nextTurnNumber);

            _endTurnUIs.Add(endTurnUI.transform);
        }

        public void MoveNext(UnitData currentData)
        {
            var turn = _turns.FirstOrDefault(x => x.Data == currentData);

            if (turn != null)
            {
                _turns.Remove(turn);
                _turns.Add(turn);

                turn.Despawn();
            }

            TryDeleteEndTurn();
        }

        public void AddTurn(UnitData data)
        {
            var turn = _pooler.Get(data);

            if (!_turns.Contains(turn))
            {
                _turns.Add(turn);
            }
        }

        public void RemoveTurn(UnitData data)
        {
            foreach (var turn in _turns.Where(x => x.Data == data))
            {
                turn.Despawn();
            }

            TryDeleteEndTurn();
        }

        private void TryDeleteEndTurn()
        {
            var firstChild = _parent.GetChild(0);

            if (_endTurnUIs.Contains(firstChild))
            {
                Destroy(firstChild.gameObject);
            }
        }
    }
}