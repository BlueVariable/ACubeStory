using System;
using System.Collections;
using UnityEngine;

namespace BLUE.ACubeStory.Grass
{
    public class Grass : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer = null;

        [SerializeField] private int _grassMaterialIndex = 0;

        [SerializeField] private float _grassTrampleDuration = 0.25f;

        [SerializeField] private float _grassRecoveryDuration = 1f;

        [SerializeField] private string _heightPropertyID = "_GrassHeight";
        
        private MaterialPropertyBlock _propertyBlock;
        private MaterialPropertyBlock _PropertyBlock
        {
            get
            {
                if (_propertyBlock == null)
                {
                    MaterialPropertyBlock mpb = new MaterialPropertyBlock();

                    _meshRenderer.GetPropertyBlock(mpb, _grassMaterialIndex);

                    _propertyBlock = mpb;

                    _propertyBlock.SetFloat(_heightPropertyID, _meshRenderer.materials[_grassMaterialIndex].GetFloat(_heightPropertyID));
                }

                return _propertyBlock;
            }
        }

        private bool _trampling = false;
        
        private IEnumerator _grassTrampleRoutine;
        private IEnumerator _grassRecoveryRoutine;

        private Action _onTrampleCompleted;
        
        public void TrampleGrass()
        {
            StopGrassRecoveryRoutine();
            
            StartGrassTrampleRoutine();
            
            _onTrampleCompleted -= OnTrampleCompleted;
        }

        public void RecoverGrass(bool waitForTrampleCompletion = false)
        {
            if (waitForTrampleCompletion && _trampling)
            {
                _onTrampleCompleted += OnTrampleCompleted;
                    
                return;
            }

            StopGrassTrampleRoutine();
            
            StartGrassRecoveryRoutine();
        }
        
        private void OnTrampleCompleted()
        {
            _onTrampleCompleted -= OnTrampleCompleted;
            
            StartGrassRecoveryRoutine();
        }

        private void StartGrassTrampleRoutine()
        {
            StopGrassTrampleRoutine();

            _grassTrampleRoutine = GrassTrampleProgress();

            StartCoroutine(_grassTrampleRoutine);
        }

        private void StopGrassTrampleRoutine()
        {
            if (_grassTrampleRoutine != null)
            {
                StopCoroutine(_grassTrampleRoutine);
            }
        }

        private IEnumerator GrassTrampleProgress()
        {
            _trampling = true;
            
            float timePassed = 0;

            float startHeight = _PropertyBlock.GetFloat(_heightPropertyID);
            
            while (timePassed < _grassTrampleDuration)
            {
                timePassed += Time.deltaTime;

                float height = Mathf.Lerp(startHeight, 0, timePassed / _grassTrampleDuration);
                
                SetGrassHeight(height);
                
                yield return null;
            }
            
            _trampling = false;
            
            _onTrampleCompleted?.Invoke();
        }

        private void StartGrassRecoveryRoutine()
        {
            StopGrassRecoveryRoutine();

            _grassRecoveryRoutine = GrassRecoveryProgress();

            StartCoroutine(_grassRecoveryRoutine);
        }

        private void StopGrassRecoveryRoutine()
        {
            if (_grassRecoveryRoutine != null)
            {
                StopCoroutine(_grassRecoveryRoutine);
            }
        }

        private IEnumerator GrassRecoveryProgress()
        {
            yield return new WaitForSeconds(_grassRecoveryDuration);
            
            float timePassed = 0;

            float startHeight = _PropertyBlock.GetFloat(_heightPropertyID);
            
            while (timePassed < _grassTrampleDuration)
            {
                timePassed += Time.deltaTime;

                float height = Mathf.Lerp(startHeight, 1, timePassed / _grassTrampleDuration);
                
                SetGrassHeight(height);
                
                yield return null;
            }
        }

        private void SetGrassHeight(float val)
        {
            val = Mathf.Clamp(val, 0, 1);
            
            _PropertyBlock.SetFloat(_heightPropertyID, val);
            
            _meshRenderer.SetPropertyBlock(_PropertyBlock, _grassMaterialIndex);
        }
    }
}