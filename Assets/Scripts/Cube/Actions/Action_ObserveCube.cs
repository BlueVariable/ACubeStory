using System;
using System.Collections;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_ObserveCube : InterruptableActionBase
    {
        [Serializable]
        private class ObserveInfo
        {
            [field: SerializeField] public Transform Observer { get; private set; }
            [field: SerializeField] public RotationBehaviour_LerpAngle RotationBehaviour { get; private set; }
            [field: SerializeField] public RotationBehaviour_LerpAngle.RotationSettings RotSettings { get; private set; }
            [field: SerializeField] public float PreObserveDuration { get; private set; }
            
            [field: SerializeField] public float ObserveDuration { get; private set; }
        }

        [SerializeField] private ObserveInfo[] _observeInfoColl = null;

        private List<IEnumerator> _observeRoutines = new List<IEnumerator>();

        private List<GameObject> _previousTargetObjects = new List<GameObject>();

        private int _traversedObserverCount = 0;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            StartObserveRoutines();
        }

        public override void Interrupt()
        {
            StopObserveRoutines();
        }
        
        private void StartObserveRoutines()
        {
            StopObserveRoutines();

            for (int i = 0; i < _observeInfoColl.Length; i++)
            {
                IEnumerator observeRoutine = ObserveProgress(_observeInfoColl[i]);

                _observeRoutines.Add(observeRoutine);

                StartCoroutine(observeRoutine);
            }
        }

        private void StopObserveRoutines()
        {
            foreach (IEnumerator observeRoutine in _observeRoutines)
            {
                if (observeRoutine != null)
                {
                    StopCoroutine(observeRoutine);
                }
            }
            
            _observeRoutines.Clear();

            for (int i = 0; i < _previousTargetObjects.Count; i++)
            {
                Destroy(_previousTargetObjects[i]);
            }
            
            _previousTargetObjects.Clear();
        }

        private IEnumerator ObserveProgress(ObserveInfo observeInfo)
        {
            yield return new WaitForSeconds(observeInfo.PreObserveDuration);

            bool isRotatedTowardsCube = false;
            
            void onRotationCompletedTowardsCube(bool result)
            {
                isRotatedTowardsCube = true;
            }

            GameObject gameObject = new GameObject();

            gameObject.transform.position = observeInfo.Observer.position + observeInfo.Observer.forward;

            _previousTargetObjects.Add(gameObject);
            
            observeInfo.RotationBehaviour.StartRotation(observeInfo.RotSettings, Cube.Instance.transform,
                onRotationCompletedTowardsCube);
            
            yield return new WaitUntil(() => isRotatedTowardsCube);

            yield return new WaitForSeconds(observeInfo.ObserveDuration);

            bool isRotationRestored = false;
            
            void onRestoredOldRotation(bool result)
            {
                isRotationRestored = true;
            }
            
            observeInfo.RotationBehaviour.StartRotation(observeInfo.RotSettings, gameObject.transform,
                onRestoredOldRotation);
            
            yield return new WaitUntil(() => isRotationRestored);

            _traversedObserverCount++;

            CheckCompletion();
        }

        private void CheckCompletion()
        {
            if (_traversedObserverCount == _observeRoutines.Count)
            {
                OnCompleted?.Invoke(this);
            }
        }
    }
}