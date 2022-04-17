using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CreateAssetMenu(fileName = "CubeEditorInputSO", menuName = "BLUE/Settings/Level Maker/Create Editor Input Settings SO", order = 0)]
    public class CubeEditorInputSO : ScriptableObject
    {
        [SerializeField] private List<CubeEditorControlSettings> _controlSettings = new List<CubeEditorControlSettings>();
        public List<CubeEditorControlSettings> ControlSettings => _controlSettings;

        public bool TryGetControlSettings(ECubeEditorControlType controlType, out CubeEditorControlSettings settings)
        {
            settings = _controlSettings.SingleOrDefault(i => i.ControlType == controlType);

            return settings != null;
        }
    }
}