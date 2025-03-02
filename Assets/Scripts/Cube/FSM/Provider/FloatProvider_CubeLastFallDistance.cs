using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class FloatProvider_CubeLastFallDistance : FloatProviderBase
    {
        [SerializeField] private State_Cube_Fall _fallState = null;

        [SerializeField] private float _coef = 1;

        [SerializeField] private float _minValue = int.MinValue;
        [SerializeField] private float _maxValue = int.MaxValue;
        
        public override float GetValue()
        {
            float value = _fallState.LastFallDistance * _coef;
            
            value = Mathf.Clamp(value, _minValue, _maxValue);

            return value;
        }

        public override void SetValue(float targetVal)
        {
        }
    }
}