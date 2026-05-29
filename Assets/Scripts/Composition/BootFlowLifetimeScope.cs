using System;
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
            ValidateSerializedReferences();

            builder.RegisterInstance(_settings).As<BootFlowSettings>();
            builder.RegisterInstance(CreateCatalog()).As<UIScreenCatalog>();

            builder.Register<SplashUIViewModel>(Lifetime.Singleton).AsSelf();
            builder.Register<LoadingUIViewModel>(Lifetime.Singleton).AsSelf();
            builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf();

            builder.Register(
                resolver => new Lazy<IStatesController<BootStateCode>>(
                    () => resolver.Resolve<IStatesController<BootStateCode>>()),
                Lifetime.Singleton);

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

        private void ValidateSerializedReferences()
        {
            ThrowIfMissing(_settings, nameof(_settings));
            ThrowIfMissing(_uiRoot, nameof(_uiRoot));
            ThrowIfMissing(_splashPrefab, nameof(_splashPrefab));
            ThrowIfMissing(_loadingPrefab, nameof(_loadingPrefab));
            ThrowIfMissing(_menuPrefab, nameof(_menuPrefab));
        }

        private static void ThrowIfMissing(UnityEngine.Object value, string fieldName)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{nameof(BootFlowLifetimeScope)} is missing serialized reference '{fieldName}'.");
            }
        }
    }
}
