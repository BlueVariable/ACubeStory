using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class Test_SceneTransition : MonoBehaviour
    {
        [SerializeField] private KeyCode _targetKey = KeyCode.K;

        private void Update()
        { 
            if (Input.GetKeyDown(_targetKey))
            {
                SceneTransitionController sceneTransitionController = FindObjectOfType<SceneTransitionController>();

                sceneTransitionController.TriggerSceneTransition();
            }
        }
    }
}