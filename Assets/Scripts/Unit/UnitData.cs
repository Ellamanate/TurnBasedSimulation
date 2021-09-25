using UnityEngine;

namespace MainGame.Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField]
        private int _initiative;

        [SerializeField]
        private int _speed;

        public int Initiative => _initiative;
        public int Speed => _speed;
    }
}