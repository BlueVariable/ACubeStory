using BLUE.UISystem;
using TMPro;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_MainMenuButton : UIBase
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro = null;

        [SerializeField] private Material _defaultMaterial = null;

        [SerializeField] private Material _selectedMaterial = null;

        [SerializeField] private GlowBehaviour_CanvasGroupBased _glowBehaviour = null;
        
        public void ButtonSelected()
        {
            _textMeshPro.fontMaterial = _selectedMaterial;

            _glowBehaviour.Activate();
        }

        public void ButtonDeselected()
        {
            _textMeshPro.fontMaterial = _defaultMaterial;
            
            _glowBehaviour.Deactivate();
        }
    }
}