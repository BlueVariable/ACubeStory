using System;
using System.Collections;
using UnityEngine;

namespace BLUE.ACubeStory.Grass
{
    public class Grass : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer = null;

        [SerializeField] private int _grassMaterialIndex = 0;

        [SerializeField] private float _grassSmashDuration = 0.25f;

        [SerializeField] private float _grassRecoveryDuration = 1f;

        [SerializeField] private string _heightPropertyId = "";
        
        private MaterialPropertyBlock _mpb;
        private MaterialPropertyBlock _Mpb
        {
            get
            {
                if (_mpb == null)
                {
                    MaterialPropertyBlock mpb = new MaterialPropertyBlock();

                    _meshRenderer.GetPropertyBlock(mpb, _grassMaterialIndex);

                    _mpb = mpb;

                    _mpb.SetFloat(_heightPropertyId, _meshRenderer.materials[_grassMaterialIndex].GetFloat(_heightPropertyId));
                }

                return _mpb;
            }
        }

        private bool _smashing = false;
        
        private IEnumerator _grassSmashRoutine;
        private IEnumerator _grassStepRoutine;
        
        private Action _onSmashCompleted { get; set; }
        
        public void SmashGrass()
        {
            StopGrassFreeRoutine();
            
            StartGrassSmashRoutine();
            
            _onSmashCompleted -= OnSmashCompleted;
        }

        public void RecoveryGrass(bool waitForSmash = false)
        {
            if (waitForSmash && _smashing)
            {
                _onSmashCompleted += OnSmashCompleted;
                    
                return;
            }

            StopGrassSmashRoutine();
            
            StartGrassFreeRoutine();
        }
        
        private void OnSmashCompleted()
        {
            _onSmashCompleted -= OnSmashCompleted;
            
            StartGrassFreeRoutine();
        }

        private void StartGrassSmashRoutine()
        {
            StopGrassSmashRoutine();

            _grassSmashRoutine = GrassSmashProgress();

            StartCoroutine(_grassSmashRoutine);
        }

        private void StopGrassSmashRoutine()
        {
            if (_grassSmashRoutine != null)
            {
                StopCoroutine(_grassSmashRoutine);
            }
        }

        private IEnumerator GrassSmashProgress()
        {
            _smashing = true;
            
            float timePassed = 0;

            float startingHeight = _Mpb.GetFloat(_heightPropertyId);
            
            while (timePassed < _grassSmashDuration)
            {
                timePassed += Time.deltaTime;

                float height = Mathf.Lerp(startingHeight, 0, timePassed / _grassSmashDuration);
                
                SetGrassHeight(height);
                
                yield return null;
            }
            
            _smashing = false;
            
            _onSmashCompleted?.Invoke();
        }

        private void StartGrassFreeRoutine()
        {
            StopGrassFreeRoutine();

            _grassStepRoutine = GrassFreeProgress();

            StartCoroutine(_grassStepRoutine);
        }

        private void StopGrassFreeRoutine()
        {
            if (_grassStepRoutine != null)
            {
                StopCoroutine(_grassStepRoutine);
            }
        }

        private IEnumerator GrassFreeProgress()
        {
            yield return new WaitForSeconds(_grassRecoveryDuration);
            
            float timePassed = 0;

            float startingHeight = _Mpb.GetFloat(_heightPropertyId);
            
            while (timePassed < _grassSmashDuration)
            {
                timePassed += Time.deltaTime;

                float height = Mathf.Lerp(startingHeight, 1, timePassed / _grassSmashDuration);
                
                SetGrassHeight(height);
                
                yield return null;
            }
        }

        private void SetGrassHeight(float val)
        {
            val = Mathf.Clamp(val, 0, 1);
            
            _Mpb.SetFloat(_heightPropertyId, val);
            
            _meshRenderer.SetPropertyBlock(_Mpb, _grassMaterialIndex);
        }
    }
}