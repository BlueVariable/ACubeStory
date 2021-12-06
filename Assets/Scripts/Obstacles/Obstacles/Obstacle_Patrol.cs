using System.Collections;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Obstacles
{
    public class Obstacle_Patrol : ObstacleBase
    {
        [SerializeField] private Transform[] _patrolPoints = null;

        [SerializeField] private MoveBehaviour_Tween _movementBehaviour = null;
        
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
            
        }

        private void StopPatrolRoutine()
        {
            
        }

        private IEnumerator PatrolProgress()
        {
            yield break;
        }
    }
}