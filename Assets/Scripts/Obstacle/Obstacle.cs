using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private bool _activateAwake = false;

        [SerializeField] private ActionExecutor _obstacleActivatedActions = null;

        [SerializeField] private ActionExecutor _obstacleDeactivatedActions = null;
        
        private void Awake()
        {
            if (_activateAwake)
            {
               ActivateObstacle();     
            }
        }

        public void ActivateObstacle()
        {
            _obstacleActivatedActions?.Execute();
        }

        public void DeactivateObstacle()
        {
            _obstacleDeactivatedActions?.Execute();
        }
    }
}