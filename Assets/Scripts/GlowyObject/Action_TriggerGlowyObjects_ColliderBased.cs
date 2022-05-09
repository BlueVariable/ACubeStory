using System.Collections.Generic;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.GlowyObject
{
    public class Action_TriggerGlowyObjects_ColliderBased : ActionBase
    {
        [SerializeField] private Collider _collider = null;
        
        private List<Trigger_External_GlowyObject> _triggers;
        private List<Trigger_External_GlowyObject> _Triggers
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
        
        private List<Trigger_External_GlowyObject> GetTriggers()
        {
            Collider[] colliders = Physics.OverlapBox(
                _collider.bounds.center, 
                _collider.bounds.size / 2, 
                _collider.transform.rotation);

            List<Trigger_External_GlowyObject> glowyObjectTriggers = 
                new List<Trigger_External_GlowyObject>();
            
            foreach (Collider c in colliders)
            {
                Trigger_External_GlowyObject glowyObjectTrigger = c
                    .gameObject
                    .GetComponentInChildren<Trigger_External_GlowyObject>();

                if (glowyObjectTrigger == null)
                {
                    continue;
                }

                glowyObjectTriggers.Add(glowyObjectTrigger);
            }

            return glowyObjectTriggers;
        }
    }
}