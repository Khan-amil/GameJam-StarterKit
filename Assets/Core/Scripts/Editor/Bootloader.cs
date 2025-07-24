using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public static class BootstrapLoader
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        // Always ensure Core is loaded first
        if (!IsCoreSceneLoaded())
        {
#if UNITY_EDITOR
            // Editor workflow: Load Core additively
            EditorSceneManager.LoadScene("Assets/Core/Scenes/Core.unity", 
                LoadSceneMode.Additive);
#else
            // Build workflow
            UnityEngine.SceneManagement.SceneManager.LoadScene("Core", 
                LoadSceneMode.Additive);
#endif
        }
    }

    private static bool IsCoreSceneLoaded()
    {
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name == "Core")
                return true;
        }
        return false;
    }
}