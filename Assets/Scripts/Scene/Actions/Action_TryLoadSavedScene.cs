using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Scene
{
    public class Action_TryLoadSavedScene : ActionBase
    {
        public override void Execute()
        {
            if (SceneManager.Instance.InitialSceneProvider is SavedSceneProvider savedSceneProvider)
            {
                if (savedSceneProvider.TryGetSavedSceneType(out EScene savedSceneType))
                {
                    SceneManager.Instance.TryLoadSceneByType(savedSceneType, true);

                    return;
                }
            }
            
            SceneManager.Instance.TryLoadNextScene(true);
        }
    }
}