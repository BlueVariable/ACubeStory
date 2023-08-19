using System.Collections.Generic;
using BLUE.ACubeStory.Cube;
using BLUE.ACubeStory.Fail;
using BLUE.ACubeStory.LoadingScreen;
using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class CubeFailTransitionController : MonoBehaviour
    {
        private readonly Dictionary<ECubeFail, float> _failTypeDelayMapping = new()
        {
            {ECubeFail.Fall_Void, 0.5f}
        };
        
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
            CubeFailController cubeFailController = failController as CubeFailController;

            ECubeFail cubeFailType = cubeFailController.LastCubeFailTrigger.CubeFailType;
            
            if (_failTypeDelayMapping.TryGetValue(cubeFailType, out float delay))
            {
                CoroutineHelper.Instance.WaitForSeconds(delay, onPreFailActionsCompleted);
            }
            else
            {
                onPreFailActionsCompleted();
            }

            void onPreFailActionsCompleted()
            {
                _loadingScreenRequestor.Raise(
                    new EventRaiserArgs_OnLoadingScreenRequested(true, false));
            }
        }
        
        private void OnFailed(IFailController failController)
        {
            _loadingScreenRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenRequested(false, false));
        }
    }
}