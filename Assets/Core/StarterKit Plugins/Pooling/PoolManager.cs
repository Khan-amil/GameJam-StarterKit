using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Utils
{
    public class PoolManager : Singleton<PoolManager>
    {
        private readonly Dictionary<string, Pooler> _pools = new();
        private readonly Dictionary<GameObject, Pooler> _spawnedObjects = new();
        
        public void RegisterPool(Pooler pooler)
        {
            if (!_pools.TryAdd(pooler.Id, pooler))
            {
                Debug.LogWarning($"[PoolManager] Pool with the same ID already exists: {pooler.Id}");
            }
        }

        public void UnregisterPool(string id)
        {
            if (_pools.TryGetValue(id, out var pool))
            {
                pool.Clear();
                _pools.Remove(id);
            }
            else
            {
                Debug.LogWarning($"[PoolManager] Pool '{id}' not found.");
            }
        }
        
        public GameObject Spawn(string id, Vector3 pos, Quaternion rot, Transform parent = null)
        {
            if (_pools.TryGetValue(id, out var pool))
            {
                var spawned = pool.Get(pos, rot, parent);
                if (spawned)
                {
                    _spawnedObjects[spawned] = pool;
                }
                else
                {
                    Debug.LogWarning($"[PoolManager] Failed to spawn object from pool '{id}'. Pool may be empty or max size reached.");
                }
                return spawned;
            }

            Debug.LogWarning($"[PoolManager] Pool '{id}' not found, you need to register it first.");
            return null;
        }

        public void Despawn(GameObject obj)
        {
            if (_spawnedObjects.TryGetValue(obj, out var pool))
                pool.Return(obj);
            else
            {
                Debug.LogWarning($"[PoolManager] Pool for '{obj}' not found, destroying.");
                Destroy(obj);
            }
        }
        
        public void ClearAllPools()
        {
            foreach (var poolId in _pools.Keys)
            {
                UnregisterPool(poolId);
            }
            _pools.Clear();
        }
    }
}
