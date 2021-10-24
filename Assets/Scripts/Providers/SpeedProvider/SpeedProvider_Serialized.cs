using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
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