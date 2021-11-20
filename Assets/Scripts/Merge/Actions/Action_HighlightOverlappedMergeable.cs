using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Misc;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Action_HighlightOverlappedMergeable : ActionBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;
        
        [SerializeField] private Trigger_CubeMergeableOverlapBase _mergeableOverlapTrigger = null;

        [SerializeField] private QuickOutline_Highlighter.QuickOutlineHighlighterData _highlighterData = null;

        [SerializeField] private bool _onlyHighlightOverlapped = false;

        public override void Execute()
        {
            HashSet<QuickOutline_Highlighter> highlighterColl = new HashSet<QuickOutline_Highlighter>();
            
            foreach (Mergeable mergeable in _mergeableOverlapTrigger.GetLastBottleneckMergeables())
            {
                highlighterColl.Add(mergeable.GetComponentInChildren<QuickOutline_Highlighter>());
            }

            if (!_onlyHighlightOverlapped)
            {
                foreach (Mergeable mergeable in _mergeableComposition.Mergeables)
                {
                    highlighterColl.Add(mergeable.GetComponentInChildren<QuickOutline_Highlighter>());
                }
            }

            foreach (QuickOutline_Highlighter highlighter in highlighterColl)
            {
                highlighter.StartHighlightRoutine(_highlighterData);
            }
        }
    }
}