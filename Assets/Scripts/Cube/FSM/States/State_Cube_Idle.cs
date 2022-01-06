using UnityEngine;
using BLUE.FSMSystem;
using BLUE.GrassScripts;
using Unity.Mathematics;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Idle : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [SerializeField] private LayerMask _groundLayerMask = default;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Idle;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube transitionMessage)
        {
            TryGrassStep();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            TryFreeGrass();
            
            base.ExitStateCustomActions();
        }

        private void TryGrassStep()
        {
            if (TryGetGrass(out Grass grass))
            {
                grass.SteppedOnGrass();
            }
        }

        private void TryFreeGrass()
        {
            if (TryGetGrass(out Grass grass))
            {
                grass.GrassFreed();
            }
        }

        private bool TryGetGrass(out Grass grass)
        {
            RaycastHit hit;

            grass = null;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000, _groundLayerMask))
            {
                grass = hit.collider.GetComponent<Grass>();
                
                return grass != null;
            }

            return false;
        }
    }
}