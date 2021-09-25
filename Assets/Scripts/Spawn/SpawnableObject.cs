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
            OnDespawn?.Invoke(this);

            WhenDespawn();
        }

        public virtual void Dispose()
        {
            OnDispose?.Invoke(this);

            Destroy(gameObject);
        }

        protected virtual void WhenDespawn() { }
    }
}