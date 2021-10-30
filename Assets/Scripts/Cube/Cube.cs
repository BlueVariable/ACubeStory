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

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}