using System;
using System.Collections.Generic;
using System.Linq;

namespace RunFoxyRun
{
    public sealed class ObjectPool
    {
        private static ObjectPool _pool = new ObjectPool();

        public static ObjectPool Pool => _pool;

        private IDictionary<string, IList<object>> _objectPool;

        private IDictionary<string, Func<object>> _objectFactoryPool;

        public ObjectPool()
        {
            _objectPool = new Dictionary<string, IList<object>>();
            _objectFactoryPool = new Dictionary<string, Func<object>>();
        }

        public void RegisterCollection(string name, Func<object> create)
        {
            _objectFactoryPool.Add(name, create);
            _objectPool.Add(name, new List<object>());
        }

        public object Retriev(string collection)
        {
            var obj = _objectPool[collection].FirstOrDefault();

            if (obj != null)
            {
                _objectPool[collection].Remove(obj);
            }

            return obj ?? _objectFactoryPool[collection]();
        }

        public void ReleaseObject(string collection, object obj)
        {
            _objectPool[collection].Add(obj);
        }
    }
}
