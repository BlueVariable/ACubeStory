using System;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Fail;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeFailController : MonoBehaviour, IFailController
    {
        public enum ECubeFail
        {
            None = 0,
        
            Fall_Void = 1,
            Fall_Water = 2,
            
            Trap_Obstacle = 10,
        }

        [Serializable]
        private class CubeFailTriggerInfo
        {
            [SerializeField] private ECubeFail _cubeFailType = default;
            public ECubeFail CubeFailType => _cubeFailType;

            [SerializeField] private TriggerBase[] _failTriggers = default;
            public TriggerBase[] FailTriggers => _failTriggers;
        }

        [SerializeField] private CubeFailTriggerInfo[] _cubeFailTriggerMapping = default;

        [SerializeField] private ActionExecutor _failTriggeredActionExecutor = null;
        
        #region Events

        public Action<IFailController> OnFailed { get; set; }

        public Action<ECubeFail, TriggerBase> OnFailTriggered { get; set; }

        #endregion
        
        private void Awake()
        {
            RegisterToFailTriggers();

            FailManager.Instance.RegisterFailController(this);
        }

        private void OnDestroy()
        {
            UnregisterFromFailTriggers();

            if (FailManager.Instance != null)
            {
                FailManager.Instance.UnregisterFailController(this);
            }
        }

        private void RegisterToFailTriggers()
        {
            foreach (CubeFailTriggerInfo info in _cubeFailTriggerMapping)
            {
                foreach (TriggerBase failTrigger in info.FailTriggers)
                {
                    failTrigger.OnTriggered += OnTriggered;
                }
            }
        }

        private void UnregisterFromFailTriggers()
        {
            foreach (CubeFailTriggerInfo info in _cubeFailTriggerMapping)
            {
                foreach (TriggerBase failTrigger in info.FailTriggers)
                {
                    failTrigger.OnTriggered -= OnTriggered;
                }
            }
        }

        private void OnTriggered(TriggerBase failTrigger)
        {
            CubeFailTriggerInfo info = _cubeFailTriggerMapping.FirstOrDefault(val 
                => val.FailTriggers.Contains(failTrigger));
            
            _failTriggeredActionExecutor.Execute();

            OnFailTriggered?.Invoke(info.CubeFailType, failTrigger);

            OnFailed?.Invoke(this);
        }
    }
}