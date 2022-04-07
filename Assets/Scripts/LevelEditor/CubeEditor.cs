using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeEditor : MonoBehaviour
    {
        [SerializeField] private ACubeStory.Level.Level _level = null;

        public ACubeStory.Level.Level Level
        {
            get => _level;

            set => _level = value;
        }
    }
}