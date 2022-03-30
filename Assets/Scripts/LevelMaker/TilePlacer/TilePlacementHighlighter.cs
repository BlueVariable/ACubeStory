using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class TilePlacementHighlighter : MonoBehaviour
    {
        [SerializeField] private TilePlacer _tilePlacer = null;
        
        [SerializeField] private Transform _placementHighlight = null;

        [SerializeField] private Color _correctPlacementColor = Color.black;

        [SerializeField] private Color _incorrectPlacementColor = Color.black;

        private Renderer _highlighterRenderer = null;

        private Renderer _HighlighterRenderer
        {
            get
            {
                if (_highlighterRenderer == null)
                {
                    _highlighterRenderer = _placementHighlight.GetComponentInChildren<Renderer>();
                }

                return _highlighterRenderer;
            }
        }

        private MaterialPropertyBlock _highlightPropBlock;

        private MaterialPropertyBlock _HighlightPropBlock
        {
            get
            {
                if (_highlightPropBlock == null)
                {
                    _highlightPropBlock = new MaterialPropertyBlock();
                }

                return _highlightPropBlock;
            }
        }
        
        public void SetHighlightEnabled(bool isEnabled)
        {
            _placementHighlight.gameObject.SetActive(isEnabled);
        }

        public void HighlightPlacement(Vector3 position)
        {
            _placementHighlight.transform.position = position;

            Color c = _tilePlacer.CheckAllPlacementConditionsAreSatisfied(position)
                ? _correctPlacementColor
                : _incorrectPlacementColor;

            SetHighlighterColor(c);
        }

        private void Awake()
        {
            SetHighlightEnabled(false);
        }

        private void SetHighlighterColor(Color c)
        {
            _HighlightPropBlock.SetColor("_BaseColor", c);
            
            _HighlighterRenderer.SetPropertyBlock(_HighlightPropBlock);
        }
    }
}