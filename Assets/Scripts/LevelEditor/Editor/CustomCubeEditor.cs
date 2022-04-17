using System;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(CubeEditor))]
    public class CustomCubeEditor : Editor
    {
        private int _selectedCategoryIndex = 0;

        private SerializedProperty _toolTipProperty;
        
        [MenuItem("BLUE/Level Editor/Initialize New Level")]
        public static void InitLevel()
        {
            Level.Level level = FindObjectOfType<Level.Level>();

            if (level != null)
            {
                throw new Exception($"There is already initialized level in active scene.");
            }

            GameObject cubeEditor = new GameObject();
            cubeEditor.name = $"=== CUBE_EDITOR ===";
            CubeEditor editor = cubeEditor.AddComponent<CubeEditor>();
            ResetGameObject(cubeEditor, null);

            GameObject levelObject = new GameObject();
            levelObject.name = "=== LEVEL ===";
            editor.Level = levelObject.AddComponent<Level.Level>();
            ResetGameObject(levelObject, null);

            GameObject baseCategory = new GameObject();
            baseCategory.name = "BASE";
            baseCategory.AddComponent<TileGroup>();
            ResetGameObject(baseCategory, levelObject.transform);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.BeginVertical();
            
            EditorGUILayout.Space(10);
            
            InitCategorySelection();

            CreateToolTips();
            
            GUILayout.EndVertical();
        }

        private void OnEnable()
        {
            _toolTipProperty = serializedObject.FindProperty("_displayToolTips");
        }

        private void InitCategorySelection()
        {
            CubeEditor cubeEditor = (CubeEditor) target;

            int categoryCount = cubeEditor.Level.transform.childCount;
            
            string[] categories = new string[categoryCount];

            for (int i = 0; i < categoryCount; i++)
            {
                categories[i] = cubeEditor.Level.transform.GetChild(i).name;
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Select Category : ", GUILayout.Width(150));
            
            _selectedCategoryIndex = EditorGUILayout.Popup(_selectedCategoryIndex, categories);

            cubeEditor.SelectedTileGroupIndex = _selectedCategoryIndex;
            
            EditorGUILayout.EndHorizontal();
        }

        private void CreateToolTips()
        {
            if (!_toolTipProperty.boolValue)
            {
                return;
            }

            float labelWidth = 200;
            
            EditorGUILayout.Space(20);
            
            EditorGUI.BeginDisabledGroup(true);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.BeginVertical();
            GUILayout.Label("==== TOOLTIPS ====");
            GUILayout.EndVertical();
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("You can init Editor object in menu : ", GUILayout.Width(labelWidth));
            GUILayout.Label("BLUE > Level Editor > Initialize New Level");
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tile Selection Window : ", GUILayout.Width(labelWidth));
            GUILayout.Label("BLUE > Level Editor > Tile Selection Window");
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Activate Disable Editor :", GUILayout.Width(labelWidth));
            GUILayout.Label("Left Ctrl + Left Alt + O");
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tile Addition :", GUILayout.Width(labelWidth));
            GUILayout.Label("Left Alt + MOUSE1");
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tile Deletion :", GUILayout.Width(labelWidth));
            GUILayout.Label("Left Alt + MOUSE2");
            GUILayout.EndVertical();

            GUILayout.EndVertical();
            
            EditorGUI.EndDisabledGroup();  
            
            EditorGUILayout.Space(20);
        }
        
        private static void ResetGameObject(GameObject gameObject, Transform parent)
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
            
            gameObject.transform.SetParent(parent);
            EditorUtility.SetDirty(gameObject);
        }
    }
}