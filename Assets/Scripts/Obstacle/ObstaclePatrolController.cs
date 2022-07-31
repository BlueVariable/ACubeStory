using System.Collections;
using BLUE.ACubeStory.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Obstacle
{
    public class ObstaclePatrolController : MonoBehaviour
    {
        [SerializeField] private PatrolBehaviourBase _patrolBehaviour = null;
        
        [SerializeField] private float _idleDurationOnPatrolPointReached = 0.5f;
        
        private IEnumerator _patrolRoutine;
        
        public void StartPatrolling()
        {
            StopPatrolling();
            
            _patrolRoutine = PatrolProgress();

            StartCoroutine(_patrolRoutine);
        }

        public void StopPatrolling()
        {
            if (_patrolRoutine != null)
            {
                StopCoroutine(_patrolRoutine);
            }
        }

        private IEnumerator PatrolProgress()
        {
            while (true)
            {
                _patrolBehaviour.GoToNextPatrolPoint();

                yield return new WaitUntil(() => _patrolBehaviour.IsPatrolling);

                yield return new WaitForSeconds(_idleDurationOnPatrolPointReached);
            }
        }
    }
}