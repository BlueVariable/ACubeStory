using System;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class PatrolBehaviour_MoveBehaviourBased : PatrolBehaviourBase
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _movementSettings = default;
        
        protected override void MoveToTargetPoint(PatrolPointBase patrolPoint, Action completedCallback)
        {
            string parentName = Cube.Cube.Instance.transform.parent != null ? Cube.Cube.Instance.transform.parent.gameObject.name : "null";
            
            Debug.Log(parentName);
            
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