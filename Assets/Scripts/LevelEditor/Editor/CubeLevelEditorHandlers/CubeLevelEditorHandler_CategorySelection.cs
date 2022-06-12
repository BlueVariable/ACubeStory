using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_CategorySelection : CubeLevelEditorHandlerBase
    {
        private int _selectedIndex = 0;

        private float _height = 50;
        private float _width = 150;

        public override void Handle(Event e)
        {
            List<string> categories = new List<string>();

            if (CubeEditor.Instance.LevelHolder == null)
            {
                return;
            }
            
            foreach (TileGroup tileGroup in CubeEditor.Instance.LevelHolder.GetComponentsInChildren<TileGroup>())
            {
                categories.Add(tileGroup.gameObject.name);
            }

            Handles.BeginGUI();
            GUILayout.BeginArea(new Rect(Camera.current.pixelWidth - _width - 25, 125, _width, _height));
            
            Rect rect = EditorGUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUI.color = Color.cyan;
            GUI.Box(rect, GUIContent.none);

            GUI.color = Color.white;

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Select Tile Group : ");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _selectedIndex = 
                EditorGUILayout.Popup(
                    _selectedIndex,
                    categories.ToArray(),
                    GUILayout.Width(_width - 25));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
            
            GUILayout.EndArea();
            Handles.EndGUI();

            CubeEditor.Instance.SelectedTileGroupIndex = _selectedIndex;
        }
    }
}