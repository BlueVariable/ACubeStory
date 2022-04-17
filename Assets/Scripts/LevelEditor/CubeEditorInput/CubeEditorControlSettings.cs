using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [Serializable]
    public class CubeEditorControlSettings
    {
        [SerializeField] private ECubeEditorControlType _controlType = ECubeEditorControlType.None;
        
        [SerializeField] private List<KeyCode> _keyCombination = new List<KeyCode>();

        [SerializeField] private List<int> _mouseButton = new List<int>();

        public ECubeEditorControlType ControlType => _controlType;

        public List<KeyCode> KeyCombination => _keyCombination;

        public List<int> MouseButton => _mouseButton;
    }
}