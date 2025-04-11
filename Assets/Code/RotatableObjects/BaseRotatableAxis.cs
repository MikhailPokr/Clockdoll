using System.Collections;
using UnityEngine;

internal abstract class BaseRotatableAxis : MonoBehaviour
{
    [Header("0: outside left - 4: outside right")]
    [SerializeField] protected GameObject[] _places;


    protected GameObject[] _objects = new GameObject[5];
    protected Coroutine _rotationCoroutine;
    protected bool _isRotating = false;
    protected bool _clockwise;
    protected int _currentIndex;
    private float _duration;

    protected int _queuedDirection = 0;
    protected bool _isCompletingMove = false;

    protected PrefabPalette _palette;
    protected TableData _tableData;

    public void Initiate(PrefabPalette palette, TableData seccion, float duration)
    {
        _palette = palette;
        _tableData = seccion;
        _duration = duration;
        _currentIndex = _tableData.CurrentPlace;

        _tableData.TableStartRotated += QueueRotation;

        InitializeObjects();
    }

    public void QueueRotation(int direction)
    {
        direction = Mathf.Clamp(direction, -1, 1);

        if (!_isRotating && direction != 0)
        {
            StartRotation(direction > 0);
            return;
        }

        _queuedDirection = direction;

        if (direction ==0)
        {
            _isCompletingMove = true;
        }
    }

    private void InitializeObjects()
    {
        for (int i = -2; i <= 2; i++)
        {
            int place = ConvertPlaceForCircle(i + _currentIndex, 12, false);
            SetNewObject(place);
        }
    }

    private int ConvertPlaceForCircle(int place, int max, bool fromZero)
    {
        if (fromZero)
        {
            place %= max;
            return place < 0 ? place + max : place;
        }
        else
        {
            place = (place - 1) % max + 1;
            return place <= 0 ? place + max : place;
        }
    }

    private void SetNewObject(int number)
    {
        GameObject obj = Generate(number);
        int place = ConvertPlaceForCircle(number - _currentIndex + 2, 12, true);
        SetObjectTransform(obj, place);
        _objects[place] = obj;
    }

    private void SetObjectTransform(GameObject obj, int place)
    {
        Transform placeTransform = _places[place].transform;
        obj.transform.SetPositionAndRotation(placeTransform.position, placeTransform.rotation);
        obj.transform.localScale = placeTransform.localScale;
    }

    protected abstract GameObject Generate(int place);

    protected void StartRotation(bool clockwise)
    {
        if (_isRotating) return;

        _clockwise = clockwise;
        _isRotating = true;
        _isCompletingMove = false;
        _rotationCoroutine = StartCoroutine(RotationCoroutine());
    }

    private IEnumerator RotationCoroutine()
    {
        while (true)
        {
            if (!PrepareRotationStep(out var startTransforms, out var targetTransforms))
                yield break;

            yield return ExecuteRotationAnimation(startTransforms, targetTransforms);

            if (!FinalizeRotationStep())
                yield break;
        }
    }

    private bool PrepareRotationStep(
        out (Vector3 position, Quaternion rotation, Vector3 scale)[] startTransforms,
        out (Vector3 position, Quaternion rotation, Vector3 scale)[] targetTransforms)
    {
        int direction = _clockwise ? -1 : 1;
        int excessObjectIndex = _clockwise ? 0 : _objects.Length - 1;

        startTransforms = new (Vector3, Quaternion, Vector3)[_objects.Length];
        targetTransforms = new (Vector3, Quaternion, Vector3)[_objects.Length];

        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == excessObjectIndex) continue;

            startTransforms[i] = (
                _objects[i].transform.position,
                _objects[i].transform.rotation,
                _objects[i].transform.localScale
            );

            int targetIndex = i + direction;
            Transform targetTransform = _places[targetIndex].transform;
            targetTransforms[i] = (
                targetTransform.position,
                targetTransform.rotation,
                targetTransform.localScale
            );
        }

        return true;
    }

    private IEnumerator ExecuteRotationAnimation(
        (Vector3 position, Quaternion rotation, Vector3 scale)[] startTransforms,
        (Vector3 position, Quaternion rotation, Vector3 scale)[] targetTransforms)
    {
        float elapsed = 0f;
        int excessObjectIndex = _clockwise ? 0 : _objects.Length - 1;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / _duration);

            AnimateObjects(startTransforms, targetTransforms, excessObjectIndex, t);
            yield return null;
        }

        FinalizeObjectTransforms(targetTransforms, excessObjectIndex);
    }

    private void AnimateObjects(
        (Vector3 position, Quaternion rotation, Vector3 scale)[] startTransforms,
        (Vector3 position, Quaternion rotation, Vector3 scale)[] targetTransforms,
        int excessObjectIndex, float t)
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == excessObjectIndex) continue;

            _objects[i].transform.position = Vector3.Lerp(
                startTransforms[i].position, targetTransforms[i].position, t);
            _objects[i].transform.rotation = Quaternion.Lerp(
                startTransforms[i].rotation, targetTransforms[i].rotation, t);
            _objects[i].transform.localScale = Vector3.Lerp(
                startTransforms[i].scale, targetTransforms[i].scale, t);
        }
    }

    private void FinalizeObjectTransforms(
        (Vector3 position, Quaternion rotation, Vector3 scale)[] targetTransforms,
        int excessObjectIndex)
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == excessObjectIndex) continue;
            _objects[i].transform.position = targetTransforms[i].position;
            _objects[i].transform.rotation = targetTransforms[i].rotation;
            _objects[i].transform.localScale = targetTransforms[i].scale;
        }
    }

    private bool FinalizeRotationStep()
    {
        CompleteRotationStep();

        if (_queuedDirection != 0)
        {
            bool newDirection = _queuedDirection > 0;
            _queuedDirection = 0;

            if (newDirection == _clockwise)
            {
                return true;
            }
            else
            {
                _isRotating = false;
                StartRotation(newDirection);
                return false;
            }
        }

        if (_isCompletingMove)
        {
            _isRotating = false;
            _isCompletingMove = false;
            return false;
        }

        return true;
    }

    private void CompleteRotationStep()
    {
        RemoveExcessObject();
        ReorganizeObjects();
        UpdateCurrentIndex();
        CreateNewObject();
    }

    private void RemoveExcessObject()
    {
        int excessObjectIndex = _clockwise ? 0 : _objects.Length - 1;
        Destroy(_objects[excessObjectIndex]);
        _objects[excessObjectIndex] = null;
    }

    private void ReorganizeObjects()
    {
        int direction = _clockwise ? -1 : 1;
        int newObjectIndex = _clockwise ? _objects.Length - 1 : 0;
        GameObject[] newObjects = new GameObject[_objects.Length];

        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == newObjectIndex) continue;
            newObjects[i] = _objects[i - direction];
        }

        _objects = newObjects;
    }

    private void UpdateCurrentIndex()
    {
        int direction = _clockwise ? -1 : 1;
        _currentIndex = ConvertPlaceForCircle(_currentIndex - direction, 12, false);
        _tableData.SetCurrentDoll(_currentIndex);
    }

    private void CreateNewObject()
    {
        int virtualPlaceIndex = _currentIndex + (_clockwise ? 2 : -2);
        SetNewObject(ConvertPlaceForCircle(virtualPlaceIndex, 12, false));
    }
}