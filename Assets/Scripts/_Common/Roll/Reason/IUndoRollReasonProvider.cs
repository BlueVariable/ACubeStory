namespace BLUE.ACubeStory.Common
{
    public interface IUndoRollReasonProvider
    {
       public EUndoRollReason GetUndoRollReason { get; }
    }
}