# SwishIt 🏀

A basketball shooting game built in Unity as a technical test.
Charge your throw, aim, and shoot, score points, buy new balls, score even more.

## 🕹️ Build

[Download latest build on itch.io](https://bisic.itch.io/swishit)

## 📋 Infos

- Unity **6000.0.58f2**
- Template: **Universal 3D (URP)**
- New Unity Input System

## 🎮 Controls

| Action | Input |
|--------|-------|
| Move | WASD / ZQSD / Arrow Keys |
| Look | Mouse |
| Grab / Interact | Left Click |
| Charge Throw | Hold Left Click |
| Release Throw | Release Left Click |
| Cap FPS 30 / 60 / 90 | F1 / F2 / F3 |
| Quit | Escape |

## 🏗️ Architecture

### MonoBehaviours

| Category | Scripts |
|----------|---------|
| Ball | `Ball`, `BallSpawner` |
| Shop | `ShopManager`, `ShopElement` |
| Input | `BaseInputManager`, `Grab`, `PlayerMovement`, `Interactable` |
| UI | `DisplayMoney`, `DisplayFPS` |
| Utility | `TriggerEffect`, `Destructible`, `Snapper` |
| Debug | `CapFPS`, `QuitApplication` |

### ScriptableObjects
- `BallData` — ball parameters
- `PlayerData` — player stats and settings

## 🏷️ Tags
- `Grabbable`
- `Target`
- `Interactable`

## ⚙️ Input Setup
- Created `PlayerActions` (InputActionAsset) assigned as **Project-wide Input Action**
- ⚠️ Do not use `PlayerInput` component : interferes with mouse events

## 📝 Notes
- Removed Screen Space Ambient Occlusion for VR compatibility
- Architecture designed for easy PC -> VR conversion

## 🔄 PC → VR Conversion
The project is structured to facilitate VR migration:
- Camera rig ready to be replaced by XR Rig
- Physics-based interactions compatible with hand tracking
