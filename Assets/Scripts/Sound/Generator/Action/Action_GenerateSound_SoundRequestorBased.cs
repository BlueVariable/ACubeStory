using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class Action_GenerateSound_SoundRequestorBased : ActionBase
    {
        [SerializeField] private SoundRequestor_TypeBased _soundRequestor = null;
        [SerializeField] private Trigger_LayerBase _trigger = null;
            
        public override void Execute()
        {
            if (!_trigger.LastCandidateObject)
            {
                return;
            }

            if (!_trigger
                .LastCandidateObject
                .TryGetComponent(out SoundGenerator_SoundRequestor_Simple soundGenerator))
            {
                return;
            }

            soundGenerator.GenerateSound(_soundRequestor);
        }
    }
}