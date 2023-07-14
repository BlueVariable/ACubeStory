using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Fail
{
    public class FailManager : MonoBehaviour
    {
        #region Singleton

        private static FailManager _instance;
        public static FailManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<FailManager>();
                }

                return _instance;
            }
        }

        #endregion
        
        private readonly List<IFailController> _failControllers = new();
        
        private readonly List<IFailObserver> _failObservers = new();
        
        #region Events

        public Action<IFailController> OnFailed { get; set; }

        #endregion

        public void RegisterFailController(IFailController failController)
        {
            if (!_failControllers.Contains(failController))
            {
                _failControllers.Add(failController);

                failController.OnFailed += OnFailTriggered;
            }
        }
        
        public void UnregisterFailController(IFailController failController)
        {
            _failControllers.Remove(failController);
            
            failController.OnFailed -= OnFailTriggered;
        }

        private void OnFailTriggered(IFailController failController)
        {
            OnFailed?.Invoke(failController);

            _failObservers.ForEach(val => val.FailObserved(failController));
        }
        
        public void RegisterFailObserver(IFailObserver observer)
        {
            if (!_failObservers.Contains(observer))
            {
                _failObservers.Add(observer);
            }
        }
        
        public void UnregisterFailObserver(IFailObserver observer)
        {
            _failObservers.Remove(observer);
        }
    }
}