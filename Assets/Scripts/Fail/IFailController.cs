using System;

namespace BLUE.ACubeStory.Fail
{
    public interface IFailController
    {
        public Action<IFailController> OnFailed { get; set; }
    }
}