using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BootFlow.UI
{
    public sealed class UIScreenFactory : IUIScreenFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly Transform _root;
        private readonly UIScreenCatalog _catalog;
        private UIView _currentView;

        public UIScreenFactory(IObjectResolver resolver, Transform root, UIScreenCatalog catalog)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _root = root != null ? root : throw new ArgumentNullException(nameof(root));
            _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
        }

        public UIView Show(UIScreenCode screenCode)
        {
            HideCurrent();

            var prefab = _catalog.GetPrefab(screenCode);
            var instance = _resolver.Instantiate(prefab.gameObject, _root, false);
            _currentView = instance.GetComponent<UIView>();
            _currentView.Initialize();
            return _currentView;
        }

        public void HideCurrent()
        {
            if (_currentView == null)
            {
                return;
            }

            _currentView.Release();
            UnityEngine.Object.Destroy(_currentView.gameObject);
            _currentView = null;
        }
    }
}
