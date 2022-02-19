using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public abstract class CubeSnapshotFactory_RollBased : CubeSnapshotFactory
    {
        [SerializeField] protected float _rollSpeed = 5;

        [SerializeField] protected float _rollAngle = 90;

        [SerializeField] protected AnimationCurve _rollCurve = default;
        
        [SerializeField] protected Vector3 _rollPointOffset = Vector3.zero;
        
        [SerializeField] protected CubeSnapshotRewinder_Roll _rollRewinder = null;
    }
}