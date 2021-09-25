using System;

namespace Modules.Spawn
{
    public interface ISpawnable : IDisposableObject<ISpawnable>
    {
        event Action<ISpawnable> OnDespawn;
        void Despawn();
    }
}
