using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Platform.Task
{
    public class Action_Platform_UpdateExitedObjectParent : ActionBase
    {
        [SerializeField] private PlatformObjectContainer _platformObjectContainer = null;

        [SerializeField] private Transform _newParent = null;
        
        public override void Execute()
        {
            _platformObjectContainer.LastExitedObject.SetParent(_newParent);
            
            Debug.Log($"Action Platform Update Exited Object Parent {Time.frameCount}");
        }
    }
}