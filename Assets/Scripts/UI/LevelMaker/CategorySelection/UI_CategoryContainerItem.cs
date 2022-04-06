using System;
using BLUE.ACubeStory.LevelMaker;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_CategoryContainerItem : UIBase
    {
        [SerializeField] private UI_Text _categoryNameText = null;
        
        public void InitItem(TileContainer tileContainer, Transform parent)
        {
            _categoryNameText.Text = tileContainer.ContainerName;
            
            transform.SetParent(parent);

            transform.localRotation = Quaternion.identity;
            
            _categoryNameText.TryActivate();
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            _categoryNameText.TryDeactivate();

            base.DeactivatingCustomActions(onCompleted);
        }
    }
}