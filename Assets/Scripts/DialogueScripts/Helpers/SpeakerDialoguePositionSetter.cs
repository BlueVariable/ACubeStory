using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue
{
    public class SpeakerDialoguePositionSetter : MonoBehaviour
    {
        [SerializeField] private Vector3ProviderBase _dialoguePositionProvider = null;

        [SerializeField] private Vector3 _worldRotation = Vector3.zero;

        private void LateUpdate()
        {
            transform.position = _dialoguePositionProvider.GetVector3();
            
            transform.rotation = Quaternion.Euler(_worldRotation);
        }
    }
}