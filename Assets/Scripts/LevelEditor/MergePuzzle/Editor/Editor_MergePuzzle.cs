using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.TileSystem;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(MergePuzzleEditorContainer))]
    public class Editor_MergePuzzle : Editor
    {
        private float _nextSceneTriggerVerticalOffset = -2.5f;
        
        private float _exitAreaInvGroundVerticalOffset = -5f;
        
        private float _wallVerticalOffset = 2.5f;
        private float _wallPlacementOffset = 0.75f;
        private float _wallHeight = 5f;
        private float _wallThickness = 0.1f;
        
        private float _wallRendererVerticalOffset = -0.4f;
        private float _wallRendererThickness = 0.01f;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(10);
            
            CreateAdditionalComponentsButton();
        }

        private void CreateAdditionalComponentsButton()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create & Prepare Additional Merge Puzzle Components", GUILayout.Height(25)))
            {
                OnAdditionalComponentsButtonClicked();
            }
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void OnAdditionalComponentsButtonClicked()
        {
            MergePuzzle mergePuzzle = FindObjectOfType<MergePuzzle>();

            if (mergePuzzle == null)
            {
                Debug.LogError($"Merge Puzzle is not initialized yet!");
                
                return;
            }
                
            ResetMergePuzzle(mergePuzzle);

            TileGroupEditorContainer mergeTileGroupEditorContainer = null;

            foreach (TileGroupEditorContainer tileGroup in LevelEditorContainer.Instance.EditorLevelRef.GetComponentsInChildren<TileGroupEditorContainer>())
            {
                if (tileGroup.gameObject.name.Equals(LevelEditorHelper.MERGE_TILE_GROUP_NAME))
                {
                    mergeTileGroupEditorContainer = tileGroup;
                }
            }
                
            if (mergeTileGroupEditorContainer == null)
            {
                Debug.LogError($"No merge tile group found!");
                
                return;
            }
                
            Tile[] mergeTiles = mergeTileGroupEditorContainer.GetComponentsInChildren<Tile>();

            if (mergeTiles.Length == 0)
            {
                Debug.LogError($"No merge puzzle tile found!");
                    
                return;
            }
                
            CreateMergeAreaWalls(mergePuzzle, mergeTiles);
                
            PrepareNextSceneTrigger(mergePuzzle, mergeTiles);

            PrepareMergeableSlotGrounds(mergePuzzle);
            
            PrepareExitAreaInvisibleGrounds(mergePuzzle, mergeTiles);
        }

        private void ResetMergePuzzle(MergePuzzle mergePuzzle)
        {
            MergeAreaWall[] existingWalls = mergePuzzle.GetComponentsInChildren<MergeAreaWall>(true);

            for (int i = 0; i < existingWalls.Length; i++)
            {
                DestroyImmediate(existingWalls[i].gameObject);
            }

            MergePuzzleExitArea mergePuzzleExitArea = mergePuzzle.GetComponentInChildren<MergePuzzleExitArea>();

            Tile[] exitAreaTiles = mergePuzzleExitArea.GetComponentsInChildren<Tile>();
            
            for (int i = 0; i < exitAreaTiles.Length; i++)
            {
                DestroyImmediate(exitAreaTiles[i].gameObject);
            }
        }

        private void PrepareNextSceneTrigger(MergePuzzle mergePuzzle, Tile[] mergeTiles)
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

            void handleMin(Vector3 tilePos)
            {
                if (tilePos.x < min.x)
                {
                    min.x = tilePos.x;
                }

                if (tilePos.z < min.z)
                {
                    min.z = tilePos.z;
                }
            }
            
            void handleMax(Vector3 tilePos)
            {
                if (tilePos.x > max.x)
                {
                    max.x = tilePos.x;
                }

                if (tilePos.z > max.z)
                {
                    max.z = tilePos.z;
                }
            }
            
            foreach (Tile tile in mergeTiles)
            {
                Vector3 tilePos = tile.transform.position;

                handleMin(tilePos);
                
                handleMax(tilePos);
            }

            center = (min + max) / 2;
            size = max - min;
            size += new Vector3(1, 1, 1);
        }

        private void PrepareExitAreaInvisibleGrounds(MergePuzzle mergePuzzle, Tile[] mergeTiles)
        {
            MergePuzzleExitArea mergePuzzleExitArea = mergePuzzle.GetComponentInChildren<MergePuzzleExitArea>();

            Tile[] mergeableSlotGroundTiles = mergePuzzleExitArea.MergableSlotGrounds.GetComponentsInChildren<Tile>();
            
            foreach (Tile tile in mergeableSlotGroundTiles)
            {
                GameObject invisibleTile = PrefabUtility.InstantiatePrefab(mergePuzzleExitArea.InvisibleTileReference.gameObject) as GameObject;
                
                invisibleTile.transform.SetParent(mergePuzzleExitArea.InvisibleGroundContainer.transform);

                invisibleTile.transform.position = tile.transform.position + new Vector3(0, _exitAreaInvGroundVerticalOffset, 0);
            }
        }

        private void PrepareMergeableSlotGrounds(MergePuzzle mergePuzzle)
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

            CreateMergeAreaWall(areaWallContainer, center + new Vector3(0, _wallVerticalOffset, size.z / 2), new Vector3(size.x, _wallHeight, _wallThickness), center);
            CreateMergeAreaWall(areaWallContainer, center + new Vector3(0, _wallVerticalOffset, -size.z / 2), new Vector3(size.x, _wallHeight, _wallThickness), center);
            CreateMergeAreaWall(areaWallContainer, center + new Vector3(size.x / 2, _wallVerticalOffset, 0), new Vector3(_wallThickness, _wallHeight, size.z), center);
            CreateMergeAreaWall(areaWallContainer, center + new Vector3(-size.x / 2, _wallVerticalOffset, 0), new Vector3(_wallThickness, _wallHeight, size.z), center);
        }

        private void CreateMergeAreaWall(MergeAreaWallContainer areaWallContainer, Vector3 center, Vector3 size, Vector3 mergeAreaCenter)
        {
            GameObject wall = PrefabUtility.InstantiatePrefab(areaWallContainer.ReferenceWall.gameObject) as GameObject;
            wall.transform.SetParent(areaWallContainer.transform);

            wall.transform.position = center;
            wall.transform.localScale = size;

            MergeAreaWall mergeAreaWall = wall.GetComponent<MergeAreaWall>();

            Vector3 dir = (center - mergeAreaCenter);
            dir.y = 0;

            mergeAreaWall.Collider.transform.localPosition = dir.normalized * _wallPlacementOffset;
            mergeAreaWall.Renderer.transform.localPosition = new Vector3(0, _wallRendererVerticalOffset, 0);

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