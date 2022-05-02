using BLUE.ACubeStory.Merge;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(MergePuzzleEditor))]
    public class CustomMergePuzzleEditor : Editor
    {
        private float _nextSceneTriggerVerticalOffset = -2.5f;
        private float _invisibleGroundVerticalOffset = -5f;
        private float _wallVerticalOffset = 5f;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(10);
            
            CreateNextSceneTriggerButton();
        }

        private void CreateNextSceneTriggerButton()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create Next Scene Trigger and Invisible Grounds", GUILayout.Height(25)))
            {
                MergePuzzle mergePuzzle = FindObjectOfType<MergePuzzle>();

                if (mergePuzzle == null)
                {
                    Debug.LogError($"Merge Puzzle not initialized yet!");
                
                    return;
                }
                
                ResetMergePuzzle(mergePuzzle);
                
                TileGroup mergeTileGroup = mergePuzzle.GetComponentInParent<TileGroup>();
                
                Tile[] mergeTiles = mergeTileGroup.GetComponentsInChildren<Tile>();

                if (mergeTiles.Length == 0)
                {
                    Debug.LogError($"No Merge Tile found!");
                    return;
                }
                
                CreateNextSceneTrigger(mergePuzzle, mergeTiles);

                CreateMergeInvisibleGround(mergePuzzle, mergeTiles);

                CreateMergeAreaWalls(mergePuzzle, mergeTiles);
            }
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void ResetMergePuzzle(MergePuzzle mergePuzzle)
        {
            MergeAreaWall[] existingWalls = mergePuzzle.GetComponentsInChildren<MergeAreaWall>();

            for (int i = 0; i < existingWalls.Length; i++)
            {
                DestroyImmediate(existingWalls[i].gameObject);
            }

            MergeNextSceneTrigger nextSceneTrigger = mergePuzzle.GetComponentInChildren<MergeNextSceneTrigger>();

            Tile[] invisibleGroundTiles = nextSceneTrigger.GetComponentsInChildren<Tile>();
            
            for (int i = 0; i < invisibleGroundTiles.Length; i++)
            {
                DestroyImmediate(invisibleGroundTiles[i].gameObject);
            }
        }

        private void CreateNextSceneTrigger(MergePuzzle mergePuzzle, Tile[] mergeTiles)
        {
            MergeNextSceneTrigger nextSceneTrigger = mergePuzzle.GetComponentInChildren<MergeNextSceneTrigger>();
            
            GetMergeTilesExtensions(mergeTiles, out Vector3 center, out Vector3 size);

            nextSceneTrigger.NextSceneTriggerCollider.transform.position = center;
            nextSceneTrigger.NextSceneTriggerCollider.size = size;
        }

        private void GetMergeTilesExtensions(Tile[] mergeTiles, out Vector3 center, out Vector3 size)
        {
            center = Vector3.zero;
            size = Vector3.zero;
            
            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;
            
            min.y = max.y = mergeTiles[0].transform.position.y + _nextSceneTriggerVerticalOffset;

            size.y = 1;

            foreach (Tile tile in mergeTiles)
            {
                Vector3 tilePos = tile.transform.position;

                if (tilePos.x < min.x)
                {
                    min.x = tilePos.x;
                }

                if (tilePos.z < min.z)
                {
                    min.z = tilePos.z;
                }
                
                if (tilePos.x > max.x)
                {
                    max.x = tilePos.x;
                }

                if (tilePos.z > max.z)
                {
                    max.z = tilePos.z;
                }
            }

            center = (min + max) / 2;
            size = max - min;
            size += new Vector3(1, 1, 1);
        }

        private void CreateMergeInvisibleGround(MergePuzzle mergePuzzle, Tile[] mergeTiles)
        {
            MergeNextSceneTrigger nextSceneTrigger = mergePuzzle.GetComponentInChildren<MergeNextSceneTrigger>();
            
            foreach (Tile tile in mergeTiles)
            {
                GameObject invisibleTile = PrefabUtility.InstantiatePrefab(nextSceneTrigger.InvisibleTileReference.gameObject) as GameObject;
                invisibleTile.transform.SetParent(nextSceneTrigger.InvisibleGroundContainer.transform);

                invisibleTile.transform.position = tile.transform.position + new Vector3(0, _invisibleGroundVerticalOffset, 0);
            }
        }

        private void CreateMergeAreaWalls(MergePuzzle mergePuzzle, Tile[] mergeTiles)
        {
            MergePuzzleWallContainer wallContainer = mergePuzzle.GetComponentInChildren<MergePuzzleWallContainer>(true);
            
            GetMergeTilesExtensions(mergeTiles, out Vector3 center, out Vector3 size);

            CreateWall(wallContainer, center + new Vector3(0, _wallVerticalOffset, size.z / 2), new Vector3(size.x, 5f, 0.1f));
            CreateWall(wallContainer, center + new Vector3(0, _wallVerticalOffset, -size.z / 2), new Vector3(size.x, 5f, 0.1f));
            CreateWall(wallContainer, center + new Vector3(size.x / 2, _wallVerticalOffset, 0), new Vector3(0.1f, 5f, size.z));
            CreateWall(wallContainer, center + new Vector3(-size.x / 2, _wallVerticalOffset, 0), new Vector3(0.1f, 5f, size.z));
        }

        private void CreateWall(MergePuzzleWallContainer wallContainer, Vector3 center, Vector3 size)
        {
            GameObject wall = PrefabUtility.InstantiatePrefab(wallContainer.ReferenceWall.gameObject) as GameObject;
            wall.transform.SetParent(wallContainer.transform);

            wall.transform.position = center;
            wall.transform.localScale = size;
        }
    }
}