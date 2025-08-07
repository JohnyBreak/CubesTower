using System;
using System.Collections.Generic;
using Cubes;
using Object = UnityEngine.Object;

namespace Pool
{
    public class CubePool
    {
        private Cube _objectPrefab;
        private List<Cube> _pooledObjects = new();

        public void SetPrefab(Cube prefab)
        {
            _objectPrefab = prefab;
        }

        public Cube GetPooledObject(bool isActive = false)
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                var tempT = _pooledObjects[i];
                if (tempT == null) continue;

                if (!_pooledObjects[i].gameObject.activeInHierarchy)
                {
                    _pooledObjects[i].transform.SetParent(null);
                    _pooledObjects[i].gameObject.SetActive(isActive);
                    return _pooledObjects[i];
                }
            }

            Cube o = CreateNewObject();
            o.gameObject.transform.parent = null;
            o.gameObject.SetActive(isActive);
            return o;
        }

        protected Cube CreateNewObject()
        {
            if (_objectPrefab == null)
            {
                throw new NullReferenceException("_objectPrefab");
            }

            Cube obj = Object.Instantiate(_objectPrefab);

            BackObjectToPool(obj);
            _pooledObjects.Add(obj);
            return obj;
        }


        public void BackObjectToPool(Cube obj)
        {
            if (obj == null) return;

            if (obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(false);
            }
        }

        public void Dispose()
        {
            foreach (var obj in _pooledObjects)
            {
                Object.Destroy(obj);
            }
        }
    }
}