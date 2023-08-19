using BLUE.ACubeStory.Sound;
using BLUE.Extensions;
using UnityEngine;

namespace BLUE.ACubeStory.Common

{
    public class RollSFXController : MonoBehaviour
    {
        [SerializeField] private RollBehaviourBase _rollBehaviour = null;

        [SerializeField] private ESound[] _soundTypes = default;
        
        private void Awake()
        {
            _rollBehaviour.OnRollStarted += OnRollStarted;
        }

        private void OnDestroy()
        {
            _rollBehaviour.OnRollStarted -= OnRollStarted;
        }

        private void OnRollStarted()
        {
            ESound randSound = _soundTypes.GetRandomElement();

            SoundManager.Instance.PlaySound(randSound);
        }
    }
}