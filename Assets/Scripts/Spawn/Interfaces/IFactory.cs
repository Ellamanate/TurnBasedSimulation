namespace Modules.Spawn
{
    public interface IFactory<T1, T2>
    {
        T1 Create(T2 obj);
    }
}
