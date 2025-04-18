using UnityEngine;

internal class Initializer : IService
{
    public void InitializeObjects()
    {
        var monobehObjects = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < monobehObjects.Length; i++)
        {
            if (monobehObjects[i] is IInitializable initializable)
            {
                initializable.Initialize();
            }
        }
    }
}
