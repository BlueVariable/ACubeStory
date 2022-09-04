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

        [SerializeField] private RotationBehaviour_Tween _rotationBehaviour = null;
        
        [SerializeField] private RotationBehaviour_Tween.RotationSettings _rotationSettings = default;
        
        [SerializeField] private bool _updateRotation = false;
        
        [SerializeField] private float _delayInSeconds = 0.125f;
        
        protected override async void MoveToTargetPoint(PatrolPointBase patrolPoint, Action completedCallback)
        {
            await Task.Delay((int) (_delayInSeconds * 1000));
            
            _moveBehaviour.StartMovementTowards(
                _movementSettings,
                patrolPoint.GetPatrolPointPosition(),
                () =>
                    {
                        completedCallback?.Invoke();
                    });

            if (!_updateRotation)
            {
                return;
            }

            _rotationBehaviour.StartRotation(
                _rotationSettings,
                patrolPoint.GetPatrolPointRotation());
        }

        protected override bool TryStopMoving()
        {
            _moveBehaviour.StopMovement();

            if (_updateRotation)
            {
                _rotationBehaviour.StopRotation();
            }

            return true;
        }
    }
}