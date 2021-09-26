using UnityEngine;
using UnityEngine.UI;

namespace MainGame.Turns.Visualization
{
    public class EndTurnUI : MonoBehaviour
    {
        [SerializeField]
        private Text _turnNumber;

        public void SetNumber(int number)
        {
            _turnNumber.text = string.Format("Раунд {0}", number);
        }
    }
}