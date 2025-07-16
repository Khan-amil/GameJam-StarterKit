# State Management System

The `GameStateManager.cs` singleton helps manage and centralize the curent game state and transitions to different states. 
This system is designed to handle various game states such as menus, gameplay, paused state, etc.
For example, it can handle timescale for pause menus, cut down inputs when in dialogs or menus...

## Usage
1. You need to add the GameStateManager Monobehaviour in a scene before using.
   It is already included in the `GameManager` prefab put in the `Core` scene loaded automatically at start.
2. Create a Game State from the project view via `Create > Game State > Base State`.
3. Create the Game State Transition from the project view via `Create > Game State > Transition`.
   You can extend the `GameStateTransition` class to create custom transitions.
4. Add the created transitions to the Game State by selecting the Game State in the project view and adding the transitions in the inspector.
   The transitions will be executed when the game state is changed, in the order they are listed in the inspector.
5. Use the `GameStateManager` to change the game state in your scripts.
   You can access the current state with `GameStateManager.Instance.CurrentState`.
   To change the state, use `GameStateManager.Instance.ChangeStateAsync(newState)`.
6. Optionally, you can use the `GameStateDebugger` to visualize and debug the current state and transitions.

## Classes description

### GameStateManager.cs

Handles the switching of game states.
- Keeps track of the current state.
- Provides methods to change states safely.
- Calls the appropriate lifecycle methods (`OnEnterAsync`, `OnExitAsync`) on states when switching.
  - Note that the methods are asynchronous to allow for smooth transitions, especially useful for loading screens or animations.

Usage example:
```csharp


// In the script where we want to handle inputs

// State with a transition to pause/unpause logic/time setup
public PauseState myPauseState; 
public GameState myPlayingState;
...
    
if( Input.getKeyDown(KeyCode.Escape) )
{
    if( GameStateManager.Instance.CurrentState is PlayingState )
    {
        // Switch to pause state
        await GameStateManager.Instance.ChangeStateAsync(myPauseState);
    }
    else if( GameStateManager.Instance.CurrentState is PausedState )
    {
        // Resume the game
        await GameStateManager.Instance.ChangeStateAsync(new PlayingState());
    }
}
```
### GameState

Defines the base class for all game states. 
It supports asynchronous entry and exit methods, allowing for smooth transitions and loading screens.
It should be enough for most scenarios, but if you need more custom logic, you can extend it.

### GameStateTransition

Each transition is an atomic action that will be took when the game change state.
Extend it to create custom transitions. 

### GameStateDebugger.cs

A utility for debugging the state management system.
- Displays the current state and associated informations.
- Can be used in the editor or at runtime to debug and force state changes.
