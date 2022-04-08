using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_Activation : CubeLevelEditorHandlerBase
    {
        private bool _isEditorActive = false;

        private bool _isCtrlDown = false;
        private bool _isAltDown = false;
        
        public override void Handle(Event e)
        {
            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.LeftControl)
                {
                    _isCtrlDown = true;
                }

                if (e.keyCode == KeyCode.LeftAlt)
                {
                    _isAltDown = true;
                }
            }

            if (e.type == EventType.KeyUp)
            {
                if (e.keyCode == KeyCode.LeftControl)
                {
                    _isCtrlDown = false;
                }

                if (e.keyCode == KeyCode.LeftAlt)
                {
                    _isAltDown = false;
                }
            }
            

            if (_isCtrlDown && _isAltDown && e.type == EventType.KeyDown && e.keyCode == KeyCode.O)
            {
                _isEditorActive = !_isEditorActive;
                
                UnityEngine.Debug.Log($"Cube Level Editor {_isEditorActive}");
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
            }
        }
        
        private Texture2D MakeTex( int width, int height, Color col )
        {
            Color[] pix = new Color[width * height];
            for( int i = 0; i < pix.Length; ++i )
            {
                pix[ i ] = col;
            }
            Texture2D result = new Texture2D( width, height );
            result.SetPixels( pix );
            result.Apply();
            return result;
        }
    }
}