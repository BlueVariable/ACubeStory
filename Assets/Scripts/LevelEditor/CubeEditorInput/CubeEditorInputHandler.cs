using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeEditorInputHandler
    {
        private ECubeEditorControlType _controlType = ECubeEditorControlType.None;

        private Dictionary<KeyCode, bool> _keycodes = new Dictionary<KeyCode, bool>();

        private Dictionary<int, bool> _mouseButtons = new Dictionary<int, bool>();

        public CubeEditorInputHandler(ECubeEditorControlType controlType)
        {
            _controlType = controlType;

            CubeEditor.Instance.InputSo.TryGetControlSettings(_controlType, out CubeEditorControlSettings settings);
            
            foreach (KeyCode key in settings.KeyCombination)
            {
                _keycodes.Add(key, false);
            }

            foreach (int mouseButton in settings.MouseButton)
            {
                _mouseButtons.Add(mouseButton, false);
            }
        }

        public bool Handle(Event e)
        {
            if (e.type == EventType.KeyDown && _keycodes.ContainsKey(e.keyCode))
            {
                _keycodes[e.keyCode] = true;
            }
            
            if (e.type == EventType.KeyUp && _keycodes.ContainsKey(e.keyCode))
            {
                _keycodes[e.keyCode] = false;
            }
            
            if (e.type == EventType.MouseDown && _mouseButtons.ContainsKey(e.button))
            {
                _mouseButtons[e.button] = true;
            }
            
            if (e.type == EventType.MouseUp && _mouseButtons.ContainsKey(e.button))
            {
                _mouseButtons[e.button] = false;
            }

            bool res = true;

            foreach (KeyCode keyCode in _keycodes.Keys)
            {
                res = res && _keycodes[keyCode];
            }

            foreach (int mouseButton in _mouseButtons.Keys)
            {
                res = res && _mouseButtons[mouseButton];
            }

            return res;
        }
    }
}