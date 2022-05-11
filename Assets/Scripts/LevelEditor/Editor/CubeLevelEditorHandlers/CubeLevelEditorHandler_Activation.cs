using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_Activation : CubeLevelEditorHandlerBase
    {
        private bool _isEditorActive = false;

        private CubeEditorInputHandler _inputHandler;

        private CubeEditorInputHandler _InputHandler
        {
            get
            {
                if (_inputHandler == null)
                {
                    _inputHandler = new CubeEditorInputHandler(ECubeEditorControlType.Activation);
                }

                return _inputHandler;
            }
        }
        
        public override void Handle(Event e)
        {
            if (_InputHandler.Handle(e))
            {
                _isEditorActive = !_isEditorActive;
                
                return;
            }

            if (_isEditorActive)
            {
                Handles.BeginGUI();
                GUILayout.BeginArea(new Rect(20, 20, 200, 100));
                
                GUI.color = Color.cyan;
                var rect = EditorGUILayout.BeginVertical();

                GUI.Box(rect, GUIContent.none);
                GUILayout.Space(10);
                
                GUI.color = Color.white;
                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Cube Editor is active.");
                GUILayout.Space(25);
                GUILayout.EndHorizontal();
                
                GUILayout.Space(10);
                EditorGUILayout.EndVertical();
                
                GUILayout.EndArea();
                Handles.EndGUI();

                foreach (CubeLevelEditorHandlerBase handler in _SubHandlers)
                {
                    handler.Handle(e);
                }
            }
        }

        protected override CubeLevelEditorHandlerBase[] GetSubHandlers()
        {
            return new CubeLevelEditorHandlerBase[]
            {
                new CubeLevelEditorHandler_TileModifier(),
                new CubeLevelEditorHandler_CategorySelection(),
            };
        }
    }
}