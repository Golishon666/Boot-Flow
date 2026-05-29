# Boot Flow

Unity test project for the Boot Flow variant.

## Current Status

The repository contains the first working Boot Flow implementation: Splash -> Load -> Menu, with restart from Menu back through Load. Runtime orchestration is built around a custom async state controller, VContainer composition, UniTask cancellation, and R3 UI bindings.

## Requirements

- Unity `6000.3.14f1`
- C#
- VContainer
- UniTask
- R3 `Assets/Plugins/R3`
- DOTween `Assets/Plugins/DOTween`

## How to Open

1. Clone the repository.
2. Open the repository root in Unity Hub with Unity `6000.3.14f1`.
3. Open `Assets/Scenes/SampleScene.unity`.

## What I Would Do With 2 More Hours

- Add a PlayMode automation test for repeated restart flow.
- Replace placeholder visual styling with polished prefab-authored feedback and DOTween transitions.
- Add a prefab validation test that fails if required UI references are missing.
