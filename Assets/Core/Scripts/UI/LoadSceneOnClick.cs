using Core.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class LoadSceneOnClick : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [SerializeField] private string _sceneName;

        [SerializeField] private bool _useTransitionScreen = false;
        private void Awake()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }

            if (_button != null)
            {
                _button.onClick.AddListener(OnButtonClick);
            }
            else
            {
                Debug.LogError("Button component is not assigned or found on the GameObject.");
            }
        }
        
        private void OnButtonClick()
        {
            if (string.IsNullOrEmpty(_sceneName))
            {
                Debug.LogError("Scene name is not assigned.");
                return;
            }

            if (_useTransitionScreen)
            {
                TransitionSceneManager.Instance.TransitionToScene(_sceneName);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
            }
        }
    }
}