using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Cube : MonoBehaviour
    {
        #region Singleton

        private static Cube _instance;
        public static Cube Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Cube>();
                }

                return _instance;
            }
        }

        #endregion

        private FSM_Cube _cubeFSM;

        public FSM_Cube CubeFSM
        {
            get
            {
                if (_cubeFSM == null)
                {
                    _cubeFSM = GetComponentInChildren<FSM_Cube>();
                }

                return _cubeFSM;
            }
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}