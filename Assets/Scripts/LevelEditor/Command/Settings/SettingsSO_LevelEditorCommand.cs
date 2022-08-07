using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CreateAssetMenu(fileName = "SettingsSO_LevelEditorCommand", menuName = "BLUE/Settings/Level Editor/Create SettingsSO_LevelEditorCommand")]
    public class SettingsSO_LevelEditorCommand : ScriptableObject
    {
        [SerializeField] private List<LevelEditorCommandSettings> _commandSettingsColl = new List<LevelEditorCommandSettings>();

        #region Events

        public static Action OnCommandSettingsValidated { get; set; }

        #endregion

        public bool TryGetCommandSettings(ELevelEditorCommand commandType, out LevelEditorCommandSettings settings)
        {
            settings = _commandSettingsColl.SingleOrDefault(i => i.CommandType == commandType);

            return settings != null;
        }

        private void OnValidate()
        {
            OnCommandSettingsValidated?.Invoke();
        }
    }
}