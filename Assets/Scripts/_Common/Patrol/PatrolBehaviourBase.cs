using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class PatrolBehaviourBase : MonoBehaviour
    {
        public bool IsPatrolling => _curTargetPoint != null;
        
        private List<PatrolPointBase> _patrolPoints;

        private List<PatrolPointBase> _PatrolPoints
        {
            get
            {
                if (_patrolPoints == null)
                {
                    _patrolPoints = GetComponentsInChildren<PatrolPointBase>().ToList();
                }

                return _patrolPoints;
            }
        }

        private PatrolPointBase _curTargetPoint;
        private PatrolPointBase _prevTargetPoint;

        private IEnumerator _patrolRoutine;
        
        protected abstract void MoveToTargetPoint(PatrolPointBase patrolPoint, Action completedCallback);
        protected abstract bool TryStopMoving();

        public void GoToNextPatrolPoint()
        {
            if (_PatrolPoints.Count == 0)
            {
                throw new Exception($"Could not found any patrol point!");
            }

            if (_curTargetPoint != null)
            {
                return;
            }

            _curTargetPoint = GetNextPatrolPoint(_prevTargetPoint);
            
            MoveToTargetPoint(_curTargetPoint, OnPatrolCompleted);
        }

        public void StartPatrolLoop()
        {
            CancelPatrol();

            _patrolRoutine = PatrolProgress();

            StartCoroutine(_patrolRoutine);
        }

        public void CancelPatrol(bool forceStop = true)
        {
            if (forceStop)
            {
                TryStopMoving();
            }

            if (_patrolRoutine != null)
            {
                StopCoroutine(_patrolRoutine);
            }
        }

        private void OnPatrolCompleted()
        {
            _prevTargetPoint = _curTargetPoint;
            _curTargetPoint = null;
        }

        private IEnumerator PatrolProgress()
        {
            while (true)
            {
                yield return new WaitUntil(() => !IsPatrolling);
                
                GoToNextPatrolPoint();
            }
        }

        private PatrolPointBase GetNextPatrolPoint(PatrolPointBase patrolPoint)
        {
            if (patrolPoint == null)
            {
                return _PatrolPoints[0];
            }

            int patrolPointIndex = _PatrolPoints.IndexOf(patrolPoint);

            if (patrolPointIndex == -1 || patrolPointIndex == _PatrolPoints.Count - 1)
            {
                return _PatrolPoints[0];
            }

            return _PatrolPoints[patrolPointIndex + 1];
        }
    }
}