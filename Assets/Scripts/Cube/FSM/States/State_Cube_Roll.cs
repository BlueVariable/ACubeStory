using BLUE.ACubeStory.Common;
using BLUE.ACubeStory.Cube.Ability;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Roll : StateBase<ECubeState, FSMTransitionMessage_Cube_Roll>
    {
        [Header("Object Settings")]
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private Transform _objectToRoll = null;
        
        [Header("Raycast Settings")]
        [SerializeField] private float _rollRayDistance = 1;
        [SerializeField] private LayerMask _layerMask = default;
        
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;

        [SerializeField] private Ability_Cube_Dash _dashAbility = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Roll;
        }
        
        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            _rollBehaviourProvider.GetRollBehaviour().TryRollTo(transitionMessage.Direction, onRolledCallback: onCompleted);

            void onCompleted()
            {
                _cubeFSM.TryChangeState(ECubeState.Fall);
            }
        }

        protected override void ExitStateCustomActions()
        {
            _rollBehaviourProvider.GetRollBehaviour().TryPauseRoll();
            
            base.ExitStateCustomActions();
        }

        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            if (_dashAbility.ListeningInputForDash)
            {
                return false;
            } 
            
            return !Physics.Raycast(_objectToRoll.position, transitionMessage.Direction,
                out RaycastHit hit, _rollRayDistance, _layerMask);
        }
    }
}