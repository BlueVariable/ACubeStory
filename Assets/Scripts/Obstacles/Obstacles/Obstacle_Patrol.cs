using System.Collections;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Obstacles
{
    public class Obstacle_Patrol : ObstacleBase
    {
        [SerializeField] private Transform[] _patrolPoints = null;

        [SerializeField] private MoveBehaviour_Tween _movementBehaviour = null;

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _movementSettings = default;

        [SerializeField] private float _idleDurationOnReachPatrolPoint = 0.5f;
        
        private IEnumerator _patrolRoutine;
        
        private void Start()
        {
            ActivateObstacle();
        }

        private void OnDestroy()
        {
            DeactivateObstacle();
        }

        private void ActivateObstacle()
        {
            StartPatrolRoutine();
        }

        private void DeactivateObstacle()
        {
            StopPatrolRoutine();
        }

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
            int patrolIndex = 0;
            while (true)
            {
                bool completed = false;
                
                _movementBehaviour
                .StartMovementTowards(
                    _movementSettings,
                    _patrolPoints[patrolIndex % _patrolPoints.Length].position,
            () =>
                       {
                          completed = true;
                       });

                while (!completed)
                {
                    yield return null;
                }

                patrolIndex++;
                
                yield return new WaitForSeconds(_idleDurationOnReachPatrolPoint);
            }
        }
    }
}