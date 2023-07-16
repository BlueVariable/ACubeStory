using BLUE.ACubeStory.Cube;
using BLUE.ACubeStory.Fail;
using BLUE.ACubeStory.LoadingScreen;
using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class CubeFailTransitionController : MonoBehaviour
    {
        private readonly EventRaiser_OnLoadingScreenRequested _loadingScreenRequestor = new();
        
        private void Awake()
        {
            RegisterToCubeFailController();
        }

        private void OnDestroy()
        {
            UnregisterFromFailController();
        }

        private void RegisterToCubeFailController()
        {
            CubeFailController.Instance.OnFailStarted += OnFailStarted;
            CubeFailController.Instance.OnFailed += OnFailed;
        }

        private void UnregisterFromFailController()
        {
            CubeFailController.Instance.OnFailStarted -= OnFailStarted;
            CubeFailController.Instance.OnFailed -= OnFailed;
        }

        private void OnFailStarted(IFailController failController)
        {
            _loadingScreenRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenRequested(true));
        }
        
        private void OnFailed(IFailController failController)
        {
            _loadingScreenRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenRequested(false));
        }
    }
}