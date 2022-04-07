using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class LevelMaker : MonoBehaviour
    {
        private static LevelMaker _instance;

        public static LevelMaker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LevelMaker>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void InitCurLevel()
        {
            /*string levelID = _LevelIDProvider.GetLevelID();

            GameObject gameObject = new GameObject();
            gameObject.transform.parent = transform;
            gameObject.name = levelID;

            Level level = gameObject.AddComponent<Level>();
            level.InitLevel(levelID);

            _curLevel = level;*/
        }
    }
}