using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class LevelIDProvider_Serialized : MonoBehaviour,
        ILevelIDProvider
    {
        [SerializeField] private string _defaultLevelID = "Level";
        
        public string GetLevelID()
        {
            return _defaultLevelID;
        }
    }
}