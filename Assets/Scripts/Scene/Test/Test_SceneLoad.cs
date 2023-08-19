using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class Test_SceneLoad : MonoBehaviour
    {
        [SerializeField] private KeyCode _loadNextSceneKey = KeyCode.N;
        [SerializeField] private KeyCode _reloadCurSceneKey = KeyCode.Backspace;

        private void Update()
        {
            if (Input.GetKeyDown(_loadNextSceneKey))
            {
                SceneManager.Instance.TryLoadNextScene(true);
            }
            else if (Input.GetKeyDown(_reloadCurSceneKey))
            {
                SceneManager.Instance.TryReloadCurScene();
            }
        }
    }
}