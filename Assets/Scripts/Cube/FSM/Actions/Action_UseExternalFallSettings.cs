using BLUE.ActionSystem;
using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_UseExternalFallSettings : ActionBase
    {
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;
        
        public override void Execute()
        {
            Cube.Instance.CubeFSM.TryGetState(ECubeState.Fall, out IState<ECubeState> fallState);

            ((State_Cube_Fall)fallState).UseExternalMoveSettings(_moveSettings);
        }
    }
}