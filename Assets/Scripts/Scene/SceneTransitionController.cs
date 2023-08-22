using System;
using System.Collections;
using BLUE.ACubeStory.LoadingScreen;
using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class SceneTransitionController : MonoBehaviour
    {
        #region Singleton

        private static SceneTransitionController _instance;
        public static SceneTransitionController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SceneTransitionController>();
                }

                return _instance;
            }
        }

        #endregion
        
        [SerializeField] private float _forcedDuration = 4f;

        public bool IsTransitioning { get; private set; } = false;
        
        private IEnumerator _transitionRoutine;

        private bool _sceneLoadCompleted = false;
        
        private readonly EventRaiser_OnLoadingScreenRequested _loadingScreenRequestor = new();

        #region Events

        public Action OnSceneTransitionCompleted { get; set; }

        #endregion
        
        public void TriggerSceneTransition()
        {
            OnTriggered();
        }
        
        private void OnTriggered()
        {
            IsTransitioning = true;

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
            
            IsTransitioning = true;

            _transitionRoutine = TransitionProgress();

            CoroutineHelper.Instance.StartCoroutine(_transitionRoutine);

        }

        private void StopTransitionRoutine()
        {
            if (_transitionRoutine != null)
            {
                CoroutineHelper.Instance.StopCoroutine(_transitionRoutine);
            }
            
            IsTransitioning = false;
        }

        private IEnumerator TransitionProgress()
        {
            _sceneLoadCompleted = false;
            
            RegisterToSceneLoad();
            
            SceneManager.Instance.TryLoadNextScene(true);

            yield return new WaitForSeconds(_forcedDuration);

            while (!_sceneLoadCompleted)
            {
                yield return null;
            }

            void onLoadingScreenResulted(bool result)
            {
                IsTransitioning = false;

                OnSceneTransitionCompleted?.Invoke();
            }
                        
            _loadingScreenRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenRequested(false, true, onLoadingScreenResulted));

        }
    }
}