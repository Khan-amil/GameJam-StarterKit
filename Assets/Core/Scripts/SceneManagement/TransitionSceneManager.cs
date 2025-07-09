using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Scripts.Utils;

namespace Core.Scripts.SceneManagement
{
    public class TransitionSceneManager : Singleton<TransitionSceneManager>
    {
        [Header("Configuration")]
        [SerializeField] private string _coreSceneName = "Core";
        [SerializeField] private string _loadingSceneName = "LoadingScreen";
        [SerializeField] private float _minimumLoadTime = 1f; // For loading screen

        private readonly List<string> _loadedScenes = new ();
        private bool _coreSceneLoaded;
        
        /// <summary> Public API: Load scene additively </summary>
        public void LoadScene(string sceneName) 
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }
    
        /// <summary> Public API: Load scene additively </summary>
        public void LoadScene(int sceneNumber) 
        {
            StartCoroutine(LoadSceneRoutine(sceneNumber));
        }

        /// <summary> Public API: Unload scene </summary>
        public void UnloadScene(string sceneName) 
        {
            StartCoroutine(UnloadSceneRoutine(sceneName));
        }
    
        /// <summary> Public API: Unload scene </summary>
        public void UnloadScene(int sceneNumber) 
        {
            StartCoroutine(UnloadSceneRoutine(sceneNumber));
        }
    
        /// <summary> Switch active scenes with transition </summary>
        public void TransitionToScene(string newSceneName)
        {
            StartCoroutine(TransitionRoutine(newSceneName));
        }
    
        public void TransitionToScene(int newSceneNumber)
        {
            StartCoroutine(TransitionRoutine(newSceneNumber));
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            if (SceneIsLoaded(sceneName)) yield break;

            // Show loading screen here
            yield return StartCoroutine(ShowLoadingScreen());

            yield return LoadSceneInternal(sceneName);

            // Hide loading screen here
            yield return HideLoadingScreen();
        }

        private IEnumerator LoadSceneRoutine(int sceneNumber)
        {
            if (SceneIsLoaded(sceneNumber)) yield break;

            // Show loading screen here
            yield return StartCoroutine(ShowLoadingScreen());

            yield return LoadSceneInternal(sceneNumber);

            // Hide loading screen here
            yield return HideLoadingScreen();
        }

    
        private IEnumerator UnloadSceneRoutine(string sceneName)
        {
            if (!SceneIsLoaded(sceneName) 
                || string.Equals(sceneName, _coreSceneName, StringComparison.OrdinalIgnoreCase)) 
                yield break;

            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);
            while (!unloadOp.isDone) 
                yield return null;
        
            _loadedScenes.Remove(sceneName);
        }
    
        private IEnumerator UnloadSceneRoutine(int sceneNumber)
        {
            if (!SceneIsLoaded(sceneNumber) 
                || string.Equals(SceneToString(sceneNumber), _coreSceneName, StringComparison.OrdinalIgnoreCase))
                yield break;

            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneNumber);
            while (!unloadOp.isDone)
                yield return null;
        
            _loadedScenes.Remove(SceneToString(sceneNumber));
        }

        private string SceneToString(int sceneNumber)
        {
            return Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(sceneNumber));
        }
    
        private int SceneToInt(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (SceneUtility.GetScenePathByBuildIndex(i).EndsWith(sceneName + ".unity"))
                {
                    return i;
                }
            }
            return -1; // Not found
        }
    
        private IEnumerator TransitionRoutine(string newSceneName)
        {
            // Show loading screen
            yield return StartCoroutine(ShowLoadingScreen());
        
            // Unload all non-core scenes
            foreach (string scene in new List<string>(_loadedScenes))
            {
                if (!string.Equals(scene, _coreSceneName, StringComparison.OrdinalIgnoreCase) 
                    && !string.Equals(scene, _loadingSceneName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return UnloadSceneRoutine(scene);
                }
            }
        
            // Load new scene
            yield return LoadSceneRoutine(newSceneName);
        
            // Hide loading screen
            yield return HideLoadingScreen();
        }
    
        private IEnumerator TransitionRoutine(int newSceneNumber)
        {
            // Show loading screen
            yield return StartCoroutine(ShowLoadingScreen());
        
            // Unload all non-core scenes
            foreach (string scene in new List<string>(_loadedScenes))
            {
                if (scene != _coreSceneName)
                {
                    yield return UnloadSceneRoutine(scene);
                }
            }
        
            // Load new scene
            yield return LoadSceneRoutine(newSceneNumber);
        
            // Hide loading screen
            yield return HideLoadingScreen();
        }

        private IEnumerator LoadSceneInternal(string sceneName, bool skipMinLoadingTime = false)
        {
            // Check if already loaded
            if (SceneIsLoaded(sceneName)) yield break;

            float startTime = Time.realtimeSinceStartup;
        
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            loadOp.allowSceneActivation = false;

            if (!skipMinLoadingTime)
            {
                // Simulate minimum load time
                while (loadOp.progress < 0.9f || (Time.realtimeSinceStartup - startTime) < _minimumLoadTime)
                {
                    // Update loading bar here
                    yield return null;
                }
            }
        
            // Activate the scene
            loadOp.allowSceneActivation = true;
        
            while (!loadOp.isDone) 
                yield return null;
        
            if (!_loadedScenes.Contains(sceneName))
                _loadedScenes.Add(sceneName);
        }
    
        private IEnumerator LoadSceneInternal(int sceneNumber)
        {
            // Check if already loaded
            if (SceneIsLoaded(sceneNumber)) yield break;

            float startTime = Time.realtimeSinceStartup;
        
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Additive);
            loadOp.allowSceneActivation = false;
        
            // Simulate minimum load time
            while (loadOp.progress < 0.9f || (Time.realtimeSinceStartup - startTime) < _minimumLoadTime)
            {
                // Update loading bar here
                yield return null;
            }
        
            // Activate the scene
            loadOp.allowSceneActivation = true;
        
            while (!loadOp.isDone) 
                yield return null;
        
            if (!_loadedScenes.Contains(SceneToString(sceneNumber)))
                _loadedScenes.Add(SceneToString(sceneNumber));
        }

        private bool SceneIsLoaded(string sceneName)
        {
            return _loadedScenes.Contains(sceneName);
        }
    
        private bool SceneIsLoaded(int sceneNumber)
        {
            return _loadedScenes.Contains(SceneToString(sceneNumber));
        }

        // Implement these with your UI system
        private IEnumerator ShowLoadingScreen()
        {
            // Load transition scene if not already loaded
            yield return LoadSceneInternal(_loadingSceneName, true);
        }
    
        private IEnumerator HideLoadingScreen()
        {
            yield return UnloadSceneRoutine(_loadingSceneName);
        }
    }
}