# Boot Flow Architecture

## Goal

Build variant A from the assignment: a three-state application boot flow implemented with a custom async state machine, VContainer DI, UniTask cancellation, and R3 reactive UI bindings.

## Layers

- `Core`: generic state-machine contracts and implementation.
- `Boot`: concrete boot states and boot flow enum.
- `Services`: async lifecycle abstraction for long-lived services.
- `UI`: base `UIView`, generic `UIView<TVm>`, view models, and MonoBehaviour views.
- `Composition`: Unity `LifetimeScope` that registers settings, services, states, view models, and controllers.

## UI Prefab Policy

- All screens are authored as prefab assets under `Assets/Prefabs/UI`.
- Runtime code may instantiate prefab views through a UI factory/registry registered in VContainer.
- Runtime code must not create or hardcode UI hierarchies, layout, colors, sprites, labels, buttons, progress bars, or anchors.
- Dynamic UI values may be bound by views, but any display format belongs to the prefab serialized fields. For example, loading percent uses `ProgressFormat` on `LoadingUIView.prefab`.
- The scene should contain only stable hosts such as the composition root, event system, camera, and canvas/root transform needed for prefab placement.

## Main Contracts

- `IState`: exposes `UniTask EnterAsync(CancellationToken ct)` and `UniTask ExitAsync(CancellationToken ct)`.
- `IStatesController<TEnum>`: exposes `UniTask EnterStateAsync(TEnum code, CancellationToken ct)`.
- `StatesController<TEnum>`: stores the active state and guarantees `await current.ExitAsync(ct)` before `await next.EnterAsync(ct)`.
- `IService`: exposes `InitializeAsync` and `ReleaseAsync` for app-level service lifetime.
- `UIView`: owns `Initialize()` and `Release()`.
- `UIView<TVm>`: binds a plain C# view model to Unity references; view models are not MonoBehaviours.

## Boot Flow

1. `SplashState` shows splash UI, waits one second through `UniTask.Delay(..., cancellationToken: ct)`, then enters `Load`.
2. `LoadState` shows loading UI and updates an R3 `ReactiveProperty<float>` from `0` to `1` over five 200 ms steps.
3. `LoadingUIView` binds the progress bar fill and percent text to the `LoadState` progress provider.
4. `MenuState` shows the menu UI. The Restart button enters `Load`.

## Reactive Policy

- Use R3 for observable values and UI commands.
- Every subscription is stored in a disposable container owned by the view/state that created it.
- `Release()` or `ExitAsync()` disposes all subscriptions created during `Initialize()` or `EnterAsync()`.
- No raw `event Action` for cross-layer state notifications.

## DI Policy

- Register implementations as interfaces in VContainer, for example `builder.Register<StatesController<BootStateCode>>(Lifetime.Singleton).As<IStatesController<BootStateCode>>()`.
- Register ScriptableObject settings through serialized `LifetimeScope` fields and `builder.RegisterInstance`.
- Do not use `FindObjectOfType`, `Singleton.Instance`, or static mutable state.

## Cancellation Policy

- All async operations accept and pass through `CancellationToken`.
- No `async void` except Unity callbacks if absolutely required.
- State transitions should be safe when cancellation happens during splash delay, loading delay, or UI transition.
