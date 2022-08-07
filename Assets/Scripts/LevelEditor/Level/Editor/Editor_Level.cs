using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.TileSystem;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(LevelEditorContainer))]
    public class Editor_Level : Editor
    {
        private int _selectedCategoryIndex = 0;

        private SerializedProperty _tooltipProperty;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.BeginVertical();
            
            EditorGUILayout.Space(10);

            CreateMergePuzzleButton();

            EditorGUILayout.Space(10);

            CreateToolTips();
            
            GUILayout.EndVertical();
        }
        
        [MenuItem("BLUE/Level Editor/Create New Level")]
        public static void CreateLevel()
        {
            InitializeLevelEditor();

            InitializeLevel();
        }

        private static void InitializeLevelEditor()
        {
            if (FindObjectOfType<LevelEditorContainer>() != null)
            {
                return;
            }
            
            GameObject levelEditor = new GameObject
            {
                name = $"=== LEVEL_EDITOR ==="
            };
            levelEditor.AddComponent<LevelEditorContainer>();
            levelEditor.AddComponent<TileIDProvider_Editor>();
            levelEditor.AddComponent<MergePuzzleEditorContainer>();
            
            ResetGameObject(levelEditor, null);
        }

        private static void InitializeLevel()
        {
            if (FindObjectOfType<EditorLevelRef>() != null)
            {
                return;
            }
            
            GameObject levelObject = new GameObject
            {
                name = "=== LEVEL_CONTAINER ==="
            };
            levelObject.AddComponent<EditorLevelRef>();
            ResetGameObject(levelObject, null);

            GameObject baseCategory = new GameObject
            {
                name = "BASE"
            };
            baseCategory.AddComponent<TileGroupEditorContainer>();
            ResetGameObject(baseCategory, levelObject.transform);
        }

        private void OnEnable()
        {
            _tooltipProperty = serializedObject.FindProperty(LevelEditorHelper.DISPLAY_TOOLTIP_PROPERTY_NAME);
        }

        private void CreateMergePuzzleButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            string buttonText = "Init Merge Puzzle";

            if (FindObjectOfType<MergePuzzle>() != null)
            {
                buttonText = "Reset Merge Puzzle";
            }
            
            if (GUILayout.Button(buttonText, GUILayout.Width(200), GUILayout.Height(25)))
            {
                LevelEditorContainer levelEditorContainer = (LevelEditorContainer) target;

                foreach (Transform child in levelEditorContainer.EditorLevelRef.transform)
                {
                    if (child.gameObject.name.Equals(LevelEditorHelper.MERGE_TILE_GROUP_NAME))
                    {
                        DestroyImmediate(child.gameObject);
                    }
                }
                
                GameObject mergeCategory = new GameObject();
                mergeCategory.name = LevelEditorHelper.MERGE_TILE_GROUP_NAME;
                mergeCategory.AddComponent<TileGroupEditorContainer>();
                ResetGameObject(mergeCategory, levelEditorContainer.EditorLevelRef.transform);

                GameObject mergePuzzle = PrefabUtility.InstantiatePrefab(levelEditorContainer.MergePuzzleRef.gameObject) as GameObject;
                mergePuzzle.name = LevelEditorHelper.MERGE_PUZZLE_NAME;
                ResetGameObject(mergePuzzle, levelEditorContainer.EditorLevelRef.transform);
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void CreateToolTips()
        {
            if (!_tooltipProperty.boolValue)
            {
                return;
            }

            float labelWidth = 200;

            LevelEditorContainer levelEditorContainer = (LevelEditorContainer) target;
            
            EditorGUILayout.Space(20);
            
            EditorGUI.BeginDisabledGroup(true);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.BeginVertical();
            GUILayout.Label("==== TOOLTIP ====");
            GUILayout.EndVertical();
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Create New Level: ", GUILayout.Width(labelWidth));
            GUILayout.Label("BLUE > Level Editor > Create New Level");
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Create Tile Selection Window: ", GUILayout.Width(labelWidth));
            GUILayout.Label("BLUE > Level Editor > Create Tile Selection Window");
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Activate/Deactivate Level Editor:", GUILayout.Width(labelWidth));
            GUILayout.Label(GetCommandString(levelEditorContainer, ELevelEditorCommand.Activation));
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tile Modification Activation:", GUILayout.Width(labelWidth));
            GUILayout.Label(GetCommandString(levelEditorContainer, ELevelEditorCommand.Modification));
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tile Placement:", GUILayout.Width(labelWidth));
            GUILayout.Label(GetCommandString(levelEditorContainer, ELevelEditorCommand.TileAddition));
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tile Removal:", GUILayout.Width(labelWidth));
            GUILayout.Label(GetCommandString(levelEditorContainer, ELevelEditorCommand.TileRemove));
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

        private static string GetCommandString(LevelEditorContainer levelEditorContainer, ELevelEditorCommand commandType)
        {
            string text = string.Empty;

            levelEditorContainer.LevelEditorCommandSO.TryGetCommandSettings(commandType, out LevelEditorCommandSettings settings);
            
            for (int i = 0; i < settings.KeyCombination.Count; i++)
            {
                text += settings.KeyCombination[i].ToString();

                if (i != settings.KeyCombination.Count - 1)
                {
                    text += " + ";
                }
            }
            
            for (int i = 0; i < settings.MouseButton.Count; i++)
            {
                text += $"MOUSE{settings.MouseButton[i]}";

                if (i != settings.MouseButton.Count - 1)
                {
                    text += " + ";
                }
            }

            return text;
        }
    }
}