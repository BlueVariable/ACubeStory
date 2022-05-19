using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.GlowyObject
{
    public class Action_TriggerExternalGlowyObjects_ColliderBased : ActionBase
    {
        [SerializeField] private Transform _triggerOrigin = null;
        
        [SerializeField] private Collider _collider = null;

        [SerializeField] private float _delayBetweenGlowyObjects = 0.1f;

        private IEnumerator _triggerRoutine;
        
        private List<Trigger_External> _triggers;
        private List<Trigger_External> _Triggers
        {
            get
            {
                if (_triggers == null)
                {
                    _triggers = GetTriggers();

                    _triggers = _triggers.OrderBy(val 
                        => Vector3.Distance(_triggerOrigin.position, val.transform.position)).ToList();
                }

                return _triggers;
            }
        }
        
        public override void Execute()
        {
            StartTriggerRoutine();
        }

        private void StartTriggerRoutine()
        {
            StopTriggerRoutine();

            _triggerRoutine = TriggerProgress();

            StartCoroutine(_triggerRoutine);
        }

        private void StopTriggerRoutine()
        {
            if (_triggerRoutine != null)
            {
                StopCoroutine(_triggerRoutine);
            }
        }

        private IEnumerator TriggerProgress()
        {
            foreach (Trigger_External externalTrigger in _Triggers)
            {
                externalTrigger.Trigger();

                if (_delayBetweenGlowyObjects > 0)
                {
                    yield return new WaitForSeconds(_delayBetweenGlowyObjects);
                }
            }
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