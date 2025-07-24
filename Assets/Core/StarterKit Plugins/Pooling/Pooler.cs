using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Utils
{
    public class Pooler : MonoBehaviour
    {
        private readonly Stack<GameObject> _pool = new();

        [field: SerializeField] 
        public string Id { get; set; }
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private int _initialSize;
        [SerializeField]
        private int _maxSize;
        public int Count { get; private set; }

        public void Start()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                AddNew();
            }
        }
        private GameObject AddNew()
        {
            if( _maxSize<0 || Count < _maxSize)
            {
                Count++;
            }
            else
            {
                Debug.LogWarning($"[PoolInstance] Maximum size reached for pool of {_prefab.name}. Cannot add more objects.");
                return null;
            }
            
            var go = Instantiate(_prefab, transform);
            go.SetActive(false);
            _pool.Push(go);
            return go;
        }

        public GameObject Get(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject obj = _pool.Count > 0 ? _pool.Pop() : AddNew();
            obj.transform.SetPositionAndRotation(position, rotation);
            
            obj.transform.SetParent(parent, true);
            
            obj.SetActive(true);

            foreach (var poolable in obj.GetComponentsInChildren<IPoolable>(true))
            {
                poolable.OnSpawned();
            }

            return obj;
        }

        public void Return(GameObject obj)
        {
            if (!obj) 
                return;

            foreach (var poolable in obj.GetComponentsInChildren<IPoolable>(true))
            {
                poolable.OnDespawned();
            }

            obj.transform.SetParent(transform);
            obj.SetActive(false);
            _pool.Push(obj);
        }
        
        public void Clear()
        {
            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                if (obj)
                    DestroyImmediate(obj);
            }
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}