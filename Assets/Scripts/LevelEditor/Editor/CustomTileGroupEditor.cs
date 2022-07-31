using BLUE.ACubeStory.TileSystem;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(TileGroup))]
    public class CustomTileGroupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);
            
            CreateUpdateTilesButton();
        }

        private void CreateUpdateTilesButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Update Children Tiles", GUILayout.Width(200), GUILayout.Height(25)))
            {
                UpdateTileGroup();
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void UpdateTileGroup()
        {
            TileGroup tileGroup = target as TileGroup;

            int tileCount = tileGroup.transform.childCount;

            for (int i = 0; i < tileCount; i++)
            {
                Transform child = tileGroup.transform.GetChild(0);

                Vector3 position = child.transform.position;
                
                DestroyImmediate(child.gameObject);

                CreateTile(position, tileGroup.transform);
            }
        }

        private void CreateTile(Vector3 position, Transform parent)
        {
            bool res = TileFactory.Instance.TryCreateTile(CubeEditor.Instance.SelectedTileID, position, out Tile tile);
            
            tile.transform.SetParent(parent);
        }
    }
}