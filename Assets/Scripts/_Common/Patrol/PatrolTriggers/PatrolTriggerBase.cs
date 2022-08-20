using System;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class PatrolTriggerBase : MonoBehaviour
    {
        public abstract Action OnPatrolTriggered { get; set; }
    }
}