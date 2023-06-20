using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public abstract class Action_ExecuteCubeStateBase<T> : InterruptableActionBase
        where T : FSMTransitionMessage_Cube
    {
        [Serializable]
        private class StateTransitionInfo
        {
            [field: SerializeField] public T Message { get; private set; } = default;
        
            [field: SerializeField] public float PostDelay { get; private set; } = 0;
        }
        
        [SerializeField] private StateTransitionInfo[] _stateTransitionInfoColl = default;

        private IEnumerator _stateExecutionRoutine;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }
        
        #endregion
        
        protected abstract ECubeState[] GetTargetStates();
        
        public override void Execute()
        {
            StartStateExecutionRoutine();
        }

        public override void Interrupt()
        {
            StopStateExecutionRoutine();
        }

        private void OnDestroy()
        {
            StopStateExecutionRoutine();
        }

        private void StartStateExecutionRoutine()
        {
            StopStateExecutionRoutine();

            _stateExecutionRoutine = StateExecutionProgress();

            StartCoroutine(_stateExecutionRoutine);
        }

        private void StopStateExecutionRoutine()
        {
            if (_stateExecutionRoutine != null)
            {
                StopCoroutine(_stateExecutionRoutine);

                _stateExecutionRoutine = null;
            }
        }

        private IEnumerator StateExecutionProgress()
        {
            foreach (StateTransitionInfo transitionInfo in _stateTransitionInfoColl)
            {
                while (!TryChangeState(transitionInfo))
                {
                    yield return null;
                }

                if (transitionInfo.PostDelay <= 0)
                {
                    continue;
                }

                yield return new WaitForSeconds(transitionInfo.PostDelay);
            }

            OnCompleted?.Invoke(this);
        }

        private bool TryChangeState(StateTransitionInfo transitionInfo)
        {
            foreach (ECubeState cubeState in GetTargetStates())
            {
                bool isSuccess = Cube.Instance.CubeFSM.TryChangeState(cubeState,
                    transitionInfo.Message);

                if (isSuccess)
                {
                    return true;
                }
            }
            
            return false;
        }
    }

}