# AI_LOG

## Prompts Used

- User asked to prepare the Boot Flow project, split the work into sequential Codex subtasks, create a separate architecture document, and use R3 for reactive code.
- User clarified that the GitHub repository is empty and the local Unity project should be uploaded there.

## AI Work Done

- Inspected `C:\Unity Project\Boot-Flow`.
- Confirmed Unity version `6000.3.14f1`.
- Confirmed existing packages: VContainer, UniTask, DOTween, Input System, URP 2D.
- Extracted the assignment requirements from `Quiz, Plese! Unity Middle+.pdf`.
- Added documentation and repository metadata for the next implementation step.
- Fixed R3 compilation by vendoring the required NuGet assemblies into `Assets/Plugins/R3`.
- Implemented the first boot-flow runtime: generic state controller, Splash/Load/Menu states, VContainer composition root, prefab UI factory, R3 view-model bindings, and EditMode tests.
- Created prefab-authored uGUI screens for splash, loading, and menu. Runtime code instantiates prefabs and binds dynamic values only; it does not construct UI hierarchies.

## Corrections / Risks

- The local folder was not a git repository, while the remote repository only had placeholder files.
- R3 Unity integration is registered through OpenUPM in `Packages/manifest.json`; its core NuGet assemblies are committed as plugin DLLs because the OpenUPM package expects those precompiled references to exist.
- Loading percent text is updated at runtime, but the text component and format string are serialized on `LoadingUIView.prefab` to keep visible UI decisions in prefab assets.
