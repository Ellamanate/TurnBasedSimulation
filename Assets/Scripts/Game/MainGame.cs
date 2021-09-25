using UnityEngine;
using UnityEngine.UI;

using MainGame.Turns;

namespace MainGame
{
    public class MainGame : MonoBehaviour
    {
        [SerializeField]
        private Button _nextTurn;

        [SerializeField]
        private Button _killNextUnit;

        [SerializeField]
        private TurnsSystem _turnsSystem;

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