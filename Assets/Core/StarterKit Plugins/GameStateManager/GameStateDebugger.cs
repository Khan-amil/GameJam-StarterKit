using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Scripts.StateManagement
{
    public class GameStateDebugger : MonoBehaviour
    {
        public GameState _debugState;
        
        bool _canChangeState = true;
        
        bool CanChangeState => _canChangeState && GameStateManager.Instance != null && !GameStateManager.Instance.IsInTransition;
        
        [Button(enabledMode:EButtonEnableMode.Playmode)]
        private async void DebugSetState()
        {
            
            try
            {
                if (_debugState != null)
                {
                    _canChangeState = false;
                    await GameStateManager.Instance.SetStateAsync(_debugState);
                    _canChangeState = true;
                }
                else
                {
                    Debug.LogWarning("Debug state is not set.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private void OnGUI()
        {
            if (GameStateManager.Instance is null)
            {
                GUILayout.Label("Game State Manager not found", GUILayout.Height(30));
            }
            else
            {
                if (GameStateManager.Instance.CurrentState is not null)
                {
                    GUI.color = GameStateManager.Instance.CurrentState.DisplayColor;
                    GUILayout.Label($"Current State: {GameStateManager.Instance.CurrentState.DisplayName}", GUILayout.Height(30));
                    GUILayout.Label($"Is In Transition: {GameStateManager.Instance.IsInTransition}", GUILayout.Height(30));

                }
         
                if (CanChangeState)
                {
                    if (GUILayout.Button("Set Debug State", GUILayout.Height(30)))
                    {
                        DebugSetState();
                    }
                }
                else
                {
                    GUILayout.Label("Cannot change state during transition", GUILayout.Height(30));
                }
            }
            
        }
    }
}