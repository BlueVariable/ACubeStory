using System;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public interface IPlatformEntrance
    {
        public Action<Transform> OnObjectEntered { get; set; }
        public Action<Transform> OnObjectExited { get; set; }
    }
}