using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_ResetCubeBend : ActionBase
    {
        [SerializeField] private CubeBendController _bendController = null;
        
        public override void Execute()
        {
            _bendController.ResetBend();
        }
    }
}
