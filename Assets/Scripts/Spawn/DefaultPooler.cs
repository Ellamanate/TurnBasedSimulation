using UnityEngine;

namespace Modules.Spawn
{
    public class DefaultPooler<T1, T2> : BasePooler<T1, T2> where T1 : SpawnableObject
    {
        private IFactory<T1, T2> _factory;

        private Transform _parent;
        private Transform _pool;

        public DefaultPooler(IFactory<T1, T2> factory, Transform parent, Transform pool)
        {
            _factory = factory;
            _parent = parent;
            _pool = pool;
        }

        protected override T1 Create(T2 id)
        {
            var obj = _factory.Create(id);

            obj.OnDespawn += ReturnToPool;
            obj.OnDispose += DisposeObject;

            return obj;
        }

        protected override void OnGetFromPool(T1 obj)
        {
            obj.transform.SetParent(_parent);
        }

        protected override void OnReturnToPool(T1 obj)
        {
            obj.transform.SetParent(_pool);
        }

        private void DisposeObject(ISpawnable obj)
        {
            ReturnedObjects.Remove(obj);
            Identifires.RemoveAll(x => x.Object.Equals(obj));

            obj.OnDespawn -= ReturnToPool;
            obj.OnDispose -= DisposeObject;
        }
    }
}
