using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Scripts.Utils;
using Unity.VisualScripting.Dependencies.NCalc;

namespace Core.Scripts.SceneManagement
{
    /// <summary>
    /// Singleton helping managing scene transitions,
    /// including loading, unloading, and transitioning between scenes.
    /// </summary>
    public sealed class TransitionSceneManager : Singleton<TransitionSceneManager>
    {
        #region Fields and properties

        [Header("Configuration")]
        
        [SerializeField]
        [Tooltip("Scene that will be left loaded when transitioning")] 
        private string _coreSceneName = "Core";
        [SerializeField]
        [Tooltip("Scene that will be used as a loading screen during transitions")]
        private string _loadingSceneName = "LoadingScreen";
        private Scene _loadingScene;
        [SerializeField] 
        [Tooltip("Minimum time to show loading screen, even if scene loads faster")]
        private float _minimumLoadTime = 1f;

        /// <summary>
        /// Currently loaded scenes.
        /// </summary>
        private readonly List<Scene> _loadedScenes = new ();
        private bool _coreSceneLoaded;
        private ILoadingScreen _loadingScreen;

        #endregion

        #region Init
        
        private void Start()
        {
            _loadingScene = NameToScene(_loadingSceneName);
            // Register loaded event to make sure we know all loaded scenes
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            if (_loadedScenes.Contains(scene) && CanBeUnloaded(scene))
            {
                _loadedScenes.Remove(scene);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (!_loadedScenes.Contains(scene) && CanBeUnloaded(scene))
            {
                _loadedScenes.Add(scene);
            }
        }
        
        private void OnDestroy()
        {
            // Unregister events to prevent memory leaks
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        #endregion

        #region Public API

        /// <summary> Public API: Load scene additively </summary>
        public Coroutine LoadSceneAdditive(string sceneName) => 
            StartCoroutine(LoadSceneRoutine(NameToScene(sceneName)));

        /// <summary> Public API: Load scene additively </summary>
        public Coroutine LoadSceneAdditive(int sceneNumber) => 
            StartCoroutine(LoadSceneRoutine(NumberToScene(sceneNumber)));

        /// <summary> Public API: Unload scene </summary>
        public Coroutine UnloadScene(string sceneName) => 
            StartCoroutine(UnloadSceneRoutine(NameToScene(sceneName)));

        /// <summary> Public API: Unload scene </summary>
        public void UnloadScene(int sceneNumber) 
        {
            StartCoroutine(UnloadSceneRoutine(NumberToScene(sceneNumber)));
        }
    
        /// <summary> Switch active scenes with transition </summary>
        public void TransitionToScene(string sceneName)
        {
            StartCoroutine(TransitionRoutine(NameToScene(sceneName)));
        }
    
        public void TransitionToScene(int sceneNumber)
        {
            StartCoroutine(TransitionRoutine(NumberToScene(sceneNumber)));
        }

        #endregion

        #region Internal helpers
        
        private bool CanBeUnloaded(Scene scene)
        {
            return !string.Equals(scene.name, _coreSceneName, StringComparison.OrdinalIgnoreCase) &&
                   !string.Equals(scene.name, _loadingSceneName, StringComparison.OrdinalIgnoreCase);
        }
        
        private Scene NameToScene(string sceneName) => SceneManager.GetSceneByName(sceneName);
        
        private Scene NumberToScene(int sceneNumber)
        {
            if (sceneNumber < 0 || sceneNumber >= SceneManager.sceneCountInBuildSettings)
                throw new ArgumentOutOfRangeException(nameof(sceneNumber), "Scene number is out of range.");
            return SceneManager.GetSceneByBuildIndex(sceneNumber);
        }

        private IEnumerator LoadSceneRoutine(Scene scene)
        {
            if (scene.isLoaded)
                yield break;

            // Show loading screen here
            yield return StartCoroutine(ShowLoadingScreen());

            yield return LoadSceneInternal(scene);

            // Hide loading screen here
            yield return HideLoadingScreen();
        }
        
        private IEnumerator UnloadSceneRoutine(Scene scene)
        {
            if (!scene.isLoaded || !CanBeUnloaded(scene)) 
                yield break;
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(scene);
            while (!unloadOp.isDone) 
                yield return null;
        
            _loadedScenes.Remove(scene);
        }
    
        private IEnumerator TransitionRoutine(Scene scene)
        {
            // Show loading screen
            yield return StartCoroutine(ShowLoadingScreen());
        
            // Unload all non-core scenes
            foreach (Scene toUnload in new List<Scene>(_loadedScenes))
            {
                if (CanBeUnloaded(toUnload))
                {
                    yield return UnloadSceneRoutine(scene);
                }
            }
        
            // Load new scene
            yield return LoadSceneRoutine(scene);
        
            // Hide loading screen
            yield return HideLoadingScreen();
        }
        
        private IEnumerator LoadSceneInternal(Scene scene, bool skipMinLoadingTime = false)
        {
            // Early out if already loaded
            if (scene.isLoaded) 
                yield break;

            float startTime = Time.realtimeSinceStartup;
            
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
            loadOp.allowSceneActivation = false;
            
            // Feedback load progress
            while (loadOp.progress < 0.9f || !IsMinLoadingOver())
            {
                if (_loadingScreen is not null)
                {
                    _loadingScreen.Progress = loadOp.progress;
                }
                yield return null;
            }
        
            // Activate the scene
            loadOp.allowSceneActivation = true;
        
            while (!loadOp.isDone) 
                yield return null;
        
            if (!_loadedScenes.Contains(scene))
                _loadedScenes.Add(scene);

            #region Local methods

            bool IsMinLoadingOver()
            {
                return skipMinLoadingTime || Time.realtimeSinceStartup - startTime >= _minimumLoadTime;
            }

            #endregion
        }

        // Implement these with your UI system
        private IEnumerator ShowLoadingScreen()
        {
            // Load transition scene if not already loaded
            yield return LoadSceneInternal(_loadingScene, true);
            // Find and initialize loading screen
            if (_loadingScreen == null)
            {
                foreach (var rootGameObject in _loadingScene.GetRootGameObjects())
                {
                    var screen = rootGameObject.GetComponentInChildren<ILoadingScreen>();
                    if (screen != null)
                    {
                        _loadingScreen = screen;
                        break;
                    }
                }
                if (_loadingScreen == null)
                {
                    Debug.LogError($"No loading screen found in the scene {_loadingSceneName}.");
                    yield break;
                }
            }
            
            // Show loading screen
            _loadingScreen.StartLoadingScreen();
        }
    
        private IEnumerator HideLoadingScreen()
        {
            if (_loadingScreen is not null)
                _loadingScreen.StopLoadingScreen();
            yield return UnloadSceneRoutine(_loadingScene);
        }

        #endregion
    }
}