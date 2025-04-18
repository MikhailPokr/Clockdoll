// BaseRotatableAxis.cs
using System.Collections;
using UnityEngine;

internal abstract class BaseRotatableAxis : BaseRotatableObject
{
    [Header("0: outside left - 4: outside right")]
    [SerializeField] protected GameObject[] _places;

    protected GameObject[] _objects = new GameObject[5];
    protected Coroutine _rotationCoroutine;
    protected bool _isRotating = false;
    public override bool IsRotating => _isRotating;
    protected ClockNum _currentPlace;
    private float _duration;

    protected Palette _palette;
    protected IDollPlacementController _placementController;

    public override void Initiate(Palette palette, IDollPlacementController placementController, float duration)
    {
        _palette = palette;
        _placementController = placementController;
        _currentPlace = _placementController.CurrentPlace;
        _duration = duration;
        InitializeObjects();
    }
    public override void StartRotation(bool clockwise)
    {
        if (_isRotating) return;
        _isRotating = true;
        _rotationCoroutine = StartCoroutine(RotationCoroutine(clockwise));
    }


    private void InitializeObjects()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            if (_objects[i] != null)
            {
                Destroy(_objects[i].gameObject);
            }
        }

        for (int i = -2; i <= 2; i++)
        {
            ClockNum tablePlace = _currentPlace + i;
            SetNewObject(tablePlace, i + 2); // +2 для преобразования диапазона -2..2 в 0..4
        }
    }

    private IEnumerator RotationCoroutine(bool clockwise)
    {
        var startTransforms = new (Vector3 pos, Quaternion rot, Vector3 scale)[_objects.Length];
        var targetTransforms = new (Vector3 pos, Quaternion rot, Vector3 scale)[_objects.Length];
        int direction = clockwise ? -1 : 1;
        int excessIndex = clockwise ? 0 : _objects.Length - 1;

        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == excessIndex) continue;

            startTransforms[i] = (
                _objects[i].transform.position,
                _objects[i].transform.rotation,
                _objects[i].transform.localScale
            );

            int targetIndex = i + direction;
            Transform target = _places[targetIndex].transform;
            targetTransforms[i] = (target.position, target.rotation, target.localScale);
        }

        float elapsed = 0f;
        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _duration);

            for (int i = 0; i < _objects.Length; i++)
            {
                if (i == excessIndex) continue;

                _objects[i].transform.position = Vector3.Lerp(
                    startTransforms[i].pos, targetTransforms[i].pos, t);
                _objects[i].transform.rotation = Quaternion.Lerp(
                    startTransforms[i].rot, targetTransforms[i].rot, t);
                _objects[i].transform.localScale = Vector3.Lerp(
                    startTransforms[i].scale, targetTransforms[i].scale, t);
            }
            yield return null;
        }

        CompleteRotationStep(clockwise);
        _isRotating = false;
    }

    private void CompleteRotationStep(bool clockwise)
    {
        int excessIndex = clockwise ? 0 : _objects.Length - 1;
        Destroy(_objects[excessIndex]);

        int direction = clockwise ? -1 : 1;
        var newObjects = new GameObject[_objects.Length];

        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == (direction > 0 ? 0 : _objects.Length - 1)) continue;
            newObjects[i] = _objects[i - direction];
        }
        _objects = newObjects;

        _currentPlace -= direction;
        ClockNum newPlace = _currentPlace + (clockwise ? 2 : -2);
        SetNewObject(newPlace, clockwise ? 4 : 0);
    }

    private void SetNewObject(ClockNum tablePlace, int axisPlace)
    {
        GameObject obj = Generate(tablePlace);
        SetObjectTransform(obj, axisPlace);
        _objects[axisPlace] = obj;
    }

    private void SetObjectTransform(GameObject obj, int placeIndex)
    {
        Transform placeTransform = _places[placeIndex].transform;
        obj.transform.SetPositionAndRotation(placeTransform.position, placeTransform.rotation);
        obj.transform.localScale = placeTransform.localScale;
    }

    protected abstract GameObject Generate(ClockNum place);
}