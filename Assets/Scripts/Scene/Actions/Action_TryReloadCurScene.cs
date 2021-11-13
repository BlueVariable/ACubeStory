using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Scene
{
    public class Action_TryReloadCurScene : ActionBase
    {
        public override void Execute()
        {
            SceneManager.Instance.TryReloadCurScene();
        }
    }
}
