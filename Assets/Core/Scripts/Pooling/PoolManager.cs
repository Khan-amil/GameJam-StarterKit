using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Utils
{
    public class PoolManager : Singleton<PoolManager>
    {
        private readonly Dictionary<string, Pooler> _pools = new();
        
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
        
        public GameObject Spawn(string id, Vector3 pos, Quaternion rot)
        {
            if (_pools.TryGetValue(id, out var pool))
                return pool.Get(pos, rot);

            Debug.LogWarning($"[PoolManager] Pool '{id}' not found, you need to register it first.");
            return null;
        }

        public void Despawn(string id, GameObject obj)
        {
            if (_pools.TryGetValue(id, out var pool))
                pool.Return(obj);
            else
                Destroy(obj);
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
