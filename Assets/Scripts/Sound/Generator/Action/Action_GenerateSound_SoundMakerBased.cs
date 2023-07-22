using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class Action_GenerateSound_SoundMakerBased : ActionBase
    {
        [SerializeField] private SoundMaker_TypeBased _soundMaker = null;
        [SerializeField] private Trigger_LayerBase _trigger = null;
            
        public override void Execute()
        {
            if (!_trigger.LastCandidateObject)
            {
                return;
            }

            if (!_trigger
                .LastCandidateObject
                .TryGetComponent(out SoundGenerator_SoundMaker_Simple soundGenerator))
            {
                return;
            }

            soundGenerator.GenerateSound(_soundMaker);
        }
    }
}