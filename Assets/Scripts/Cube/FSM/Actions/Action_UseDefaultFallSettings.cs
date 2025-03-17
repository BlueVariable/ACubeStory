using BLUE.ActionSystem;
using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_UseDefaultFallSettings : ActionBase
    {
        public override void Execute()
        {
            Cube.Instance.CubeFSM.TryGetState(ECubeState.Fall, out IState<ECubeState> fallState);

            ((State_Cube_Fall)fallState).UseDefaultMoveSettings();
        }
    }
}