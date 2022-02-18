using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Descend : RewindSnapshot_Cube
    {
        public Vector3 DescendDirection { get; }
        
        private CubeStateRewinder_Descend _descendRewinder;

        public RewindSnapshot_Cube_Descend(CubeStateRewinder_Descend descendRewinder, Vector3 descendDirection)
        {
            _descendRewinder = descendRewinder;
            DescendDirection = descendDirection;
        }

        public override void Rewind(Action completedCallback)
        {
            _descendRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke();
            });
        }
    }
}