using System.Collections.Generic;
using BootFlow.Boot;
using BootFlow.Boot.States;
using BootFlow.StateMachine;
using BootFlow.UI;
using BootFlow.UI.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BootFlow.Composition
{
    public sealed class BootFlowLifetimeScope : LifetimeScope
    {
        [SerializeField] private BootFlowSettings _settings;
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private SplashUIView _splashPrefab;
        [SerializeField] private LoadingUIView _loadingPrefab;
        [SerializeField] private MenuUIView _menuPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_settings).As<BootFlowSettings>();
            builder.RegisterInstance(CreateCatalog()).As<UIScreenCatalog>();

            builder.Register<SplashUIViewModel>(Lifetime.Singleton).AsSelf();
            builder.Register<LoadingUIViewModel>(Lifetime.Singleton).AsSelf();
            builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf();

            builder.Register<BootStateTransitionRouter>(Lifetime.Singleton)
                .AsSelf()
                .As<IBootStateTransition>();

            builder.Register<SplashState>(Lifetime.Singleton).AsSelf();
            builder.Register<LoadState>(Lifetime.Singleton)
                .AsSelf()
                .As<ILoadProgressProvider>();
            builder.Register<MenuState>(Lifetime.Singleton).AsSelf();

            builder.Register<IUIScreenFactory>(
                resolver => new UIScreenFactory(resolver, _uiRoot, resolver.Resolve<UIScreenCatalog>()),
                Lifetime.Singleton);

            builder.Register<IStatesController<BootStateCode>>(
                resolver => new StatesController<BootStateCode>(new Dictionary<BootStateCode, IState>
                {
                    { BootStateCode.Splash, resolver.Resolve<SplashState>() },
                    { BootStateCode.Load, resolver.Resolve<LoadState>() },
                    { BootStateCode.Menu, resolver.Resolve<MenuState>() }
                }),
                Lifetime.Singleton);

            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Resolve<BootStateTransitionRouter>()
                    .Bind(resolver.Resolve<IStatesController<BootStateCode>>());
            });

            builder.RegisterEntryPoint<BootFlowEntryPoint>();
        }

        private UIScreenCatalog CreateCatalog()
        {
            return new UIScreenCatalog(new Dictionary<UIScreenCode, UIView>
            {
                { UIScreenCode.Splash, _splashPrefab },
                { UIScreenCode.Loading, _loadingPrefab },
                { UIScreenCode.Menu, _menuPrefab }
            });
        }
    }
}
