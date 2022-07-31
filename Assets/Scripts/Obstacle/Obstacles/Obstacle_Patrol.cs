using System.Collections;
using BLUE.ACubeStory.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Obstacle
{
    public class Obstacle_Patrol : ObstacleBase
    {
        [SerializeField] private PatrolBehaviourBase _patrolBehaviour = null;
        
        [SerializeField] private float _idleDurationOnPatrolPointReached = 0.5f;
        
        private IEnumerator _patrolRoutine;
        
        private void StartPatrolRoutine()
        {
            StopPatrolRoutine();
            
            _patrolRoutine = PatrolProgress();

            StartCoroutine(_patrolRoutine);
        }

        private void StopPatrolRoutine()
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