using System.Collections.Generic;
using System.Linq;

internal class MultipleRequireLock : IRequireLock
{
    public bool ItsDone => ChildLocks.All(x => x.ItsDone);

    public List<IRequireLock> ChildLocks { get; }

    public MultipleRequireLock(params IRequireLock[] childLocks)
    {
        ChildLocks = childLocks.ToList();
    }
}
