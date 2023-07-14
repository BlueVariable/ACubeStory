namespace BLUE.ACubeStory.Fail
{
    public interface IFailObserver
    {
        public void FailObserved(IFailController failController);
    }
}