using UnityEngine;

namespace Core.Scripts.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _applicationIsQuitting;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton<{typeof(T)}>] Instance already destroyed. Returning null.");
                    return null;
                }

                if (!_instance)
                {
                    Debug.LogError($"[Singleton<{typeof(T)}>] No instance found in scene.");
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"[Singleton<{typeof(T)}>] Duplicate detected, destroying object: {gameObject.name}");
                DestroyImmediate(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }

}