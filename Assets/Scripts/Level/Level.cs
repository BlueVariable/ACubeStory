using UnityEngine;

namespace BLUE.ACubeStory.Level
{
    public class Level : MonoBehaviour
    {
        #region Singleton

        private static Level _instance;
        public static Level Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Level>();
                }

                return _instance;
            }
        }

        #endregion

        private FSM_Level _LevelFSM;

        public FSM_Level LevelFSM
        {
            get
            {
                if (_LevelFSM == null)
                {
                    _LevelFSM = GetComponentInChildren<FSM_Level>();
                }

                return _LevelFSM;
            }
        }
    }
}