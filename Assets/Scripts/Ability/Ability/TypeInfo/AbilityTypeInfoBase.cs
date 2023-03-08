using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public abstract class AbilityTypeInfoBase<T> : MonoBehaviour
        where T : Enum
    {
        public abstract Dictionary<T, Type> GetAbilityTypeInfoMapping();
    }
}