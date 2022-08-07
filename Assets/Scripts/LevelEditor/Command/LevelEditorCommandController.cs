using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class LevelEditorCommandController
    {
        private ELevelEditorCommand _commandType = ELevelEditorCommand.None;

        private Dictionary<KeyCode, bool> _keyCodeMapping = new Dictionary<KeyCode, bool>();

        private Dictionary<int, bool> _mouseButtonMapping = new Dictionary<int, bool>();

        public LevelEditorCommandController(ELevelEditorCommand commandType)
        {
            _commandType = commandType;

            InitCommandMappings();
            
            SettingsSO_LevelEditorCommand.OnCommandSettingsValidated += OnCommandSettingsValidated;
        }

        ~LevelEditorCommandController()
        {
            SettingsSO_LevelEditorCommand.OnCommandSettingsValidated -= OnCommandSettingsValidated;
        }

        public bool Handle(Event e)
        {
            ProcessKeyCodes(e);
            ProcessMouseButtons(e);

            bool res = true;

            foreach (KeyCode keyCode in _keyCodeMapping.Keys)
            {
                res = res && _keyCodeMapping[keyCode];
            }

            foreach (int mouseButton in _mouseButtonMapping.Keys)
            {
                res = res && _mouseButtonMapping[mouseButton];
            }

            return res;
        }

        private void ProcessKeyCodes(Event e)
        {
            if (e.type == EventType.KeyDown && _keyCodeMapping.ContainsKey(e.keyCode))
            {
                _keyCodeMapping[e.keyCode] = true;
            }
            
            if (e.type == EventType.KeyUp && _keyCodeMapping.ContainsKey(e.keyCode))
            {
                _keyCodeMapping[e.keyCode] = false;
            }
        }

        private void ProcessMouseButtons(Event e)
        {
            if (e.type == EventType.MouseDown && _mouseButtonMapping.ContainsKey(e.button))
            {
                _mouseButtonMapping[e.button] = true;
            }
            
            if (e.type == EventType.MouseUp && _mouseButtonMapping.ContainsKey(e.button))
            {
                _mouseButtonMapping[e.button] = false;
            }
        }

        private void OnCommandSettingsValidated()
        {
            InitCommandMappings();
        }

        private void InitCommandMappings()
        {
            LevelEditorContainer.Instance.LevelEditorCommandSO.TryGetCommandSettings(_commandType, out LevelEditorCommandSettings settings);
            
            _keyCodeMapping.Clear();
            _mouseButtonMapping.Clear();
            
            foreach (KeyCode key in settings.KeyCombination)
            {
                _keyCodeMapping.Add(key, false);
            }

            foreach (int mouseButton in settings.MouseButton)
            {
                _mouseButtonMapping.Add(mouseButton, false);
            }
        }
    }
}