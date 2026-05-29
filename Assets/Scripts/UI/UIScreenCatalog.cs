using System;
using System.Collections.Generic;

namespace BootFlow.UI
{
    public sealed class UIScreenCatalog
    {
        private readonly IReadOnlyDictionary<UIScreenCode, UIView> _prefabs;

        public UIScreenCatalog(IReadOnlyDictionary<UIScreenCode, UIView> prefabs)
        {
            _prefabs = prefabs ?? throw new ArgumentNullException(nameof(prefabs));
        }

        public UIView GetPrefab(UIScreenCode screenCode)
        {
            if (!_prefabs.TryGetValue(screenCode, out var prefab) || prefab == null)
            {
                throw new KeyNotFoundException($"UI prefab for '{screenCode}' is not registered.");
            }

            return prefab;
        }
    }
}
