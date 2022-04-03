using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
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
        
        private Level _curLevel;

        public Level CurLevel
        {
            get
            {
                if (_curLevel == null)
                {
                    InitCurLevel();
                }

                return _curLevel;
            }
        }

        private ILevelIDProvider _levelIDProvider;

        private ILevelIDProvider _LevelIDProvider
        {
            get
            {
                if (_levelIDProvider == null)
                {
                    _levelIDProvider = GetComponent<ILevelIDProvider>();
                }

                return _levelIDProvider;
            }
        }

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void InitCurLevel()
        {
            string levelID = _LevelIDProvider.GetLevelID();

            GameObject gameObject = new GameObject();
            gameObject.transform.parent = transform;
            gameObject.name = levelID;

            Level level = gameObject.AddComponent<Level>();
            level.InitLevel(levelID);

            _curLevel = level;
        }
    }
}