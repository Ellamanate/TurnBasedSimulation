using UnityEngine;
using UnityEngine.UI;

using MainGame.Turns;
using MainGame.Units;
using MainGame.Turns.Visualization;

namespace MainGame
{
    public class MainGame : MonoBehaviour
    {
        [SerializeField]
        private UnitData[] _unitTurns;

        [SerializeField]
        private Button _nextTurn;

        [SerializeField]
        private Button _killNextUnit;

        [SerializeField]
        private TurnsVisualization _visualization;

        [SerializeField]
        private int _maxTurns;

        private TurnsSystem _turnsSystem;

        private void Awake()
        {
            _turnsSystem = new TurnsSystem(_unitTurns, _maxTurns);
        }

        private void Start()
        {
            _visualization.SetTurnsSystem(_turnsSystem);
            _turnsSystem.FillQueue();
        }

        private void OnEnable()
        {
            _nextTurn.onClick.AddListener(_turnsSystem.GetNext);
            _killNextUnit.onClick.AddListener(RemoveCurrent);
        }

        private void OnDisable()
        {
            _nextTurn.onClick.RemoveListener(_turnsSystem.GetNext);
            _killNextUnit.onClick.RemoveListener(RemoveCurrent);
        }

        private void RemoveCurrent()
        {
            _turnsSystem.Remove(1);
        }
    }
}