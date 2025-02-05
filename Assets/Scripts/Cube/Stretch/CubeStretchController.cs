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

            [field: SerializeField] public DirectionBasedMegaModifierProvider ModifierProvider = null;

            [field: SerializeField] public DirectionProviderBase TargetCubeFaceDirectionProvider = null;
        }
        
        private Dictionary<MegaModifier, (IEnumerator Routine, StretchData StretchData)> _stretchRoutines = new();
        
        public void StartStretchRoutine(StretchData StretchData)
        {
            EDirection targetCubeFaceDirection = StretchData.TargetCubeFaceDirectionProvider.GetDirection();

            if (!StretchData.ModifierProvider.TryGetModifier(targetCubeFaceDirection, out MegaModifier bendModifier))
            {
                Debug.Log("CubeStretchController: No modifier found for direction: " + targetCubeFaceDirection);
                
                return;
            }
            
            StopStretchRoutine(bendModifier);
            
            IEnumerator routine = StretchProgress(StretchData);
            
            _stretchRoutines.Add(bendModifier, (routine, StretchData));
            
            StartCoroutine(routine);
        }

        public void StopStretchRoutine(MegaModifier bend)
        {
            if (!_stretchRoutines.TryGetValue(bend, out (IEnumerator Routine, StretchData StretchData) routineData))
            {
                return;
            }

            if (routineData.Routine == null)
            {
                return;
            }
            
            StopCoroutine(routineData.Routine);
            
            _stretchRoutines.Remove(bend);
        }
        
        private IEnumerator StretchProgress(StretchData StretchData)
        {
            EDirection targetCubeFaceDirection = StretchData.TargetCubeFaceDirectionProvider.GetDirection();

            StretchData.ModifierProvider.TryGetModifier(targetCubeFaceDirection, out MegaModifier modifier);

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