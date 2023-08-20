using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Scene
{
    public class Action_TriggerSceneTransition : ActionBase
    {
        public override void Execute()
        {
            SceneTransitionController.Instance.TriggerSceneTransition();
        }
    }
}