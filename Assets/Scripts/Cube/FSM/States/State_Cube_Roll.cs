using BLUE.ACubeStory.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Roll : StateBase<ECubeState, FSMTransitionMessage_Cube_Roll>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Roll;
        }

        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            _rollBehaviourProvider.GetRollBehaviour().TryRollTo(transitionMessage.Direction, onCompleted);

            void onCompleted()
            {
                _cubeFSM.TryChangeState(ECubeState.Fall);
            }
        }
    }
}