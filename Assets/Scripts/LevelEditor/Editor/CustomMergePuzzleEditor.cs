using BLUE.ACubeStory.Merge;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [CustomEditor(typeof(MergePuzzleEditor))]
    public class CustomMergePuzzleEditor : Editor
    {
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
                
                TileGroup mergeTileGroup = mergePuzzle.GetComponentInParent<TileGroup>();
                
                Tile[] mergeTiles = mergeTileGroup.GetComponentsInChildren<Tile>();

                if (mergeTiles.Length == 0)
                {
                    Debug.LogError($"No Merge Tile found!");
                    return;
                }
                
                CreateNextSceneTrigger(mergePuzzle, mergeTiles);

                CreateMergeInvisibleGround(mergePuzzle, mergeTiles);
            }
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
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

            float verticalOffset = -5;

            min.y = max.y = mergeTiles[0].transform.position.y + verticalOffset;

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

            float verticalOffset = -7.5f;
            
            foreach (Tile tile in mergeTiles)
            {
                GameObject invisibleTile = Instantiate(nextSceneTrigger.InvisibleTileReference.gameObject,
                    nextSceneTrigger.InvisibleGroundContainer);

                invisibleTile.transform.position = tile.transform.position + new Vector3(0, verticalOffset, 0);
            }
        }
    }
}