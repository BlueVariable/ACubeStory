using System.Collections.Generic;
using BLUE.TriggerSystem;

namespace BLUE.ACubeStory.Merge
{
    public abstract class Trigger_CubeMergeableOverlapBase : TriggerBase
    {
        public abstract HashSet<Mergeable> GetLastBottleneckMergeables();
    }
}