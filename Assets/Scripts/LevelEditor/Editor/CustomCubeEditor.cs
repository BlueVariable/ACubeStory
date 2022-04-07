using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(CubeEditor))]
    public class CustomCubeEditor : Editor
    {
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