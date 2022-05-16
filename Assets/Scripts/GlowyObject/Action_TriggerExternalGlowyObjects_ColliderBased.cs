using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.GlowyObject
{
    public class Action_TriggerExternalGlowyObjects_ColliderBased : ActionBase
    {
        [SerializeField] private Collider _collider = null;
        
        private List<Trigger_External> _triggers;
        private List<Trigger_External> _Triggers
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
        
        private List<Trigger_External> GetTriggers()
        {
            Collider[] colliders = Physics.OverlapBox(
                _collider.bounds.center, 
                _collider.bounds.size / 2, 
                _collider.transform.rotation);

            List<Trigger_External> glowyObjectTriggers = 
                new List<Trigger_External>();
            
            foreach (Collider c in colliders)
            {
                GlowyObject glowyObject = c
                    .gameObject
                    .GetComponentInChildren<GlowyObject>();

                if (glowyObject == null)
                {
                    continue;
                }

                Trigger_External externalGlowyObjectTrigger = glowyObject.GlowTrigger as Trigger_External;

                if (externalGlowyObjectTrigger == null)
                {
                    continue;
                }
                
                glowyObjectTriggers.Add(externalGlowyObjectTrigger);
            }

            return glowyObjectTriggers;
        }
    }
}