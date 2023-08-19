using System.Collections;
using BLUE.ACubeStory.LoadingScreen;
using BLUE.Helpers;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class SceneTransitionController : MonoBehaviour
    {
        [SerializeField] private TriggerBase _transitionTrigger = null;

        [SerializeField] private float _forcedDuration = 4f;

        private IEnumerator _transitionRoutine;

        private bool _sceneLoadCompleted = false;
        
        private readonly EventRaiser_OnLoadingScreenRequested _loadingScreenRequestor = new();
        
        public void ForceTransition()
        {
            OnTriggered(null);
        }
        
        private void Awake()
        {
            RegisterToTrigger();
        }

        private void OnDestroy()
        {
            UnregisterFromTrigger();
        }

        private void RegisterToTrigger()
        {
            _transitionTrigger.OnTriggered += OnTriggered;
        }

        private void UnregisterFromTrigger()
        {
            _transitionTrigger.OnTriggered -= OnTriggered;
        }

        private void OnTriggered(TriggerBase trigger)
        {
            UnregisterFromTrigger();

            _loadingScreenRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenRequested(true, true,
                    OnLoadingScreenResulted));
        }
        
        private void OnLoadingScreenResulted(bool result)
        {
            StartTransitionRoutine();
        }
        
        private void RegisterToSceneLoad()
        {
            SceneManager.Instance.OnPostSceneLoad += OnPostSceneLoad;
        }

        private void UnregisterFromSceneLoad()
        {
            SceneManager.Instance.OnPostSceneLoad -= OnPostSceneLoad;
        }

        private void OnPostSceneLoad(EScene sceneType)
        {
            UnregisterFromSceneLoad();

            _sceneLoadCompleted = true;
        }

        private void StartTransitionRoutine()
        {
            StopTransitionRoutine();

            _transitionRoutine = TransitionProgress();

            CoroutineHelper.Instance.StartCoroutine(_transitionRoutine);

        }

        private void StopTransitionRoutine()
        {
            if (_transitionRoutine != null)
            {
                CoroutineHelper.Instance.StopCoroutine(_transitionRoutine);
            }
        }

        private IEnumerator TransitionProgress()
        {
            _sceneLoadCompleted = false;
            
            RegisterToSceneLoad();
            
            //SceneManager.Instance.TryLoadNextScene(true);
            
            SceneManager.Instance.TryReloadCurScene();
            
            yield return new WaitForSeconds(_forcedDuration);

            while (!_sceneLoadCompleted)
            {
                yield return null;
            }
            
            _loadingScreenRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenRequested(false, true));
        }
    }
}