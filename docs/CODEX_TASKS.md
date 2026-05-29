# Codex Task Breakdown

## Task 1: Repository and Package Baseline

- Verify Unity opens without compile errors.
- Let Unity resolve `com.cysharp.r3` from OpenUPM and update `Packages/packages-lock.json`.
- Confirm `VContainer`, `UniTask`, `R3`, and `DOTween` are available to project scripts.

Acceptance: Unity console has no compile errors after package restore.

## Task 2: Core Contracts

- Add `IState`, `IStatesController<TEnum>`, and `StatesController<TEnum>`.
- Add `IService` with async initialize/release lifecycle.
- Add unit tests for transition order and cancellation propagation.

Acceptance: tests prove `ExitAsync` runs before the next `EnterAsync`.

## Task 3: UI Base Layer

- Add `IUIViewModel`, `UIView`, and `UIView<TVm>`.
- Add a UI prefab factory/registry that receives prefab references from the composition root.
- Views own Unity references and disposable bindings only.
- View models stay plain C# classes.
- Do not create UI controls or layout from C#; all screens must be prefab assets.

Acceptance: a view can initialize, bind, release, and reinitialize without duplicate subscriptions.

## Task 4: Boot States

- Add `BootStateCode` enum with `Splash`, `Load`, and `Menu`.
- Implement `SplashState`, `LoadState`, and `MenuState`.
- `LoadState` exposes read-only R3 progress and updates it in five cancellable steps.

Acceptance: repeated transitions do not leak subscriptions or throw NREs.

## Task 5: UI Screens

- Create simple splash, loading, and menu uGUI prefabs under `Assets/Prefabs/UI`.
- Reference those prefabs from the `LifetimeScope` or a serialized UI catalog/settings asset.
- Loading view binds to load progress and updates the progress bar.
- Menu view exposes a Restart button command that re-enters `Load`.

Acceptance: entering Play Mode runs Splash -> Load -> Menu from instantiated prefabs, and Restart runs Load -> Menu again.

## Task 6: Composition Root

- Add a project `LifetimeScope`.
- Register settings, services, states, state controller, view models, and views.
- Register UI prefabs/catalog through serialized references; no resource-path strings for screen lookup.
- Start the initial boot state from a Unity entry point without static state.

Acceptance: all runtime dependencies come from VContainer.

## Task 7: Final Verification

- Run EditMode tests.
- Run a PlayMode smoke pass.
- Update `README.md`, `SELF_NOTES.md`, and `AI_LOG.md` with final implementation notes.

Acceptance: repository is ready to submit without `Library/`, `Temp/`, `Logs/`, or generated IDE files.
