using Core.Scripts.Utils;
using UnityEngine;
using System;
using System.Threading.Tasks;
using NaughtyAttributes;

namespace Core.Scripts.StateManagement
{
    public class GameStateManager : Singleton<GameStateManager>
    {

        [SerializeField] private GameState _initialState;

        public GameState CurrentState { get; private set; }

        public bool IsInTransition { get; private set; } = false;


        public event Action<GameState, GameState> OnStateChanged;

        protected void Start()
        {
            SetStateAsync(_initialState);
        }

        public async Task SetStateAsync(GameState newState)
        {
            if (newState == null || newState == CurrentState) 
                return;
            
            if (IsInTransition)
            {
                Debug.LogWarning($"[GameStateManager] Attempted to change state from {CurrentState} while already in transition. Ignoring request.");
                return;
            }

            IsInTransition = true;
            var oldState = CurrentState;

            if (CurrentState != null)
            {
                await CurrentState.OnExitAsync();
            }
            
            await newState.OnEnterAsync();
            
            CurrentState = newState;
            Debug.Log($"[GameStateManager] State changed to: {newState.name}");

            OnStateChanged?.Invoke(oldState, newState);
            
            IsInTransition = false;
        }
    }
}