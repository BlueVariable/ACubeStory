using System;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_LevelMaker : UIBase
    {
        [SerializeField] private UI_TileSelection _tileSelection = null;

        protected override void ActivatedCustomActions()
        {
            _tileSelection.TryActivate();
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            _tileSelection.TryDeactivate();
            
            base.DeactivatingCustomActions(onCompleted);
        }
    }
}