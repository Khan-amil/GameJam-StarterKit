using UnityEngine;

namespace Core.Scripts.SceneManagement.Sample
{
    public class LoadingScreenFeedback : MonoBehaviour, ILoadingScreen
    {

        [SerializeField] private UnityEngine.UI.Slider _slider;
        
        private bool _isLoading;
        
        public float Progress { get; set; }
        
        public void StartLoadingScreen()
        {
            _isLoading = true;
        }

        public void StopLoadingScreen()
        {
            _isLoading = false;
        }
        
        void Update()
        {
            if (_isLoading)
            {
                _slider.value = Progress;
            }
        }
    }
}