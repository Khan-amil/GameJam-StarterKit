namespace Core.Scripts.SceneManagement
{
    public interface ILoadingScreen
    {
        float Progress { get; set; }
        
        void StartLoadingScreen();
        
        void StopLoadingScreen();
    }
}