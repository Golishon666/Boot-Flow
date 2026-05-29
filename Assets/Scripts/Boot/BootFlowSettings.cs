using UnityEngine;

namespace BootFlow.Boot
{
    [CreateAssetMenu(menuName = "Boot Flow/Boot Flow Settings", fileName = "BootFlowSettings")]
    public sealed class BootFlowSettings : ScriptableObject
    {
        [SerializeField, Min(0f)] private float _splashSeconds = 1f;
        [SerializeField, Min(1)] private int _loadSteps = 5;
        [SerializeField, Min(0f)] private float _loadStepSeconds = 0.2f;

        public float SplashSeconds => Mathf.Max(0f, _splashSeconds);
        public int LoadSteps => Mathf.Max(1, _loadSteps);
        public float LoadStepSeconds => Mathf.Max(0f, _loadStepSeconds);
    }
}
