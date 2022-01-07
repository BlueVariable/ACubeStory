using UnityEngine;
using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Idle : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [SerializeField] private LayerMask _groundLayerMask = default;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Idle;
        }
    }
}