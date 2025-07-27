# GameJam StarterKit

Welcome to the **GameJam StarterKit** project!
This project is set up to help you kickstart your game development in Unity.
It is made for Unity 6.1 and above, setup for URP and both the legacy and the new Unity Input System.
---

## 📂 Project Structure

Here’s an overview of the main project structure:

- **Assets/Core**: Contains all essential scripts and reusable tools for the project. Also contains general project settings.
- **Assets/_Game**: Put the assets for your game here.
- **Assets/Plugins**: Includes third-party plugins and libraries used in the project.
- **Assets/Core/StarterKit Plugins**: Contains optional plugins and system developed for the StarterKit, see below for details.

---

## StarterKit Plugins

The following plugins are included in the StarterKit, feel free to use them if you find them useful:
- [Pool system](Assets/Core/StarterKit%20Plugins/Pooling/Readme.md): Generic and straightforward pooling system.
- [Scene Management](Assets/Core/StarterKit%20Plugins/Scene%20Management/Readme.md): A manager to help having clean scene transitions.
- [GameStateManager](Assets/Core/StarterKit%20Plugins/GameStateManager/Readme.md): A set of extandable ScriptableObjects to manage game states and transitions.
- SoundManager: A simple script to centralize sound, to play music and spawn sfx with pooling.
- UIManager: Open and manage menu, including stacking. Supports Dotween to feedback the menu transitions. 

They are all setup in the `Core` scene loaded by default, but they're designed to not have any impact by default.

---

## External projects and packages included

Some projects and packages are included through openUpm:
- [PlayerPrefs Editor](https://github.com/Dysman/bgTools-playerPrefsEditor) (GPL3.0): A tool to easily edit PlayerPrefs in the Unity Editor.
- [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes) (MIT): A collection of attributes to enhance the Unity Inspector.
- [In game Debug Console](https://github.com/yasirkula/UnityIngameDebugConsole) (MIT): A console for debugging in-game, useful for logging and testing.
- Cinemachine
- TextMeshPro
- [Dotween](https://dotween.demigiant.com/download.php): A fast and efficient tweening engine for Unity, useful for animations and transitions.

---

## Contribution and support

If you have any questions, suggestions, or issues,
feel free to open an issue on the GitHub repository or contact the project maintainers.

Merge requests are welcome!

---

## Other resources

Check out these links to get some assets to get you started on your game jam:

- [Kenney.nl](https://kenney.nl/assets): A great source for free game assets.
- [OpenGameArt.org](https://opengameart.org/): A community-driven site for free game art.
- [itch.io](https://itch.io/game-assets/free): A platform with a wide range of free and paid game assets.
- [Unity Asset Store](https://assetstore.unity.com/): Unity's official asset store with a variety of free and paid assets.
- [GameDev Market](https://www.gamedevmarket.net/): A marketplace for game assets, including free and paid options.
- [CraftPix](https://craftpix.net/freebies/): Offers free game assets, including 2D and 3D graphics.
- [FreePBR](https://freepbr.com/): A resource for free PBR textures and materials.
- [GameArt2D](https://gameart2d.com/freebies.html): Provides free 2D game art assets.
- [GameDev Academy](https://www.gamedevacademy.org/free-game-assets/): A collection of free game assets for various genres.
