using System;
using System.Collections;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Fail;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeFailController : MonoBehaviour, IFailController
    {
        #region Singleton

        private static CubeFailController _instance;
        public static CubeFailController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CubeFailController>();
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private ActionExecutor _failTriggeredActionExecutor = null;

        [SerializeField] private TriggerBase[] _triggersToBlockOnFail = default;

        [SerializeField] private float _triggerBlockDuration = 0.25f;
        
        public Trigger_CubeFailBase LastCubeFailTrigger { get; private set; }

        private IEnumerator _triggerReactivationRoutine;
        
        private readonly List<Trigger_CubeFailBase> _cubeFailTriggers = new();
        
        #region Events

        public Action<IFailController> OnFailStarted { get; set; }
        public Action<IFailController> OnFailed { get; set; }
        
        #endregion

        public void RegisterTrigger(Trigger_CubeFailBase cubeFailTrigger)
        {
            if (!_cubeFailTriggers.Contains(cubeFailTrigger))
            {
                _cubeFailTriggers.Add(cubeFailTrigger);

                cubeFailTrigger.OnFailStarted += OnFailTriggerStarted;
                
                cubeFailTrigger.OnTriggered += OnTriggered;
            }
        }

        public void UnregisterTrigger(Trigger_CubeFailBase cubeFailTrigger)
        {
            _cubeFailTriggers.Remove(cubeFailTrigger);

            cubeFailTrigger.OnFailStarted -= OnFailTriggerStarted;
            
            cubeFailTrigger.OnTriggered -= OnTriggered;
        }

        private void OnFailTriggerStarted(Trigger_CubeFailBase cubeFailTrigger)
        {
            LastCubeFailTrigger = cubeFailTrigger;

            Array.ForEach(_triggersToBlockOnFail, val => val.TryDeactivate());

            OnFailStarted?.Invoke(this);
        }

        private void Awake()
        {
            FailManager.Instance.RegisterFailController(this);
        }

        private void OnDestroy()
        {
            if (FailManager.Instance != null)
            {
                FailManager.Instance.UnregisterFailController(this);
            }

            StopTriggerReactivationRoutine();
        }

        private void OnTriggered(TriggerBase failTrigger)
        {
            _failTriggeredActionExecutor.Execute();

            OnFailed?.Invoke(this);

            StartTriggerReactivationRoutine();
        }

        private void StartTriggerReactivationRoutine()
        {
            StopTriggerReactivationRoutine();

            _triggerReactivationRoutine = TriggerReactivationProgress();

            StartCoroutine(_triggerReactivationRoutine);
        }

        private void StopTriggerReactivationRoutine()
        {
            if (_triggerReactivationRoutine != null)
            {
                StopCoroutine(_triggerReactivationRoutine);
            }
        }

        private IEnumerator TriggerReactivationProgress()
        {
            yield return new WaitForSeconds(_triggerBlockDuration);
            
            Array.ForEach(_triggersToBlockOnFail, val => val.TryActivate());
        }
    }
}