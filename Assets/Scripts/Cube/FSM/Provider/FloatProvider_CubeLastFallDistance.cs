using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class FloatProvider_CubeLastFallDistance : FloatProviderBase
    {
        [SerializeField] private float _coef = 1;

        [SerializeField] private float _minValue = int.MinValue;
        [SerializeField] private float _maxValue = int.MaxValue;
        
        public override float GetValue()
        {
            Cube.Instance.CubeFSM.TryGetState(ECubeState.Fall, out IState<ECubeState> fallState);

            float value = ((State_Cube_Fall)fallState).LastFallDistance * _coef;
            
            value = Mathf.Clamp(value, _minValue, _maxValue);

            return value;
        }

        public override void SetValue(float targetVal)
        {
        }
    }
}