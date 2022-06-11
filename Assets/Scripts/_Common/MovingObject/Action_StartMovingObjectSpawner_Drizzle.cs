using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class Action_StartMovingObjectSpawner_Drizzle : InterruptableActionBase
    {
        [SerializeField] private MovingObjectSpawner_Drizzle[] _movingObjectSpawners = null;

        [SerializeField] private MovingObjectSpawner_Drizzle.SpawnSettings _spawnSettings = null;

        [SerializeField] private float _delayBtwSpawners = 0f;

        private IEnumerator _spawnRoutine;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            StartSpawnRoutine();
        }

        public override void Interrupt()
        {
            StopSpawnRoutine();
        }

        private void StartSpawnRoutine()
        {
            StopSpawnRoutine();

            _spawnRoutine = SpawnProgress();

            StartCoroutine(_spawnRoutine);
        }
        
        private void StopSpawnRoutine()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
            }
        }

        private IEnumerator SpawnProgress()
        {
            foreach (MovingObjectSpawner_Drizzle spawner in _movingObjectSpawners)
            {
                spawner.StartSpawning(_spawnSettings);

                if (_delayBtwSpawners > 0)
                {
                    yield return new WaitForSeconds(_delayBtwSpawners);
                }
            }
            
            OnCompleted?.Invoke(this);
        }
    }
}