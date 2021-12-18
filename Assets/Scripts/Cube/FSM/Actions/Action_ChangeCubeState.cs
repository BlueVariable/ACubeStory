using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_ChangeCubeState : ActionBase
    {
        [SerializeField] private ECubeState _cubeState = ECubeState.None;
        
        public override void Execute()
        {
            Cube.Instance.CubeFSM.TryChangeState(_cubeState);
        }
    }
}