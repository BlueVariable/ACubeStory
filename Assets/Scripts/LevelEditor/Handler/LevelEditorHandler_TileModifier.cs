using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class LevelEditorHandler_TileModifier : LevelEditorHandlerBase
    {
        private bool _isModifierActive;
        
        private LevelEditorCommandController _commandController;
        private LevelEditorCommandController _CommandController
        {
            get
            {
                if (_commandController == null)
                {
                    _commandController = new LevelEditorCommandController(ELevelEditorCommand.Modification);
                }

                return _commandController;
            }
        }

        public override void Handle(Event e)
        {
            _isModifierActive = _CommandController.Handle(e);

            if (_isModifierActive)
            {
                int controlID = GUIUtility.GetControlID(FocusType.Passive);
                GUIUtility.hotControl = controlID;
                
                DisplayTileModifierNotification();
                
                foreach (LevelEditorHandlerBase subHandler in _SubHandlers)
                {
                    subHandler.Handle(e);
                }
            }
        }

        protected override LevelEditorHandlerBase[] GetSubHandlers()
        {
            return new LevelEditorHandlerBase[]
            {
                new LevelEditorHandler_TilePlacer(),
                new LevelEditorHandler_TileRemover(),
            };
        }

        private void DisplayTileModifierNotification()
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
            GUILayout.Label("Tile Modifier activated.");
            GUILayout.Space(25);
            GUILayout.EndHorizontal();
                
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
                
            GUILayout.EndArea();
            Handles.EndGUI();
        }
    }
}