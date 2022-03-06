using System;
using System.Linq;
using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Fall : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [Header("Object Settings")]
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private Transform _objectToFall = null;
        [SerializeField] private Vector3 _objectPivotOffset = new Vector3(0, 0.5f, 0);
        public Vector3 ObjectPivotOffset => _objectPivotOffset;

        [Header("Raycast Settings")]
        [SerializeField] private float _maxRayDistance = 100f;
        [SerializeField] private LayerMask _layerMask = default;
        [SerializeField] private float _minValidFallDistance = 0.5f;
        
        [Header("Move Settings")]
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;
        [SerializeField] private float _fallbackFallAmount = 100f;

        [Header("Misc")] 
        [SerializeField] private SimpleConditionBase[] _fallSkipConditions = null;
        
        #region Events

        public Action<RaycastHit> OnFallStartedTowardsHitObject { get; set; }
        public Action OnFallStartedTowardsVoid { get; set; }
        public Action OnFallFinished { get; set; }
        
        public Action OnFallSkipped { get; set; }

        #endregion
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Fall;
        }

        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube transitionMessage)
        {
            if (_fallSkipConditions.Any(val => val.IsConditionSatisfied()))
            {
                _cubeFSM.TryChangeState(ECubeState.Idle);
                
                OnFallSkipped?.Invoke();
                
                return;
            }
            
            if (Physics.Raycast(_objectToFall.position, Vector3.down, 
                out RaycastHit hit, _maxRayDistance, _layerMask))
            {
                if (hit.distance < _minValidFallDistance)
                {
                    _cubeFSM.TryChangeState(ECubeState.Idle);
                    
                    OnFallSkipped?.Invoke();
                    
                    return;
                }
                
                void onFallFinished()
                {
                    OnFallFinished?.Invoke();
                    
                    _cubeFSM.TryChangeState(ECubeState.Idle);
                }

                OnFallStartedTowardsHitObject?.Invoke(hit);
                
                _moveBehaviour.StartMovementTowards(_moveSettings, hit.point + _objectPivotOffset, 
                    onFallFinished);
            }
            else
            {
                void onFallFinished()
                {
                    OnFallFinished?.Invoke();
                    
                    _cubeFSM.TryChangeState(ECubeState.Idle);
                }

                _moveBehaviour.StartMovementTowards(_moveSettings, 
            _objectToFall.position + new Vector3(0, -_fallbackFallAmount, 0), 
                    onFallFinished);
                
                OnFallStartedTowardsVoid?.Invoke();
            }
        }

        protected override void ExitStateCustomActions()
        {
            _moveBehaviour.StopMovement();
            
            base.ExitStateCustomActions();
        }
    }
}