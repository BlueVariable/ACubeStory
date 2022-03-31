using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class TileHighlightData
    {
        public Vector3 HighlightPosition { get; }
        
        public Color HighlightColor { get; } = Color.white;

        public TileHighlightData(
            Vector3 highlightPosition)
        {
            HighlightPosition = highlightPosition;
        }

        public TileHighlightData(
            Vector3 highlightPosition,
            Color highlightColor)
        {
            HighlightPosition = highlightPosition;
            HighlightColor = highlightColor;
        }
    }

    public class TileHighlighter : MonoBehaviour
    {
        [SerializeField] private Transform _highlightObject = null;

        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        private Renderer _highlighterRenderer = null;

        private Renderer _HighlighterRenderer
        {
            get
            {
                if (_highlighterRenderer == null)
                {
                    _highlighterRenderer = _highlightObject.GetComponentInChildren<Renderer>();
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
            _highlightObject.gameObject.SetActive(isEnabled);
        }
        
        public void HighlightPosition(TileHighlightData highlightData)
        {
            _highlightObject.transform.position = highlightData.HighlightPosition;
            
            SetHighlighterColor(highlightData.HighlightColor);
        }
        
        private void Awake()
        {
            SetHighlightEnabled(false);
        }

        private void SetHighlighterColor(Color c)
        {
            _HighlightPropBlock.SetColor(_colorPropertyName, c);
            
            _HighlighterRenderer.SetPropertyBlock(_HighlightPropBlock);
        }
    }
}