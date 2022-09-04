using System;
using DG.Tweening;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class RotationBehaviour_Tween : MonoBehaviour
    {
        [Serializable]
        public struct RotationSettings
        {
            [field: SerializeField] public Transform RotationTransform { get; private set; }
        
            [field: SerializeField] public float RotationDuration { get; private set; }
        
            [field: SerializeField] public Ease RotationEase { get; private set; }
        }
        
        private Tween _rotationTween;
        
        public void StartRotation(
            RotationSettings rotationSettings,
            Quaternion targetQuaternion,
            Action completedCallback = null)
        {
            StopRotation();

            _rotationTween =
                rotationSettings.RotationTransform.DORotateQuaternion(
                    targetQuaternion,
                    rotationSettings.RotationDuration)
                    .SetEase(rotationSettings.RotationEase)
                    .OnComplete(() => 
                    {
                        completedCallback?.Invoke();
                    });
        }

        public void StopRotation()
        {
            if (_rotationTween != null)
            {
                _rotationTween.Kill();
            }
        }
    }
}