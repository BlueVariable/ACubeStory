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
        
        private IEnumerator _grassStepRoutine;
        private IEnumerator _grassFreeRoutine;
        
        public void SteppedOnGrass()
        {
            SetGrassHeight(0);
            
            // StopGrassFreeRoutine();
            
            // StartGrassStepRoutine();
        }

        public void GrassFreed()
        {
            StopGrassStepRoutine();
            
            StartGrassFreeRoutine();
        }

        private void StartGrassStepRoutine()
        {
            StopGrassStepRoutine();

            _grassStepRoutine = GrassStepProgress();

            StartCoroutine(_grassStepRoutine);
        }

        private void StopGrassStepRoutine()
        {
            if (_grassStepRoutine != null)
            {
                StopCoroutine(_grassStepRoutine);
            }
        }

        private IEnumerator GrassStepProgress()
        {
            float timePassed = 0;

            float startingHeight = _Mpb.GetFloat(_heightPropertyId);
            
            while (timePassed < _grassSmashDuration)
            {
                timePassed += Time.deltaTime;

                float height = Mathf.Lerp(startingHeight, 0, timePassed / _grassSmashDuration);
                
                SetGrassHeight(height);
                
                yield return null;
            }
        }

        private void StartGrassFreeRoutine()
        {
            StopGrassFreeRoutine();

            _grassFreeRoutine = GrassFreeProgress();

            StartCoroutine(_grassFreeRoutine);
        }

        private void StopGrassFreeRoutine()
        {
            if (_grassFreeRoutine != null)
            {
                StopCoroutine(_grassFreeRoutine);
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