using System.Threading.Tasks;
using Core.Scripts.StateManagement.Transitions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.StateManagement.Transitions
{
    public class InputTransition : GameStateTransition
    {

        [Header("Action to activate")]
        [SerializeField] private InputActionMap _actionMap;

        [SerializeField] private bool _invertActivation = false;
        
    
        public override Task OnEnterAsync()
        {
            if (_invertActivation)
                _actionMap.Disable();
            else
                _actionMap.Enable();
            return Task.CompletedTask;
        }

        public override Task OnExitAsync()
        {
            if (_invertActivation)
                _actionMap.Enable();
            else
                _actionMap.Disable();
            return Task.CompletedTask;
        }

        public override void OnDebugGUI()
        {
            GUILayout.Label($"{_actionMap.name}, is {_actionMap.enabled}, " +
                            $"will {( _invertActivation ? "enable" : "disable")} on exit.");
        }
    }
}