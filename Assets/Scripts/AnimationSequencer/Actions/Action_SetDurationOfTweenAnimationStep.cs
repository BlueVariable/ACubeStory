using BLUE.ActionSystem;
using BrunoMikoski.AnimationSequencer;
using UnityEngine;

namespace BLUE.ACubeStory.AnimationSequencer
{
    public class Action_SetDurationOfTweenAnimationStep : ActionBase
    {
        [SerializeField] private AnimationSequencerController _animationSequencerController = null;

        [SerializeField] private int _index = 0;
        
        [SerializeField] private float _minDuration, _maxDuration = 1;
        
        public override void Execute()
        {
            _animationSequencerController.TryGetStepAtIndex(_index, out DOTweenAnimationStep tweenAnimStep);

            float randDuration = Random.Range(_minDuration, _maxDuration);
            
            tweenAnimStep.Duration = randDuration;
        }
    }
}

