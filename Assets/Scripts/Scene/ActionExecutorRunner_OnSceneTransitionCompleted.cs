using BLUE.ACubeStory.Scene;
using UnityEngine;

namespace BLUE.ActionSystem
{
    public class ActionExecutorRunner_OnSceneTransitionCompleted : MonoBehaviour
    {
        [SerializeField] private ActionExecutor _actionExecutor = null;

        private void Awake()
        {
            if (!SceneTransitionController.Instance.IsTransitioning)
            {
                _actionExecutor.Execute();

                return;
            }

            SceneTransitionController.Instance.OnSceneTransitionCompleted += OnSceneTransitionCompleted;
        }

        private void OnDestroy()
        {
            SceneTransitionController.Instance.OnSceneTransitionCompleted -= OnSceneTransitionCompleted;

            _actionExecutor.Interrupt();
        }

        private void OnSceneTransitionCompleted()
        {
            SceneTransitionController.Instance.OnSceneTransitionCompleted -= OnSceneTransitionCompleted;

            _actionExecutor.Execute();
        }
    }
}
