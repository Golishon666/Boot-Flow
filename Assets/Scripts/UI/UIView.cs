using System;
using R3;
using UnityEngine;

namespace BootFlow.UI
{
    public abstract class UIView : MonoBehaviour
    {
        private CompositeDisposable _bindings;

        public void Initialize()
        {
            Release();
            _bindings = new CompositeDisposable();
            gameObject.SetActive(true);
            OnInitialize(_bindings);
        }

        public void Release()
        {
            OnRelease();
            _bindings?.Dispose();
            _bindings = null;
        }

        protected virtual void OnInitialize(CompositeDisposable bindings)
        {
        }

        protected virtual void OnRelease()
        {
        }

        protected IDisposable AddBinding(IDisposable disposable)
        {
            _bindings?.Add(disposable);
            return disposable;
        }
    }
}
