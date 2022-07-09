using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Moth
{
    public class MothPackAscendingController : MonoBehaviour
    {
        [SerializeField] private ActionExecutorRunner_OnAwake _targetAscendingActionExecutorRunner = null;

        private EventRaiser_OnMothPackAscendingCompleted _mothPackAscendedCompletedEventRaiser =
            new EventRaiser_OnMothPackAscendingCompleted();
        
        #region Events

        public Action OnAscendingCompleted { get; set; }

        #endregion

        public bool IsCompleted()
        {
            return !_targetAscendingActionExecutorRunner.IsExecuting();
        }

        private void Awake()
        {
            RegisterToActionExecutor();
        }

        private void OnDestroy()
        {
            UnregisterFromActionExecutor();
        }

        private void RegisterToActionExecutor()
        {
            _targetAscendingActionExecutorRunner.ActionExecutor.OnExecutionCompleted += OnExecutionCompleted;
        }
        
        private void UnregisterFromActionExecutor()
        {
            _targetAscendingActionExecutorRunner.ActionExecutor.OnExecutionCompleted -= OnExecutionCompleted;
        }

        private void OnExecutionCompleted()
        {
            OnAscendingCompleted?.Invoke();

            _mothPackAscendedCompletedEventRaiser.Raise(new EventRaiserArgs_OnMothPackAscendingCompleted(this));
        }
    }
}