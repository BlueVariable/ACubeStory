using System.Collections.Generic;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.NightLight
{
    public class Action_Trigger_NightLight_ColliderBased : ActionBase
    {
        [SerializeField] private Collider _collider = null;
        
        private List<Trigger_NightLight> _triggers;
        private List<Trigger_NightLight> _Triggers
        {
            get
            {
                if (_triggers == null)
                {
                    _triggers = GetTriggers();
                }

                return _triggers;
            }
        }
        
        public override void Execute()
        {
            _Triggers.ForEach(i => i.Trigger());
        }
        
        private List<Trigger_NightLight> GetTriggers()
        {
            Collider[] colliders = Physics.OverlapBox(
                _collider.bounds.center, 
                _collider.bounds.size / 2, 
                _collider.transform.rotation);

            List<Trigger_NightLight> colorTweeners = 
                new List<Trigger_NightLight>();
            
            foreach (Collider c in colliders)
            {
                Trigger_NightLight colorTweener = c
                    .gameObject
                    .GetComponentInChildren<Trigger_NightLight>();

                if (colorTweener == null)
                {
                    continue;
                }

                colorTweeners.Add(colorTweener);
            }

            return colorTweeners;
        }
    }
}