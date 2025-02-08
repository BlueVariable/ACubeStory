using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class FloatProvider_CubeLastFallDistance : FloatProviderBase
    {
        [SerializeField] private State_Cube_Fall _fallState = null;

        [SerializeField] private float _coef = 1;
        
        public override float GetValue()
        {
            return _fallState.LastFallDistance * _coef;
        }

        public override void SetValue(float targetVal)
        {
        }
    }
}