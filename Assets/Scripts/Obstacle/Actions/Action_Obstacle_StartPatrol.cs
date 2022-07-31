using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Obstacle.Actions
{
    public class Action_Obstacle_StartPatrol : ActionBase
    {
        [SerializeField] private ObstaclePatrolController _obstaclePatrolController = null;
        
        public override void Execute()
        {
            _obstaclePatrolController.StartPatrolling();
        }
    }
}