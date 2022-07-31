using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class PatrolBehaviourBase : MonoBehaviour
    {
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
        
        protected abstract void MoveToTargetPoint(PatrolPointBase patrolPoint, Action completedCallback);

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

        private void OnPatrolCompleted()
        {
            _prevTargetPoint = _curTargetPoint;
            _curTargetPoint = null;
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