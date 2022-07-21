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

        [SerializeField] private float _baseGlowDuration = 3f;

        [SerializeField] private float _glowDurationMultiplierBetweenDelays = 2;
        
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
            int count = 0;
            
            foreach (Trigger_External externalTrigger in _Triggers)
            {
                externalTrigger.Trigger();

                if (_delayBetweenGlowyObjects > 0)
                {
                    GlowyObject_GlowDurationController glowDurationController = externalTrigger
                        .GetComponentInChildren<GlowyObject_GlowDurationController>();

                    float additionalDuration = count * _delayBetweenGlowyObjects * _glowDurationMultiplierBetweenDelays;
                    
                    glowDurationController.TrySetDuration(_baseGlowDuration + additionalDuration);
                    
                    yield return new WaitForSeconds(_delayBetweenGlowyObjects);
                }

                count++;
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

                TriggerBase externalGlowyObjectTrigger = glowyObject.GlowTriggers.FirstOrDefault(val 
                        => val is Trigger_External);

                if (externalGlowyObjectTrigger == null)
                {
                    continue;
                }
                
                glowyObjectTriggers.Add(externalGlowyObjectTrigger as Trigger_External);
            }

            return glowyObjectTriggers;
        }
    }
}