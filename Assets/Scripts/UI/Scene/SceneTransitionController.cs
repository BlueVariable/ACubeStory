using BLUE.ACubeStory.Scene;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class SceneTransitionController : MonoBehaviour
    {
        [SerializeField] private TriggerBase _transitionTrigger = null;

        private readonly EventRaiser_OnLoadingScreenUIRequested _loadingScreenUIRequestor = new();
        
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

            _loadingScreenUIRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenUIRequested(true, OnLoadingScreenResulted));
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
            
            _loadingScreenUIRequestor.Raise(
                new EventRaiserArgs_OnLoadingScreenUIRequested(false));
        }
    }
}