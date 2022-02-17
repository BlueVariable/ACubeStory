using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Dash : RewindSnapshot_Cube
    {
        public Vector3 RewindStartPos { get; }
        
        private CubeStateRewinder_Dash _dashRewinder;

        public RewindSnapshot_Cube_Dash(CubeStateRewinder_Dash dashRewinder, Vector3 rewindStartPos)
        {
            _dashRewinder = dashRewinder;
            RewindStartPos = rewindStartPos;
        }

        public override void Rewind(Action completedCallback)
        {
            _dashRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke();
            });
        }
    }
}