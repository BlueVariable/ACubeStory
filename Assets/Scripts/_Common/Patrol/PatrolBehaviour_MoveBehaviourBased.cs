using System;
using BLUE.Common;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace BLUE.ACubeStory.Common
{
    public class PatrolBehaviour_MoveBehaviourBased : PatrolBehaviourBase
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _movementSettings = default;

        [SerializeField] private float _delayInSeconds = 0.125f;
        
        protected override async void MoveToTargetPoint(PatrolPointBase patrolPoint, Action completedCallback)
        {
            string parentName = Cube.Cube.Instance.transform.parent != null ? Cube.Cube.Instance.transform.parent.gameObject.name : "null";
            
            Debug.Log(parentName);

            await Task.Delay((int) (_delayInSeconds * 1000));
            
            _moveBehaviour.StartMovementTowards(
                _movementSettings,
                patrolPoint.GetPatrolPointPosition(),
                () =>
                    {
                        completedCallback?.Invoke();
                    });
        }

        protected override bool TryStopMoving()
        {
            _moveBehaviour.StopMovement();

            return true;
        }
    }
}