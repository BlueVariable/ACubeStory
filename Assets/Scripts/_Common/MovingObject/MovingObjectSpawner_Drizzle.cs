using System;
using System.Collections;
using System.Collections.Generic;
using BLUE.Extensions;
using BLUE.PoolingSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BLUE.ACubeStory.Common
{
    public class MovingObjectSpawner_Drizzle : MonoBehaviour
    {
        [Serializable]
        public class SpawnSettings
        {
            [SerializeField] private Vector3 _moveFromOffset = default;
            public Vector3 MoveFromOffset => _moveFromOffset;

            [SerializeField] private float _spawnRadius = 0f;
            public float SpawnRadius => _spawnRadius;
            
            [SerializeField] private int _spawnAmount = 1;
            public int SpawnAmount => _spawnAmount;
            
            [SerializeField] private float _minSpawnInterval = 0f;
            public float MinSpawnInterval => _minSpawnInterval;
            
            [SerializeField] private float _maxSpawnInterval = 0.5f;
            public float MaxSpawnInterval => _maxSpawnInterval;

            [SerializeField] private Pool_MB _movingObjectPool = null;
            public Pool_MB MovingObjectPool => _movingObjectPool;
        }
        
        [SerializeField] private Collider _targetArea = null;

        private IEnumerator _spawnRoutine;
        private SpawnSettings _spawnSettings;

        private Dictionary<MovingObjectBase, PoolObject> _movingObjectMap =
            new Dictionary<MovingObjectBase, PoolObject>();

        #region Events

        public Action<MovingObjectBase> OnMovingObjectSequenceCompleted { get; set; }

        #endregion

        public void StartSpawning(SpawnSettings settings)
        {
            _spawnSettings = settings;
            
            StopSpawning();

            _spawnRoutine = SpawnProgress();

            StartCoroutine(_spawnRoutine);
        }

        public void StopSpawning()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
            }
        }

        private IEnumerator SpawnProgress()
        {
            while (true)
            {
                for (int i = 0; i < _spawnSettings.SpawnAmount; i++)
                {
                    SpawnMovingObject();
                }
                
                float randomInterval = Random.Range(
                    _spawnSettings.MinSpawnInterval,
                    _spawnSettings.MaxSpawnInterval);
                
                yield return new WaitForSeconds(randomInterval);
            }
        }

        private void SpawnMovingObject()
        {
            Vector3 targetPosition = GetRandomTargetPos();

            Vector3 spawnPosition = GetRandomSpawnPos(targetPosition);

            PoolObject poolObject = _spawnSettings.MovingObjectPool.AllocatePoolObject();
            
            poolObject.transform.position = spawnPosition;

            MovingObjectBase movingObject = poolObject.GetComponentInChildren<MovingObjectBase>();

            if (movingObject == null)
            {
                return;
            }
            
            _movingObjectMap.Add(movingObject, poolObject);

            movingObject.OnSequenceCompleted += OnSequenceCompleted;
            
            movingObject.Move(targetPosition);
        }

        private void OnSequenceCompleted(MovingObjectBase movingObject)
        {
            movingObject.OnSequenceCompleted -= OnSequenceCompleted;

            PoolObject poolObject = _movingObjectMap[movingObject];
            
            OnMovingObjectSequenceCompleted?.Invoke(movingObject);

            _spawnSettings.MovingObjectPool.DeallocatePoolObject(poolObject);

            _movingObjectMap.Remove(movingObject);
        }

        private Vector3 GetRandomSpawnPos(Vector3 targetPosition)
        {
            Vector3 spawnPos = targetPosition + _spawnSettings.MoveFromOffset;

            Vector3 randomOffset = Random.insideUnitSphere * _spawnSettings.SpawnRadius;

            spawnPos += randomOffset;

            return spawnPos;
        }

        private Vector3 GetRandomTargetPos()
        {
            return _targetArea.bounds.GetRandomPointInBounds();
        }
    }
}