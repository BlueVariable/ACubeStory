using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class Action_ActivateMovingObjectSpawner_Drizzle : ActionBase
    {
        [SerializeField] private MovingObjectSpawner_Drizzle _movingObjectSpawner = null;

        [SerializeField] private MovingObjectSpawner_Drizzle.SpawnSettings _spawnSettings = null;
        
        public override void Execute()
        {
            _movingObjectSpawner.StartSpawning(_spawnSettings);
        }
    }
}