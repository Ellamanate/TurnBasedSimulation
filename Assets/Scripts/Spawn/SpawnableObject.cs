using System;

using UnityEngine;

namespace Modules.Spawn
{
    public class SpawnableObject : MonoBehaviour, ISpawnable
    {
        public event Action<ISpawnable> OnDespawn;
        public event Action<ISpawnable> OnDispose;

        public void Despawn()
        {
            WhenDespawn();

            OnDespawn?.Invoke(this);
        }

        public virtual void Dispose()
        {
            OnDispose?.Invoke(this);

            Destroy(gameObject);
        }

        protected virtual void WhenDespawn() { }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}