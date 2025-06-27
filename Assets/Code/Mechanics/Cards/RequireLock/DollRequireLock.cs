using System.Collections.Generic;

internal class DollRequireLock : IRequireLock
{
    public bool ItsForPedro { get; }
    public DollRequireLockType LockType { get; }
    private int _count;
    private List<int> _selectedObjects;

    public bool ItsDone => _selectedObjects.Count >= _count;

    public DollRequireLock(bool forPredro, int count, DollRequireLockType lockType)
    {
        ItsForPedro = forPredro;
        _count = count;
        LockType = lockType;

        _selectedObjects = new List<int>();
    }

    public void Add(int dollIndex)
    {
         _selectedObjects.Add(dollIndex);
    }
}
