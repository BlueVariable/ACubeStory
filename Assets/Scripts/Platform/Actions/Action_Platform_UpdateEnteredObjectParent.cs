using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Platform.Task
{
    public class Action_Platform_UpdateEnteredObjectParent : ActionBase
    {
        [SerializeField] private PlatformObjectContainer _platformObjectContainer = null;

        [SerializeField] private Transform _newParent = null;
        
        public override void Execute()
        {
            _platformObjectContainer.LastEnteredObject.SetParent(_newParent);
        }
    }
}