# SELF_NOTES

## Decisions

- I chose variant A, Boot Flow, because it directly demonstrates application startup orchestration, async cancellation, DI boundaries, and disposable reactive UI bindings.
- R3 is selected for reactive state because the task allows UniRx/R3 and R3 is the modern Cysharp direction for Unity reactive code.
- VContainer will own service/state/view-model creation. MonoBehaviour views stay thin and only bind serialized Unity references to plain C# view models.

## Why This Shape

- The state machine stays generic as `IStatesController<TEnum>` so the boot flow can be tested without Unity scene objects.
- States expose only behavior needed by other layers. `LoadState` owns progress as an R3 reactive property, while `LoadingUIView` only subscribes and renders it.
- Async work uses `UniTask` and `CancellationToken` end to end so state changes and application shutdown can cancel delays safely.
- UI is prefab-driven: code may instantiate registered prefab views, but must not create buttons, bars, labels, or layout hierarchies from hardcoded runtime construction.

## AI Usage

- AI was used to inspect the project scaffold, extract the assignment text from the PDF, and draft the architecture/task documentation.
- The implementation has been reviewed through compilation, EditMode tests, and a Play Mode smoke pass. Next manual review should focus on visual polish, repeated restart behavior, and prefab reference validation.

## Key Answers

- Why a state machine: the task explicitly requires deterministic `ExitAsync` then `EnterAsync` order, and a controller centralizes that rule.
- Why view models are plain classes: it keeps Unity references out of logic and makes binding/restart behavior easier to test.
- Why no singletons/static state: the task forbids them and VContainer gives a clearer lifetime model.
- Why UI prefabs: prefab assets keep visual structure editable in Unity and prevent hidden UI layout decisions from being buried in C#.
