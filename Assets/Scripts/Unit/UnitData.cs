using UnityEngine;

namespace MainGame.Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField]
        private ArmyType _armyType;

        [SerializeField]
        private Color _color;

        [SerializeField]
        private string _name;

        [SerializeField]
        private int _armyIndex;

        [SerializeField]
        private int _initiative;

        [SerializeField]
        private int _speed;

        public ArmyType ArmyType => _armyType;
        public Color Color => _color;
        public string Name => _name;
        public int ArmyIndex => _armyIndex;
        public int Initiative => _initiative;
        public int Speed => _speed;
    }
}