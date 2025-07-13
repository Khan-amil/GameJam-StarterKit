using System.Threading.Tasks;

namespace Core.Scripts.StateManagement
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "GameState/Base State")]
    public class GameState : ScriptableObject
    {

        public string DisplayName { get; }
        public Color DisplayColor { get; } = Color.white;

        [SerializeField]
        private bool _inputActive = true;
        [SerializeField]
        private bool _gameplayPaused = false;
        
        public virtual Task OnEnterAsync()
        {
            Debug.Log($"[GameState] Entering state: {DisplayName}");
            return Task.CompletedTask;
        }
        
        public virtual Task OnExitAsync()
        {
            Debug.Log($"[GameState] Exiting state: {DisplayName}");
            return Task.CompletedTask;
        }

        public virtual void OnDebugGui()
        {
            
        }
    }
}