using UnityEngine;
using BLUE.ACubeStory.Common;

namespace BLUE.ACubeStory.Platform
{
    public class PlatformPatrolController : MonoBehaviour
    {
        [SerializeField] private PatrolBehaviourBase _patrolBehaviour = null;

        private PlatformPatrolTriggerBase _patrolTrigger;

        private PlatformPatrolTriggerBase _PatrolTrigger
        {
            get
            {
                if (_patrolTrigger == null)
                {
                    _patrolTrigger = GetComponent<PlatformPatrolTriggerBase>();
                }

                return _patrolTrigger;
            }
        }

        private void Awake()
        {
            RegisterToPatrolTrigger();
        }

        private void OnDestroy()
        {
            UnregisterFromPatrolTrigger();
        }

        private void RegisterToPatrolTrigger()
        {
            _PatrolTrigger.OnPatrolTriggered += OnPatrolTriggered;
        }

        private void UnregisterFromPatrolTrigger()
        {
            _PatrolTrigger.OnPatrolTriggered -= OnPatrolTriggered;
        }

        private void OnPatrolTriggered()
        {
            _patrolBehaviour.GoToNextPatrolPoint();
        }
    }
}