# Boot Flow

Unity test project for the Boot Flow variant.

## Current Status

The repository contains the Unity 6 project scaffold and implementation documentation for the boot flow task. The next Codex step should implement the runtime code according to `docs/CODEX_TASKS.md` and `docs/ARCHITECTURE.md`.

## Requirements

- Unity `6000.3.14f1`
- C#
- VContainer
- UniTask
- R3 via OpenUPM
- DOTween is already present in `Assets/Plugins/DOTween`
- UI screens must be prefab assets; runtime code must not hardcode or construct UI hierarchies.

## How to Open

1. Clone the repository.
2. Open the repository root in Unity Hub with Unity `6000.3.14f1`.
3. Let Unity restore packages from `Packages/manifest.json`.
4. Open `Assets/Scenes/SampleScene.unity`.

## What I Would Do With 2 More Hours

- Implement the full state machine and the three boot states.
- Add simple uGUI prefabs for splash, loading progress, and menu restart.
- Add EditMode tests for state transition order and cancellation.
- Add a PlayMode smoke test for repeated restart flow.
- Replace placeholder UI with polished visual feedback and DOTween transitions.
