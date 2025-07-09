using UnityEngine;

namespace Core.Scripts.StateManagement
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "GameState/Base State")]
    public class GameState : ScriptableObject
    {
        public string displayName;
        public Color debugColor = Color.white;

        public bool inputActive = true;
        public bool gameplayPaused = false;
    }
}