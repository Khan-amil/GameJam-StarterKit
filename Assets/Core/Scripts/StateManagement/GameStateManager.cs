using Core.Scripts.Utils;
using UnityEngine;
using System;
using NaughtyAttributes;

namespace Core.Scripts.StateManagement
{
    public class GameStateManager : Singleton<GameStateManager>
    {

        [SerializeField] private GameState _initialState;

        public GameState CurrentState { get; private set; }

        public event Action<GameState, GameState> OnStateChanged;

        protected override void Awake()
        {
            SetState(_initialState);
        }

        public void SetState(GameState newState)
        {
            if (newState == null || newState == CurrentState) return;

            var oldState = CurrentState;
            CurrentState = newState;
            Debug.Log($"[GameStateManager] State changed to: {newState.name}");

            OnStateChanged?.Invoke(oldState, newState);
        }

#if UNITY_EDITOR
        
        public GameState _debugState;
        
        [Button]
        private void DebugSetState()
        {
            if (_debugState != null)
            {
                SetState(_debugState);
            }
            else
            {
                Debug.LogWarning("Debug state is not set.");
            }
        }
#endif
    }
}