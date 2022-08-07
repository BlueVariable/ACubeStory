using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [Serializable]
    public class LevelEditorCommandSettings
    {
        [SerializeField] private ELevelEditorCommand _commandType = ELevelEditorCommand.None;
        public ELevelEditorCommand CommandType => _commandType;
        
        [SerializeField] private List<KeyCode> _keyCombination = new List<KeyCode>();
        public List<KeyCode> KeyCombination => _keyCombination;

        [SerializeField] private List<int> _mouseButton = new List<int>();
        public List<int> MouseButton => _mouseButton;
    }
}