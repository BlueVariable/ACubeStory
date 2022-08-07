using BLUE.ACubeStory.TileSystem;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(TileGroupEditorContainer))]
    public class Editor_TileGroup : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);
            
            CreateUpdateTileGroupButton();
        }

        private void CreateUpdateTileGroupButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Update Tile Group (Children Tiles)", GUILayout.Width(250), GUILayout.Height(25)))
            {
                UpdateTileGroup();
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void UpdateTileGroup()
        {
            TileGroupEditorContainer tileGroupEditorContainer = target as TileGroupEditorContainer;

            if (tileGroupEditorContainer == null)
            {
                return;
            }
            
            int tileCount = tileGroupEditorContainer.transform.childCount;

            for (int i = 0; i < tileCount; i++)
            {
                Transform child = tileGroupEditorContainer.transform.GetChild(0);

                Vector3 position = child.transform.position;
                
                DestroyImmediate(child.gameObject);

                CreateTile(position, tileGroupEditorContainer.transform);
            }
        }

        private void CreateTile(Vector3 position, Transform parent)
        {
            TileFactory.Instance.TryCreateTile(LevelEditorContainer.Instance.SelectedTileID, position, out Tile tile);
            
            tile.transform.SetParent(parent);
        }
    }
}