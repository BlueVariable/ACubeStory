using System;
using BLUE.ACubeStory.LoadingScreen;
using BLUE.Extensions;
using BLUE.UISystem;
using BrunoMikoski.AnimationSequencer;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_LoadingScreen : UIBase
    {
        [SerializeField] private AnimationSequencerController[] _idleAnimSequencerControllers = default;

        [SerializeField] private GameObject[] _idleAnimObjects = null;
        
        private EventRaiserArgs_OnLoadingScreenRequested _lastArgs;
        
        private AnimationSequencerController _activeIdleAnimSequencerController;
        
        protected override void AwakeCustomActions()
        {
            EventRaiser_OnLoadingScreenRequested.OnRaisedStatic += OnLoadingScreenUIRequested;
            
            base.AwakeCustomActions();
        }

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            foreach (GameObject idleAnimObject in _idleAnimObjects)
            {
                idleAnimObject.SetActive(_lastArgs.IsSceneTransition);
            }
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void ActivatedCustomActions()
        {
            if (_lastArgs.IsSceneTransition)
            {
                _activeIdleAnimSequencerController = _idleAnimSequencerControllers.GetRandomElement();

                _activeIdleAnimSequencerController.Play();
            }
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            if (_activeIdleAnimSequencerController != null)
            {
                _activeIdleAnimSequencerController.Kill(true);
            }

            base.DeactivatedCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            EventRaiser_OnLoadingScreenRequested.OnRaisedStatic -= OnLoadingScreenUIRequested;

            base.DestroyCustomActions();
        }

        private void OnLoadingScreenUIRequested(object sender, EventRaiserArgs_OnLoadingScreenRequested e)
        {
            _lastArgs = e;
            
            void onActivated()
            {
                e.ResultedCallback?.Invoke(true);
            }
            
            void onDeactivated()
            {
                e.ResultedCallback?.Invoke(true);
            }
            
            if (e.Activation)
            {
                bool canActivate = TryActivate(onActivated);

                if (!canActivate)
                {
                    e.ResultedCallback?.Invoke(false);
                }
            }
            else
            {
                bool canDeactivate = TryDeactivate(onDeactivated);

                if (!canDeactivate)
                {
                    e.ResultedCallback?.Invoke(false);
                }
            }
        }
    }
}