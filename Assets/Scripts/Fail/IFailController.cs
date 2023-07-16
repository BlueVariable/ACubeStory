using System;

namespace BLUE.ACubeStory.Fail
{
    public interface IFailController
    {
        public Action<IFailController> OnFailStarted { get; set; }
        public Action<IFailController> OnFailed { get; set; }
    }
}