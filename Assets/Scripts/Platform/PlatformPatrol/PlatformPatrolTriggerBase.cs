using System;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public abstract class PlatformPatrolTriggerBase : MonoBehaviour
    {
        public abstract Action OnPatrolTriggered { get; set; }
    }
}