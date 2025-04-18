using UnityEngine;

internal abstract class BaseRotatableObject : MonoBehaviour
{
    public abstract bool IsRotating { get; }

    public abstract void Initiate(Palette palette, IDollPlacementController placementController, float duration);

    public abstract void StartRotation(bool clockwise);
}