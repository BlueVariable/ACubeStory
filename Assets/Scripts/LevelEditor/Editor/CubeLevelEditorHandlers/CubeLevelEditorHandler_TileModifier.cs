using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_TileModifier : CubeLevelEditorHandlerBase
    {
        private bool _isModifierActive;
        
        public override void Handle(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftAlt)
            {
                _isModifierActive = true;
            }

            if (e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftAlt)
            {
                _isModifierActive = false;
            }

            if (_isModifierActive)
            {
                int controlID = GUIUtility.GetControlID(FocusType.Passive);
                GUIUtility.hotControl = controlID;
                
                CreateTileModificationActivated();
                
                foreach (CubeLevelEditorHandlerBase subHandler in _SubHandlers)
                {
                    subHandler.Handle(e);
                }
            }
        }

        protected override CubeLevelEditorHandlerBase[] GetSubHandlers()
        {
            return new CubeLevelEditorHandlerBase[]
            {
                new CubeLevelEditorHandler_TilePlacer(),
                new CubeLevelEditorHandler_TileRemover(),
            };
        }

        private void CreateTileModificationActivated()
        {
            Handles.BeginGUI();
            GUILayout.BeginArea(new Rect(20, 80, 200, 100));
                
            GUI.color = Color.cyan;
            var rect = EditorGUILayout.BeginVertical();

            GUI.Box(rect, GUIContent.none);
            GUILayout.Space(10);
                
            GUI.color = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Space(25);
            GUILayout.Label("Tile Modifier Activated.");
            GUILayout.Space(25);
            GUILayout.EndHorizontal();
                
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
                
            GUILayout.EndArea();
            Handles.EndGUI();
        }
    }
}