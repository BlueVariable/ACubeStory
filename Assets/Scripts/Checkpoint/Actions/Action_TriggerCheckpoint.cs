using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Checkpoint
{
    public class Action_TriggerCheckpoint : ActionBase
    {
        [SerializeField] private Checkpoint _checkpoint = null;

        public override void Execute()
        {
            _checkpoint.TriggerCheckpoint();
        }
    }
}