using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.TileSystem;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileParentProvider
    {
        public Transform GetTileParent(Tile tile)
        {
            if (tile.TileCategory == ETileCategory.Ground)
            {
                return CubeEditor.Instance.SelectedTileGroupEditorContainer.transform;
            }

            MergePuzzle mergePuzzle = GameObject.FindObjectOfType<MergePuzzle>();

            if (tile.TileCategory == ETileCategory.Mergeable)
            {
                if (mergePuzzle == null)
                {
                    Debug.LogError($"Mergeable could not be placed. Merge Puzzle not initialized.");
                }

                return mergePuzzle.transform.GetChild(0);
            }

            if (tile.TileCategory == ETileCategory.MergeableSlot)
            {
                if (mergePuzzle == null)
                {
                    Debug.LogError($"Mergeable Slot could not be placed. Merge Puzzle not initialized.");
                }
                
                return mergePuzzle.transform.GetChild(1);
            }

            return null;
        }
    }
}