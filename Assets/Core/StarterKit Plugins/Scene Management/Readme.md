# TransitionSceneManager

`TransitionSceneManager.cs` is a singleton used to simplify scene transitions with a loading screen.

## Setup

The prefab 'SceneManager.prefab' is already added to the 'Core' scene that is auto loaded on launch.

If you don't use it, you need to have the `TransitionSceneManager` script added to a GameObject in your scene.

## Loading Screen

The TransitionSceneManager will automatically show a loading screen when transitioning to a new scene. 
All scenes are unloaded, then the loading screen is displayed, and progress is shown while the new scene is being loaded in the background.
To show progress, your loading scene should have a script in it that implements the `ILoadingScreen` interface.

## Usage

1. **Access the instance**
   ```csharp
   var manager = TransitionSceneManager.Instance;
   ```

2. **Load a scene with transition**
   ```csharp
    // Transition to a scene by name (filename, without extension, case-sensitive)
   manager.TransitionToScene("SceneName");
   // You can also use scene index
    manager.TransitionToScene(1); 
   ```
   This will show a loading screen and transition to the specified scene once it's fully loaded.
3. Before loading the scene, any other scene will be unloaded (except the Core scene).

3. **Load a scene additively**
   ```csharp
   manager.LoadSceneAdditively("SceneName");
   // You can also use scene index
    manager.LoadSceneAdditively(1); 
   ```
    This will load the specified scene additively without unloading the current scene.

## Notes

For more details, see the code documentation in `TransitionSceneManager.cs`.
