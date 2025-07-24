using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class QuitOnClick : MonoBehaviour
    {
        [SerializeField] private Button _button;
         
        private void Awake()
        {
            if (!_button)
            {
                _button = GetComponent<Button>();
            }

            if (_button)
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
            Debug.Log("Application Quit requested.");
            Application.Quit();
            
            // If running in the editor, stop playing
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}