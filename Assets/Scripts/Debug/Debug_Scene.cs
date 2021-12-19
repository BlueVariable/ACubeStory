using BLUE.ACubeStory.Scene;
using UnityEngine;

namespace BLUE.ACubeStory.Debug
{
    public class Debug_Scene : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.Instance.TryLoadNextScene(true);
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.Instance.TryReloadCurScene();
            }
        }
    }
}