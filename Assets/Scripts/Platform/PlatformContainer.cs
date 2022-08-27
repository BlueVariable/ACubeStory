using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public class PlatformContainer : MonoBehaviour
    {
        private static PlatformContainer _instance;
        public static PlatformContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlatformContainer>();
                }

                return _instance;
            }
        }
        
        [SerializeField] private Platform[] _platforms = null;
        public Platform[] Platforms => _platforms;
    }
} 