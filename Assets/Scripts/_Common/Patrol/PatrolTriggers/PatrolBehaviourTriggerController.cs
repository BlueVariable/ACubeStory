using UnityEngine;
using BLUE.ACubeStory.Common;

namespace BLUE.ACubeStory.Common
{
    public class PatrolBehaviourTriggerController : MonoBehaviour
    {
        [SerializeField] private PatrolBehaviourBase _patrolBehaviour = null;

        private PatrolTriggerBase _patrolTrigger;

        private PatrolTriggerBase _PatrolTrigger
        {
            get
            {
                if (_patrolTrigger == null)
                {
                    _patrolTrigger = GetComponent<PatrolTriggerBase>();
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