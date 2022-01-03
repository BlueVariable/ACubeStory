using System.Collections.Generic;
using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.Level;
using BLUE.ACubeStory.Merge;
using BLUE.InputSystem;
using BLUE.RewindSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Rewind_MergePuzzle : AbilityBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;
        
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private ECubeState _postRewindState = ECubeState.Idle;

        [SerializeField] private RewindController _selfRewindController = null;
        
        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;

        private List<RewindController> _mergeableRewindControllers = new List<RewindController>();
        
        public override void Attached()
        {
            RegisterToLevelFSM();

            RegisterToMergeableComposition();

            ActivateRewindables();
        }

        public override void Detached()
        {
            UnregisterFromLevelFSM();

            UnregisterFromMergeableComposition();

            UnregisterFromCubeFSM();

            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void OnDestroy()
        {
            UnregisterFromLevelFSM();
            
            UnregisterFromCubeFSM();

            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void RegisterToLevelFSM()
        {
            if (Level.Level.Instance == null)
            {
                return;
            }
            
            Level.Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState curState);

            if (curState == ELevelState.Merge)
            {
                OnLevelStateEntered(ELevelState.Merge);

                return;
            }
            
            Level.Level.Instance.LevelFSM.OnStateEntered += OnLevelStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited += OnLevelStateExited;
        }
        
        private void UnregisterFromLevelFSM()
        {
            if (Level.Level.Instance == null)
            {
                return;
            }
            
            Level.Level.Instance.LevelFSM.OnStateEntered -= OnLevelStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited -= OnLevelStateExited;
        }

        private void RegisterToMergeableComposition()
        {
            _mergeableComposition.OnMerged += OnMerged;
        }
        
        private void UnregisterFromMergeableComposition()
        {
            _mergeableComposition.OnMerged -= OnMerged;
        }

        private void RegisterToCubeFSM()
        {
            _cubeFSM.OnStateEntered += OnCubeStateEntered;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFSM.OnStateEntered -= OnCubeStateEntered;
        }

        private void ActivateRewindables()
        {
            IRewindable[] rewindables = _selfRewindController.GetComponentsInChildren<IRewindable>();

            foreach (IRewindable rewindable in rewindables)
            {
                rewindable.ActivateRewindable(_selfRewindController);
            }
        }

        private void DeactivateRewindables()
        {
            IRewindable[] rewindables = _selfRewindController.GetComponentsInChildren<IRewindable>();

            foreach (IRewindable rewindable in rewindables)
            {
                rewindable.DeactivateRewindable(_selfRewindController);
            }
        }
        
        private void OnLevelStateEntered(ELevelState levelState)
        {
            if (levelState.Equals(ELevelState.Merge))
            {
                UnregisterFromCubeFSM();
                
                RegisterToCubeFSM();
            }
        }
        
        private void OnLevelStateExited(ELevelState levelState)
        {
            if (levelState.Equals(ELevelState.Merge))
            {
                _selfRewindController.StopSaveSnapshotRoutine();
                
                _selfRewindController.ResetSnapshots();
                
                foreach (RewindController mergeableRewindController in _mergeableRewindControllers)
                {
                    mergeableRewindController.StopSaveSnapshotRoutine();
                
                    mergeableRewindController.ResetSnapshots();

                    IRewindable[] mergeableRewindables = mergeableRewindController.GetComponentsInChildren<IRewindable>();

                    foreach (IRewindable mergeableRewindable in mergeableRewindables)
                    {
                        mergeableRewindable.DeactivateRewindable(mergeableRewindController);
                    }
                }
                
                UnregisterFromLevelFSM();
            
                UnregisterFromCubeFSM();

                UnregisterFromInputManager();

                UnregisterFromRewindController();
            
                DeactivateRewindables();
            }
        }
        
        private void OnMerged(Mergeable mergeable)
        {
            RewindController mergeableRewindController = mergeable.GetComponentInChildren<RewindController>();

            if (mergeableRewindController == null)
            {
                return;
            }
            
            IRewindable[] rewindables = mergeableRewindController.GetComponentsInChildren<IRewindable>();

            foreach (IRewindable rewindable in rewindables)
            {
                rewindable.ActivateRewindable(mergeableRewindController);
            }
            
            mergeableRewindController.StopSaveSnapshotRoutine();
            
            mergeableRewindController.ResetSnapshots();
            
            mergeableRewindController.StartSaveSnapshotRoutine();

            if (!_mergeableRewindControllers.Contains(mergeableRewindController))
            {
                _mergeableRewindControllers.Add(mergeableRewindController);
            }
        }

        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Idle)
            {
                _selfRewindController.StopSaveSnapshotRoutine();
                
                _selfRewindController.ResetSnapshots();
                
                _selfRewindController.StartSaveSnapshotRoutine();
                
                UnregisterFromCubeFSM();

                RegisterToInputManager();
            }
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                _cubeFSM.TryChangeState(ECubeState.Rewind);
                
                _selfRewindController.StopSaveSnapshotRoutine();

                _mergeableRewindControllers.ForEach(val => val.StopSaveSnapshotRoutine());

                RegisterToRewindController();

                _mergeableComposition.TryUnmergeMergeablesExceptInitial();
                
                _selfRewindController.StartRewind();
                
                _mergeableRewindControllers.ForEach(val => val.StartRewind());
            }
        }
        
        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                UnregisterFromRewindController();
                
                _selfRewindController.StopRewind();
                
                _mergeableRewindControllers.ForEach(val => val.StopRewind());
            }
        }

        private void RegisterToRewindController()
        {
            _selfRewindController.OnRewindableRewound += OnRewindableRewound;
        }
        
        private void UnregisterFromRewindController()
        {
            _selfRewindController.OnRewindableRewound -= OnRewindableRewound;
        }

        private void OnRewindableRewound(IRewindable rewindable)
        {
            UnregisterFromRewindController();

            _selfRewindController.StopRewind();

            _cubeFSM.TryChangeState(_postRewindState);

            foreach (RewindController mergeableRewindController in _mergeableRewindControllers)
            {
                mergeableRewindController.StopRewind();
                
                mergeableRewindController.ResetSnapshots();

                IRewindable[] mergeableRewindables = mergeableRewindController.GetComponentsInChildren<IRewindable>();

                foreach (IRewindable mergeableRewindable in mergeableRewindables)
                {
                    mergeableRewindable.DeactivateRewindable(mergeableRewindController);
                }
            }
            
            _selfRewindController.ResetSnapshots();
            
            _mergeableRewindControllers.Clear();
                
            _selfRewindController.StartSaveSnapshotRoutine();
        }
    }
}