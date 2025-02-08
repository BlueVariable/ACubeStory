using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BLUE.ACubeStory.MegaFiers;
using BLUE.Common;
using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStretchController : MonoBehaviour
    {
        [Serializable]
        public class StretchData
        {
            [field: SerializeField] public float Duration = 0.2f;

            [field: SerializeField] public FloatProviderBase DurationCoefProvider = null;
            
            [field: SerializeField] public AnimationCurve Curve = AnimationCurve.EaseInOut(0, -0.6f, 1, 0.7f);
        
            [field: SerializeField] public float CurveMultiplier = 1f;

            [field: SerializeField] public DirectionProviderBase TargetCubeFaceDirectionProvider = null;
        }
        
        [SerializeField] private DirectionBasedMegaModifierProvider _directionBasedMegaModifierProvider = null;
        
        private Dictionary<EDirection, (IEnumerator Routine, StretchData StretchData)> _stretchRoutines = new();

        public void ResetStretch()
        {
            foreach (DirectionBasedMegaModifierProvider.DirectionModifierInfo info in _directionBasedMegaModifierProvider.DirectionModifierMapColl)
            {
                MegaStretch stretchModifier = (MegaStretch)info.Modifier;

                stretchModifier.amount = 0;
                
                StopStretchRoutine(info.Direction);
            }
        }
        
        public void StartStretchRoutine(StretchData StretchData)
        {
            EDirection targetCubeFaceDirection = StretchData.TargetCubeFaceDirectionProvider.GetDirection();
            
            StopStretchRoutine(targetCubeFaceDirection);
            
            IEnumerator routine = StretchProgress(StretchData);
            
            _stretchRoutines.Add(targetCubeFaceDirection, (routine, StretchData));
            
            StartCoroutine(routine);
        }

        public void StopStretchRoutine(EDirection direction)
        {
            if (!_stretchRoutines.TryGetValue(direction, out (IEnumerator Routine, StretchData StretchData) routineData))
            {
                return;
            }

            if (routineData.Routine == null)
            {
                return;
            }
            
            StopCoroutine(routineData.Routine);
            
            _stretchRoutines.Remove(direction);
        }
        
        private IEnumerator StretchProgress(StretchData StretchData)
        {
            EDirection targetCubeFaceDirection = StretchData.TargetCubeFaceDirectionProvider.GetDirection();

            _directionBasedMegaModifierProvider.TryGetModifier(targetCubeFaceDirection, out MegaModifier modifier);

            MegaStretch stretchModifier = (MegaStretch)modifier;

            float elapsedTime = 0f;
            
            AnimationCurve newCurve = new AnimationCurve(StretchData.Curve.keys.ToArray());

            newCurve.keys[0].value = stretchModifier.amount;

            float duration = StretchData.DurationCoefProvider ? 
                StretchData.Duration * StretchData.DurationCoefProvider.GetValue() : 
                StretchData.Duration;
            
            while (elapsedTime < duration)
            {
                float normProgress = elapsedTime / duration;
                
                float value =  newCurve.Evaluate(normProgress) * StretchData.CurveMultiplier;
                
                stretchModifier.amount = value;
                
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }
            
            float finalValue = StretchData.Curve.Evaluate(1) * StretchData.CurveMultiplier;

            stretchModifier.amount = finalValue;
        }
    }
}