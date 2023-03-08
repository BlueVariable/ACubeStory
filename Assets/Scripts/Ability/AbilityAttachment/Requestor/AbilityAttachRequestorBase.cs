using System;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public abstract class AbilityAttachRequestorBase<T> : MonoBehaviour
        where T : Enum
    {
        public abstract T AbilityType { get; }
    }
}