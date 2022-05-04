using System;
using System.Collections;
using System.Collections.Generic;
using BLUE.Extensions;
using BLUE.PoolingSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BLUE.ACubeStory.Common
{
    public class FallingObjectSpawnerBehaviour : MonoBehaviour
    {
        [Serializable]
        public class FallSettings
        {
            [SerializeField] private Vector3 _fallFromOffset = default;
            public Vector3 FallFromOffset => _fallFromOffset;

            [SerializeField] private float _spawnRadius = 0f;
            public float SpawnRadius => _spawnRadius;

            [SerializeField] private float _minSpawnInterval = 0f;
            public float MinSpawnInterval => _minSpawnInterval;
            
            [SerializeField] private float _maxSpawnInterval = 0.5f;
            public float MaxSpawnInterval => _maxSpawnInterval;

            [SerializeField] private Pool_MB _fallingObjectPool = null;
            public Pool_MB FallingObjectPool => _fallingObjectPool;
        }
        
        [SerializeField] private Collider _fallArea = null;

        private IEnumerator _fallRoutine;
        private FallSettings _fallSettings;

        private Dictionary<FallingObjectBase, PoolObject> _fallingObjectMap =
            new Dictionary<FallingObjectBase, PoolObject>();
        
        public void StartFalling(FallSettings settings)
        {
            _fallSettings = settings;
            
            StopFalling();

            _fallRoutine = FallProgress();

            StartCoroutine(_fallRoutine);
        }

        public void StopFalling()
        {
            if (_fallRoutine != null)
            {
                StopCoroutine(_fallRoutine);
            }
        }

        private IEnumerator FallProgress()
        {
            while (true)
            {
                SpawnFallingObject();
                
                float randomInterval = Random.Range(
                    _fallSettings.MinSpawnInterval,
                    _fallSettings.MaxSpawnInterval);
                
                yield return new WaitForSeconds(randomInterval);
            }
        }

        private void SpawnFallingObject()
        {
            Vector3 fallPosition = GetRandomFallPos();

            Vector3 spawnPosition = GetRandomSpawnPos(fallPosition);

            PoolObject poolObject = _fallSettings.FallingObjectPool.AllocatePoolObject();
            
            poolObject.transform.position = spawnPosition;

            FallingObjectBase fallingObject = poolObject.GetComponentInChildren<FallingObjectBase>();

            if (fallingObject == null)
            {
                return;
            }
            
            _fallingObjectMap.Add(fallingObject, poolObject);

            fallingObject.OnCompleted += OnCompleted;
            
            fallingObject.Fall(fallPosition);
        }

        private void OnCompleted(FallingObjectBase fallingObject)
        {
            fallingObject.OnCompleted -= OnCompleted;

            PoolObject poolObject = _fallingObjectMap[fallingObject];
            
            _fallSettings.FallingObjectPool.DeallocatePoolObject(poolObject);

            _fallingObjectMap.Remove(fallingObject);
        }

        private Vector3 GetRandomSpawnPos(Vector3 fallPosition)
        {
            Vector3 spawnPos = fallPosition + _fallSettings.FallFromOffset;

            Vector3 randomOffset = Random.insideUnitSphere * _fallSettings.SpawnRadius;

            spawnPos += randomOffset;

            return spawnPos;
        }

        private Vector3 GetRandomFallPos()
        {
            return _fallArea.bounds.GetRandomPointInBounds();
        }
    }
}