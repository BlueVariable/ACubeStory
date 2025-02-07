using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_ResetCubeStretch : ActionBase
    {
        [SerializeField] private CubeStretchController _stretchController = null;
        
        public override void Execute()
        {
            _stretchController.ResetStretch();
        }
    }
}
