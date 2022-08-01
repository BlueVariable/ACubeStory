using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.TileSystem;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class EditorTileParentProvider
    {
        public Transform GetTileParent(Tile tile)
        {
            if (tile.TileCategory == ETileCategory.Ground)
            {
                return CubeEditor.Instance.SelectedTileGroupEditorContainer.transform;
            }

            MergePuzzle mergePuzzle = Object.FindObjectOfType<MergePuzzle>();

            if (tile.TileCategory == ETileCategory.Mergeable)
            {
                if (mergePuzzle == null)
                {
                    Debug.LogError($"Mergeable can not be placed, merge puzzle is not initialized!");
                }

                return mergePuzzle.GetComponentInChildren<MergeableContainer>().transform;
            }

            if (tile.TileCategory == ETileCategory.MergeableSlot)
            {
                if (mergePuzzle == null)
                {
                    Debug.LogError($"Mergeable Slot can not be placed, merge puzzle is not initialized!");
                }
                
                return mergePuzzle.GetComponentInChildren<MergeableSlotContainer>().transform;
            }

            return null;
        }
    }
}