using BLUE.ACubeStory.LoadingScreen;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class SceneTransitionController : MonoBehaviour
    {
        [SerializeField] private TriggerBase _transitionTrigger = null;

        private readonly EventRaiser_OnLoadingScreenRequested _loadingScreenRequestor = new();
        
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
                new EventRaiserArgs_OnLoadingScreenRequested(true, OnLoadingScreenResulted));
        }
        
        private void OnLoadingScreenResulted(bool result)
        {
            RegisterToSceneLoad();
            
            SceneManager.Instance.TryLoadNextScene(true);
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
            
            _loadingScreenRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenRequested(false));
        }
    }
}