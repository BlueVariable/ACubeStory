using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Fall : RewindSnapshot_Cube_Movement
    {
        public RewindSnapshot_Cube_Fall(
            ECubeState cubeState, 
            CubeSnapshotRewinder_Movement movementRewinder, 
            Vector3 targetPosition, 
            MoveBehaviour_Tween moveBehaviour,
            MoveBehaviour_Tween.TweenMoveSettings moveSettingsDefault,
            MoveBehaviour_Tween.TweenMoveSettings moveSettingsHalfway,
            State_Cube_Fall fallState) 
            : base(cubeState, movementRewinder, moveBehaviour, targetPosition, moveSettingsDefault)
        {
            void onFallFinished()
            {
                fallState.OnFallFinished -= onFallFinished;

                MoveSettings = moveSettingsDefault;
            }
            
            fallState.OnFallFinished += onFallFinished;

            MoveSettings = moveSettingsHalfway;
        }
    }
}