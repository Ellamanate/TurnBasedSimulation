namespace Modules.Spawn
{
    public struct Identifier<T1, T2>
    {
        private T1 _object;
        private T2 _id;

        public T1 Object => _object;
        public T2 Id => _id;

        public Identifier(T1 obj, T2 id)
        {
            _object = obj;
            _id = id;
        }
    }
}
