using UnityEngine;
using UnityEngine.UI;

using Modules.Spawn;

using MainGame.Units;

namespace MainGame.Turns.Visualization
{
    public class TurnUI : SpawnableObject
    {
        [SerializeField]
        private Image _background;

        [SerializeField]
        private Text _name;

        [SerializeField]
        private Text _initiative;        
        
        [SerializeField]
        private Text _speed;

        public UnitData Data { get; private set; }

        public void SetData(UnitData data)
        {
            Data = data;

            _background.color = data.Color;
            _name.text = data.Name;
            _initiative.text = data.Initiative.ToString();
            _speed.text = data.Speed.ToString();
        }
    }
}