using UnityEngine;

using Modules.Spawn;

using MainGame.Units;

namespace MainGame.Turns.Visualization
{
    public class TurnUIFactory : IFactory<TurnUI, UnitData>
    {
        private TurnUI _prefab;
        private Transform _parent;

        public TurnUIFactory(Transform parent, TurnUI prefab)
        {
            _parent = parent;
            _prefab = prefab;
        }

        public TurnUI Create(UnitData obj)
        {
            var turn = Object.Instantiate(_prefab, _parent);
            turn.SetData(obj);

            return turn;
        }
    }
}
