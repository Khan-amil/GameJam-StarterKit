using System.Threading.Tasks;
using UnityEngine;

namespace Core.Scripts.StateManagement.Transitions
{
    [CreateAssetMenu(menuName = "GameState/Transition")]
    public abstract class GameStateTransition : ScriptableObject
    {
        /// <summary>
        /// Task awaited when entering a state that has this transition.
        /// </summary>
        public abstract Task OnEnterAsync();
        
        /// <summary>
        /// Task awaited when exiting a state that has this transition.
        /// </summary>
        public abstract Task OnExitAsync();

        /// <summary>
        /// (Optional) Used to debug or visualize state transition states.
        /// </summary>
        public abstract void OnDebugGUI();
    }
}