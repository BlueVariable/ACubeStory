using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class Action_StartFallingObjectSpawner : ActionBase
    {
        [SerializeField] private FallingObjectSpawnerBehaviour _fallingObjectSpawnerBehaviour = null;

        [SerializeField] private FallingObjectSpawnerBehaviour.FallSettings _fallSettings = null;
        
        public override void Execute()
        {
            _fallingObjectSpawnerBehaviour.StartFalling(_fallSettings);
        }
    }
}