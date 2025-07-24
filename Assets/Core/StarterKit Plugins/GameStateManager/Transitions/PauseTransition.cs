using System.Threading.Tasks;
using Core.Scripts.StateManagement.Transitions;
using UnityEngine;

namespace Core.StateManagement.Transitions
{
    public class PauseTransition : GameStateTransition
    {

        float _originalTimeScale;
    
        public override Task OnEnterAsync()
        {
            _originalTimeScale = Time.timeScale;
            Time.timeScale = 0;
            return Task.CompletedTask;
        }

        public override Task OnExitAsync()
        {
            Time.timeScale = _originalTimeScale;
            return Task.CompletedTask;
        }

        public override void OnDebugGUI()
        {
            GUILayout.Label($"Paused, will resume at: {Time.timeScale}");
        }
    }
}