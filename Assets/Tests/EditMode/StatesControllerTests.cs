using System.Collections.Generic;
using System.Threading;
using BootFlow.StateMachine;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

namespace BootFlow.Tests.EditMode
{
    public sealed class StatesControllerTests
    {
        [Test]
        public async System.Threading.Tasks.Task EnterStateAsync_ExitsCurrentBeforeEnteringNext()
        {
            var trace = new List<string>();
            var first = new TraceState("first", trace);
            var second = new TraceState("second", trace);
            var controller = new StatesController<string>(new Dictionary<string, IState>
            {
                { "first", first },
                { "second", second }
            });

            await controller.EnterStateAsync("first", CancellationToken.None);
            await controller.EnterStateAsync("second", CancellationToken.None);

            CollectionAssert.AreEqual(
                new[] { "enter:first", "exit:first", "enter:second" },
                trace);
        }

        [Test]
        public void EnterStateAsync_PropagatesCancellation()
        {
            var controller = new StatesController<string>(new Dictionary<string, IState>
            {
                { "first", new TraceState("first", new List<string>()) }
            });
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            Assert.CatchAsync<System.OperationCanceledException>(async () =>
            {
                await controller.EnterStateAsync("first", cts.Token);
            });
        }

        private sealed class TraceState : IState
        {
            private readonly string _name;
            private readonly List<string> _trace;

            public TraceState(string name, List<string> trace)
            {
                _name = name;
                _trace = trace;
            }

            public UniTask EnterAsync(CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                _trace.Add($"enter:{_name}");
                return UniTask.CompletedTask;
            }

            public UniTask ExitAsync(CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                _trace.Add($"exit:{_name}");
                return UniTask.CompletedTask;
            }
        }
    }
}
