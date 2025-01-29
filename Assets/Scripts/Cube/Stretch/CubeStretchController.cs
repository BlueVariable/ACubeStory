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

            [field: SerializeField] public AnimationCurve Curve = AnimationCurve.EaseInOut(0, -0.6f, 1, 0.7f);
        
            [field: SerializeField] public float CurveMultiplier = 1f;

            [field: SerializeField] public DirectionBasedMegaModifierProvider ModifierProvider = null;

            [field: SerializeField] public DirectionProviderBase TargetCubeFaceDirectionProvider = null;
        }
        
        #region Constants

        private const float COMPARISON_VALUE = 0.9f;
        
        #endregion
        
        private Dictionary<MegaModifier, (IEnumerator Routine, StretchData StretchData)> _bendRoutines = new();
        
        public void StartStretchRoutine(StretchData StretchData)
        {
            EDirection targetCubeFaceDirection = StretchData.TargetCubeFaceDirectionProvider.GetDirection();

            if (!StretchData.ModifierProvider.TryGetModifier(targetCubeFaceDirection, out MegaModifier bendModifier))
            {
                Debug.Log("CubeBendController: No modifier found for direction: " + targetCubeFaceDirection);
                
                return;
            }
            
            StopStretchRoutine(bendModifier);
            
            IEnumerator routine = BendProgress(StretchData);
            
            _bendRoutines.Add(bendModifier, (routine, StretchData));
            
            StartCoroutine(routine);
        }

        public void StopStretchRoutine(MegaModifier bend)
        {
            if (!_bendRoutines.TryGetValue(bend, out (IEnumerator Routine, StretchData StretchData) routineData))
            {
                return;
            }

            if (routineData.Routine == null)
            {
                return;
            }
            
            StopCoroutine(routineData.Routine);
            
            _bendRoutines.Remove(bend);
        }
        
        private IEnumerator BendProgress(StretchData StretchData)
        {
            EDirection targetCubeFaceDirection = StretchData.TargetCubeFaceDirectionProvider.GetDirection();

            StretchData.ModifierProvider.TryGetModifier(targetCubeFaceDirection, out MegaModifier modifier);

            MegaStretch stretchModifier = (MegaStretch)modifier;

            float elapsedTime = 0f;

            AnimationCurve newCurve = new AnimationCurve(StretchData.Curve.keys.ToArray());

            newCurve.keys[0].value = stretchModifier.amount;
            
            while (elapsedTime < StretchData.Duration)
            {
                float normProgress = elapsedTime / StretchData.Duration;
                
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