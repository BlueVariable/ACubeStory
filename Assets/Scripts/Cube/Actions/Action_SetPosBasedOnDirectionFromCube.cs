using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_SetPosBasedOnDirectionFromCube : ActionBase
    {
        [SerializeField] private Transform _origin = null;
        
        [SerializeField] private Transform _target = null;
        
        [SerializeField] private Vector3 _targetAxis = new Vector3(1, 0, 1);
        
        [SerializeField] private float _strength = 1.3f;
        
        [SerializeField] private bool _behaveAsOffset = true;
        
        [SerializeField] private bool _forcePositionUpdateOnThresholdSatisfaction = true;
            
        [SerializeField] private float _minDirectionThreshold = 0.1f;
        
        public override void Execute()
        {
            Vector3 cubePosition = Cube.Instance.GetPosition();

            Vector3 dir = _origin.position - cubePosition;

            dir.Scale(_targetAxis);

            Vector3 pos = dir * _strength;

            if (_forcePositionUpdateOnThresholdSatisfaction 
                && Mathf.Abs(dir.x) < _minDirectionThreshold 
                && Mathf.Abs(dir.y) < _minDirectionThreshold 
                && Mathf.Abs(dir.z) < _minDirectionThreshold)
            {
                int multiplierX = Random.Range(-1, 2);
                int multiplierY = Random.Range(-1, 2);
                int multiplierZ = Random.Range(-1, 2);
                
                pos = new Vector3(_strength * multiplierX, _strength * multiplierY, _strength * multiplierZ);

                pos.Scale(_targetAxis);
            }
            
            if (_behaveAsOffset)
            {
                _target.localPosition += pos;
            }
            else
            {
                _target.localPosition = pos;
            }
        }
    }
}