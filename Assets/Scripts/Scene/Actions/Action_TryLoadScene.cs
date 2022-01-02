using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Scene
{
    public class Action_TryLoadScene : ActionBase
    {
        [SerializeField] private EScene _sceneType = EScene.Intro;
        
        [SerializeField] private bool _unloadCurActiveScene = true;
        
        public override void Execute()
        {
            SceneManager.Instance.TryLoadSceneByType(_sceneType, _unloadCurActiveScene);
        }
    }
}