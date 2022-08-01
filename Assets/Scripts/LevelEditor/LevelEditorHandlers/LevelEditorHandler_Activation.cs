using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class LevelEditorHandler_Activation : LevelEditorHandlerBase
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
                GUILayout.Label("Level Editor is active.");
                GUILayout.Space(25);
                GUILayout.EndHorizontal();
                
                GUILayout.Space(10);
                
                EditorGUILayout.EndVertical();
                
                GUILayout.EndArea();
                
                Handles.EndGUI();

                foreach (LevelEditorHandlerBase handler in _SubHandlers)
                {
                    handler.Handle(e);
                }
            }
        }

        protected override LevelEditorHandlerBase[] GetSubHandlers()
        {
            return new LevelEditorHandlerBase[]
            {
                new LevelEditorHandler_TileModifier(),
                new LevelEditorHandler_TileGroupSelection(),
            };
        }
    }
}