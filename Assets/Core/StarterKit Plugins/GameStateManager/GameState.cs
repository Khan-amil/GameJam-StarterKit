using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Scripts.StateManagement.Transitions;

namespace Core.Scripts.StateManagement
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "GameState/Base State")]
    public class GameState : ScriptableObject
    {

        /// <summary>
        /// Debug name for this state, used in logs and UI.
        /// </summary>
        public string DisplayName { get; }
        
        /// <summary>
        /// Debug color for this state, used in UI to visualize the state.
        /// </summary>
        public Color DisplayColor { get; } = Color.white;

        /// <summary>
        /// Ordered list of actions to perform when entering or exiting this state.
        /// </summary>
        public List<GameStateTransition> TransitionActions { get; } = new List<GameStateTransition>();
        
        public virtual async Task OnEnterAsync()
        {
            Debug.Log($"[GameState] Entering state: {DisplayName}");

            foreach (GameStateTransition action in TransitionActions)
            {
                Debug.Log($"[GameState] Starting transition action: {action.GetType()}");
                await action.OnEnterAsync();
            }
        }
        
        public virtual async Task OnExitAsync()
        {
            Debug.Log($"[GameState] Exiting state: {DisplayName}");
            
            foreach (GameStateTransition action in TransitionActions)
            {
                Debug.Log($"[GameState] Starting transition action: {action.name}({action.GetType()}) ");
                await action.OnExitAsync();
            }
        }

        public virtual void OnDebugGui()
        {
            GUILayout.BeginVertical(new GUIContent(DisplayName), GUI.skin.box);
            foreach (GameStateTransition action in TransitionActions)
            {
                action.OnDebugGUI();
            }
            GUILayout.EndVertical();
        }
    }
}