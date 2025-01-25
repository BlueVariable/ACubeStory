using System;
using System.Collections;
using System.Collections.Generic;
using BLUE.ACubeStory.MegaFiers;
using BLUE.Common;
using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeBendController : MonoBehaviour
    {
        private Dictionary<MegaModifier, (IEnumerator Routine, BendData BendData)> _bendRoutines = 
            new Dictionary<MegaModifier, (IEnumerator Routine, BendData BendData)>();

        [Serializable]
        public class BendData
        {
            [field:SerializeField] public float Duration = 0.2f;
        
            [field:SerializeField] public AnimationCurve Curve = AnimationCurve.EaseInOut(0, -30, 1, 30);
        
            [field:SerializeField] public float CurveMultiplier = 1f;

            [field:SerializeField] public DirectionBasedMegaModifierProvider ModifierProvider = null;

            [field:SerializeField] public DirectionProviderBase TargetCubeFaceDirectionProvider = null;

            [field:SerializeField] public DirectionProviderBase BendDirectionProvider = null;
        }
        
        public void StartBend(BendData bendData)
        {
            EDirection targetCubeFaceDirection = bendData.TargetCubeFaceDirectionProvider.GetDirection();

            if (!bendData.ModifierProvider.TryGetModifier(targetCubeFaceDirection, out MegaModifier bendModifier))
            {
                Debug.Log("CubeBendController: No modifier found for direction: " + targetCubeFaceDirection);
                return;
            }
            
            StopBend(bendModifier);
            
            IEnumerator routine = BendProgress(bendData);
            
            _bendRoutines.Add(bendModifier, (routine, bendData));
            
            StartCoroutine(routine);
        }

        public void StopBend(MegaModifier bend)
        {
            if (!_bendRoutines.TryGetValue(bend, out (IEnumerator Routine, BendData BendData) routineData))
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
        
        private IEnumerator BendProgress(BendData bendData)
        {
            EDirection targetCubeFaceDirection = bendData.TargetCubeFaceDirectionProvider.GetDirection();

            bendData.ModifierProvider.TryGetModifier(targetCubeFaceDirection, out MegaModifier modifier);

            MegaBend bendModifier = (MegaBend)modifier;

            EDirection bendDirection = bendData.BendDirectionProvider.GetDirection();

            Vector3 bendVector = DirectionUtility.DirectionMap[bendDirection];

            Vector3 cubeComparisonVector = bendModifier.axis == MegaAxis.X || bendModifier.axis == MegaAxis.Z
                ? Cube.Instance.transform.forward
                : Cube.Instance.transform.up;
            
            float dot = Vector3.Dot(cubeComparisonVector, bendVector);
            
            if (dot > 0.9f)
            { 
                bendModifier.dir = 90;
            }
            else if (dot < -0.9f)
            {
                bendModifier.dir = -90;
            }
            else
            { 
                if (bendModifier.axis == MegaAxis.X)
                {
                    float signedAngle = Vector3.SignedAngle(cubeComparisonVector, bendVector, Cube.Instance.transform.up);
                
                    bendModifier.dir = signedAngle > 0 ? 0 : 180;
                }
                else if(bendModifier.axis == MegaAxis.Y)
                {
                    float signedAngle = Vector3.SignedAngle(cubeComparisonVector, bendVector, Cube.Instance.transform.right);
                
                    bendModifier.dir = signedAngle > 0 ? 180 : 0;
                }
                else if(bendModifier.axis == MegaAxis.Z)
                {
                    float signedAngle = Vector3.SignedAngle(cubeComparisonVector, bendVector, Cube.Instance.transform.up);
                
                    bendModifier.dir = signedAngle > 0 ? 180 : 0;
                }

            }
            
            float elapsedTime = 0f;

            while (elapsedTime < bendData.Duration)
            {
                float value =  bendData.Curve.Evaluate(elapsedTime/bendData.Duration) * bendData.CurveMultiplier;
                
                bendModifier.angle = value;
                
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }
            
            float finalValue = bendData.Curve.Evaluate(1) * bendData.CurveMultiplier;

            bendModifier.angle = finalValue;
        }
    }
}