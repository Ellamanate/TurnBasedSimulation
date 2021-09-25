using System.Linq;
using System.Collections.Generic;

namespace Modules.Spawn
{
    public abstract class BasePooler<T1, T2> : IPooler<T1, T2> where T1 : ISpawnable
    {
        protected readonly List<ISpawnable> ReturnedObjects = new List<ISpawnable>();
        protected readonly List<Identifier<ISpawnable, T2>> Identifires = new List<Identifier<ISpawnable, T2>>();

        public T1 Get(T2 id)
        {
            var validIdentifier = Identifires.Where(x => x.Id.Equals(id)).ToArray();

            if (validIdentifier.Length == 0)
            {
                return CreateNewObject(id);
            }

            var validObject = (T1)ReturnedObjects.Intersect(validIdentifier.Select(x => x.Object)).FirstOrDefault();

            if (validObject != null)
            {
                ReturnedObjects.Remove(validObject);

                OnGetFromPool(validObject);

                return validObject;
            }

            return CreateNewObject(id);
        }

        public void ReturnAll()
        {
            foreach (var obj in Identifires.Select(x => x.Object))
            {
                ReturnToPool(obj);
            }
        }

        public void ReturnToDefault()
        {
            ReturnedObjects.Clear();
            Identifires.Clear();
        }

        public void Dispose()
        {
            foreach (var identifier in Identifires)
            {
                identifier.Object.Dispose();
            }

            ReturnToDefault();
        }

        protected abstract T1 Create(T2 id);

        protected void ReturnToPool(ISpawnable obj)
        {
            if (!ReturnedObjects.Contains(obj) && Identifires.Select(x => x.Object).Contains(obj))
            {
                ReturnedObjects.Add(obj);

                OnReturnToPool((T1)obj);
            }
        }

        protected virtual void OnGetFromPool(T1 obj) { }
        protected virtual void OnReturnToPool(T1 obj) { }

        private T1 CreateNewObject(T2 id)
        {
            var obj = Create(id);

            Identifires.Add(new Identifier<ISpawnable, T2>(obj, id));

            return obj;
        }
    }
}