using DG.Tweening;
using UnityEngine;

internal abstract class BaseRotatableAxis : BaseRotatableObject
{
    [Header("0: outside left - 4: outside right")]
    [SerializeField] protected GameObject[] _places;

    protected GameObject[] _objects = new GameObject[5];
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
        Rotate(clockwise);
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
            SetNewObject(tablePlace, i + 2);
        }
    }

    private void Rotate(bool clockwise)
    {
        int direction = clockwise ? -1 : 1;
        int excessIndex = clockwise ? 0 : _objects.Length - 1;

        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == excessIndex) continue;

            int targetIndex = i + direction;
            Transform target = _places[targetIndex].transform;

            sequence.Join(_objects[i].transform.DOMove(target.position, _duration).SetEase(Ease.Linear));
            sequence.Join(_objects[i].transform.DORotateQuaternion(target.rotation, _duration).SetEase(Ease.Linear));
            sequence.Join(_objects[i].transform.DOScale(target.localScale, _duration).SetEase(Ease.Linear));
        }

        sequence.OnComplete(() =>
        {
            CompleteRotationStep(clockwise);
            _isRotating = false;
        });
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