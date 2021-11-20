using System;
using System.Collections;
using UnityEngine;

namespace BLUE.ACubeStory.Misc
{
    public class QuickOutline_Highlighter : MonoBehaviour
    {
        [Serializable]
        public class QuickOutlineHighlighterData
        {
            [SerializeField] private Color _color = default;
            public Color Color => _color;
            
            [SerializeField] private float _targetWidth = 7f;
            public float TargetWidth => _targetWidth;
            
            [SerializeField] private float _duration = 1f;
            public float Duration => _duration;
            
            [SerializeField] private bool _unhighlightAfterCompletion = true;
            public bool UnhighlightAfterCompletion => _unhighlightAfterCompletion;
        }
        
        [SerializeField] private QuickOutline.QuickOutline _quickOutline = null;

        private IEnumerator _highlightRoutine;

        public void StartHighlightRoutine(QuickOutlineHighlighterData data)
        {
            StopHighlightRoutine();

            _highlightRoutine = HighlightProgress(data);

            StartCoroutine(_highlightRoutine);
        }

        public void StopHighlightRoutine()
        {
            if (_highlightRoutine != null)
            {
                StopCoroutine(_highlightRoutine);
            }
        }

        private IEnumerator HighlightProgress(QuickOutlineHighlighterData data)
        {
            float timePassed = 0;

            _quickOutline.OutlineColor = data.Color;

            while (timePassed < data.Duration)
            {
                _quickOutline.OutlineWidth = Mathf.Lerp(_quickOutline.OutlineWidth, 
                    data.TargetWidth, timePassed / data.Duration);

                timePassed += Time.deltaTime;

                yield return null;
            }

            if (data.UnhighlightAfterCompletion)
            {
               timePassed = 0;
                
                while (timePassed < data.Duration)
                {
                    _quickOutline.OutlineWidth = Mathf.Lerp(_quickOutline.OutlineWidth, 
                        0, timePassed / data.Duration);

                    timePassed += Time.deltaTime;

                    yield return null;
                }
            }
        }
    }
}
