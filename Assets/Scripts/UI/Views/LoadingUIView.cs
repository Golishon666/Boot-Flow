using R3;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace BootFlow.UI.Views
{
    public sealed class LoadingUIView : UIView<LoadingUIViewModel>
    {
        public Image ProgressFill;
        public Text ProgressText;
        public string ProgressFormat;

        protected override void OnInitialize(CompositeDisposable bindings)
        {
            RenderProgress(ViewModel.CurrentProgress);
            ViewModel.ProgressChanged.Subscribe(RenderProgress).AddTo(bindings);
        }

        private void RenderProgress(float progress)
        {
            var clampedProgress = Mathf.Clamp01(progress);

            if (ProgressFill != null)
            {
                ProgressFill.fillAmount = clampedProgress;
            }

            if (ProgressText != null)
            {
                var percent = Mathf.RoundToInt(clampedProgress * 100f);
                ProgressText.text = string.IsNullOrWhiteSpace(ProgressFormat)
                    ? percent.ToString(CultureInfo.InvariantCulture)
                    : string.Format(CultureInfo.InvariantCulture, ProgressFormat, percent);
            }
        }
    }
}
