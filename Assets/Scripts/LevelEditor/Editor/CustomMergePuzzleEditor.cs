using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.TileSystem;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(MergePuzzleEditor))]
    public class CustomMergePuzzleEditor : Editor
    {
        private float _nextSceneTriggerVerticalOffset = -2.5f;
        private float _invisibleGroundVerticalOffset = -5f;
        
        private float _wallVerticalOffset = 2.5f;
        private float _wallHeight = 5f;
        private float _wallPlacementOffset = 0.75f;
        private float _wallRendererThickness = 0.01f;
        private float _wallThickness = 0.1f;

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

                TileGroup mergeTileGroup = null;

                foreach (TileGroup tileGroup in CubeEditor.Instance.LevelHolder.GetComponentsInChildren<TileGroup>())
                {
                    if (tileGroup.gameObject.name.Equals("MERGE"))
                    {
                        mergeTileGroup = tileGroup;
                    }
                }
                
                Tile[] mergeTiles = mergeTileGroup.GetComponentsInChildren<Tile>();

                if (mergeTiles.Length == 0)
                {
                    Debug.LogError($"No Merge Tile found!");
                    return;
                }
                
                CreateMergeAreaWalls(mergePuzzle, mergeTiles);
                
                CreateNextSceneTrigger(mergePuzzle, mergeTiles);

                CreateMergeInvisibleGround(mergePuzzle, mergeTiles);

                CreateMergeableSlotGrounds(mergePuzzle);
            }
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void ResetMergePuzzle(MergePuzzle mergePuzzle)
        {
            MergeAreaWall[] existingWalls = mergePuzzle.GetComponentsInChildren<MergeAreaWall>(true);

            for (int i = 0; i < existingWalls.Length; i++)
            {
                DestroyImmediate(existingWalls[i].gameObject);
            }

            MergePuzzleExitArea mergePuzzleExitArea = mergePuzzle.GetComponentInChildren<MergePuzzleExitArea>();

            Tile[] invisibleGroundTiles = mergePuzzleExitArea.GetComponentsInChildren<Tile>();
            
            for (int i = 0; i < invisibleGroundTiles.Length; i++)
            {
                DestroyImmediate(invisibleGroundTiles[i].gameObject);
            }
        }

        private void CreateNextSceneTrigger(MergePuzzle mergePuzzle, Tile[] mergeTiles)
        {
            MergePuzzleExitArea mergePuzzleExitArea = mergePuzzle.GetComponentInChildren<MergePuzzleExitArea>();
            
            GetMergeTilesExtensions(mergeTiles, out Vector3 center, out Vector3 size);

            center.y += _nextSceneTriggerVerticalOffset;

            mergePuzzleExitArea.NextSceneTriggerCollider.transform.position = center;
            mergePuzzleExitArea.NextSceneTriggerCollider.size = size;
        }

        private void GetMergeTilesExtensions(Tile[] mergeTiles, out Vector3 center, out Vector3 size)
        {
            center = Vector3.zero;
            size = Vector3.zero;
            
            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;

            min.y = max.y = mergeTiles[0].transform.position.y;

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
            MergePuzzleExitArea mergePuzzleExitArea = mergePuzzle.GetComponentInChildren<MergePuzzleExitArea>();
            
            foreach (Tile tile in mergeTiles)
            {
                GameObject invisibleTile = PrefabUtility.InstantiatePrefab(mergePuzzleExitArea.InvisibleTileReference.gameObject) as GameObject;
                invisibleTile.transform.SetParent(mergePuzzleExitArea.InvisibleGroundContainer.transform);

                invisibleTile.transform.position = tile.transform.position + new Vector3(0, _invisibleGroundVerticalOffset, 0);
            }
        }

        private void CreateMergeableSlotGrounds(MergePuzzle mergePuzzle)
        {
            MergeableSlot[] mergeableSlots = mergePuzzle.GetComponentsInChildren<MergeableSlot>();

            MergePuzzleExitArea mergePuzzleExitArea = mergePuzzle.GetComponentInChildren<MergePuzzleExitArea>();
            
            foreach (MergeableSlot slot in mergeableSlots)
            {
                GameObject invisibleTile = PrefabUtility.InstantiatePrefab(mergePuzzleExitArea.InvisibleTileReference.gameObject) as GameObject;  
                
                invisibleTile.transform.SetParent(mergePuzzleExitArea.MergableSlotGrounds);

                invisibleTile.transform.position = slot.transform.position;
            }
        }

        private void CreateMergeAreaWalls(MergePuzzle mergePuzzle, Tile[] mergeTiles)
        {
            MergeAreaWallContainer areaWallContainer = mergePuzzle.GetComponentInChildren<MergeAreaWallContainer>(true);
            
            GetMergeTilesExtensions(mergeTiles, out Vector3 center, out Vector3 size);

            CreateWall(areaWallContainer, center + new Vector3(0, _wallVerticalOffset, size.z / 2), new Vector3(size.x, _wallHeight, _wallThickness), center);
            CreateWall(areaWallContainer, center + new Vector3(0, _wallVerticalOffset, -size.z / 2), new Vector3(size.x, _wallHeight, _wallThickness), center);
            CreateWall(areaWallContainer, center + new Vector3(size.x / 2, _wallVerticalOffset, 0), new Vector3(_wallThickness, _wallHeight, size.z), center);
            CreateWall(areaWallContainer, center + new Vector3(-size.x / 2, _wallVerticalOffset, 0), new Vector3(_wallThickness, _wallHeight, size.z), center);
        }

        private void CreateWall(MergeAreaWallContainer areaWallContainer, Vector3 center, Vector3 size, Vector3 mergeAreaCenter)
        {
            GameObject wall = PrefabUtility.InstantiatePrefab(areaWallContainer.ReferenceWall.gameObject) as GameObject;
            wall.transform.SetParent(areaWallContainer.transform);

            wall.transform.position = center;
            wall.transform.localScale = size;

            MergeAreaWall mergeAreaWall = wall.GetComponent<MergeAreaWall>();

            Vector3 dir = (center - mergeAreaCenter);
            dir.y = 0;

            mergeAreaWall.Collider.transform.localPosition = dir.normalized * _wallPlacementOffset;
            mergeAreaWall.Renderer.transform.localPosition = new Vector3(0, -0.4f, 0);

            Vector3 wallSize = new Vector3(_wallRendererThickness, _wallRendererThickness, _wallRendererThickness);
            
            if (size.x > _wallThickness)
            {
                wallSize.x = 1;
            }

            if (size.z > _wallThickness)
            {
                wallSize.z = 1;
            }

            mergeAreaWall.Renderer.transform.localScale = wallSize;
        }
    }
}