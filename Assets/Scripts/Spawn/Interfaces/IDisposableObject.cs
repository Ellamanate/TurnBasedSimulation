using System;

namespace Modules.Spawn
{
    public interface IDisposableObject<T> : IDisposable
    {
        event Action<T> OnDispose;
    }
}
