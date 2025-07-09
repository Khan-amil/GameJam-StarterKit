using System;
using Core.Scripts.SceneManagement;
using NueGames.NueDeck.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NueGames.NueDeck.Scripts.Utils
{
    [DefaultExecutionOrder(-11)]
    public class CoreLoader : MonoBehaviour
    {
        private void Awake()
        {
            try
            {
                if (!GameManager.Instance)
                    TransitionSceneManager.Instance.LoadScene("NueCore");
                Destroy(gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError("You should add NueCore scene to build settings!");
                throw;
            }
           
        }
    }
}