using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class SpeedProvider_Serialized : SpeedProviderBase
    {
        [SerializeField] private float _speed = 0f;
        
        public override float GetSpeed()
        {
            return _speed;
        }
    }
}