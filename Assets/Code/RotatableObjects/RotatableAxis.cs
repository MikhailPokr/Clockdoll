using System.Collections;
using UnityEngine;

internal abstract class RotatableAxis : MonoBehaviour
{
    [Header("0: outside left - 4: outside right")]
    [SerializeField] protected GameObject[] _places;

    protected GameObject[] _objects = new GameObject[5];
    protected Coroutine _rotationCoroutine;
    protected bool _isRotating = false;
    protected bool _isCompletingMove = false;
    protected bool _clockwise;
    protected int _currentIndex;

    protected PrefabPalette _palette;
    protected SeccionData _seccionData;

    public void Initiate(PrefabPalette palette, SeccionData seccion, int firstPosition)
    {
        _palette = palette;
        _seccionData = seccion;
        _currentIndex = firstPosition;

        for (int i = - 2; i <=  2; i++)
        {
            SetNewObject(ConvertPlaceForCircle(i + firstPosition, 12, false));
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

    public void SetNewObject(int number)
    {
        GameObject obj = Generate(number);
        int place = ConvertPlaceForCircle(number - _currentIndex + 2, 12, true);
        Transform placeTransform = _places[place].transform;
        obj.transform.SetPositionAndRotation(placeTransform.position, placeTransform.rotation);
        obj.transform.localScale = placeTransform.localScale;
        _objects[place] = obj;
        //print($"кукла на позиции {number} записана в место {place}. Центр: {_currentIndex}");
    }

    protected abstract GameObject Generate(int place);

    public void StartRotation(bool clockwise)
    {
        if (_rotationCoroutine != null && _isRotating) return;

        _clockwise = clockwise;
        _isRotating = true;
        _isCompletingMove = false;

        if (_rotationCoroutine == null)
        {
            _rotationCoroutine = StartCoroutine(RotationCoroutine(clockwise));
        }
    }

    public void StopRotation()
    {
        if (!_isRotating) return;

        _isRotating = false;
    }

    private IEnumerator RotationCoroutine(bool clockwise)
    {
        while (true)
        {
            int direction = clockwise ? 1 : -1;

            Vector3[] startPositions = new Vector3[_objects.Length];
            Quaternion[] startRotations = new Quaternion[_objects.Length];
            Vector3[] startScales = new Vector3[_objects.Length];

            Vector3[] targetPositions = new Vector3[_objects.Length];
            Quaternion[] targetRotations = new Quaternion[_objects.Length];
            Vector3[] targetScales = new Vector3[_objects.Length];

            int excessObjectIndex = clockwise ? _objects.Length - 1 : 0;

            for (int i = 0; i < _objects.Length; i++)
            {
                if (i == excessObjectIndex)
                    continue;

                startPositions[i] = _objects[i].transform.position;
                startRotations[i] = _objects[i].transform.rotation;
                startScales[i] = _objects[i].transform.localScale;

                int targetIndex = i + direction;

                targetPositions[i] = _places[targetIndex].transform.position;
                targetRotations[i] = _places[targetIndex].transform.rotation;
                targetScales[i] = _places[targetIndex].transform.localScale;
            }

            float duration = 0.3f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (!_isRotating) _isCompletingMove = true;
                if (_isRotating && _isCompletingMove) _isCompletingMove = false;

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                for (int i = 0; i < _objects.Length; i++)
                {
                    if (i == excessObjectIndex)
                        continue;

                    _objects[i].transform.position = Vector3.Lerp(
                        startPositions[i],
                        targetPositions[i],
                        t);

                    _objects[i].transform.rotation = Quaternion.Lerp(
                        startRotations[i],
                        targetRotations[i],
                        t);

                    _objects[i].transform.localScale = Vector3.Lerp(
                        startScales[i],
                        targetScales[i],
                        t);
                }

                yield return null;
            }

            for (int i = 0; i < _objects.Length; i++)
            {
                if (i == excessObjectIndex)
                    continue;
                _objects[i].transform.position = targetPositions[i];
                _objects[i].transform.rotation = targetRotations[i];
                _objects[i].transform.localScale = targetScales[i];
            }

            if (!_isRotating && _isCompletingMove)
            {
                _isCompletingMove = false;
                CompleteRotationStep(clockwise);
                _rotationCoroutine = null;
                yield break;
            }

            CompleteRotationStep(clockwise);

            if (!_isRotating)
            {
                _rotationCoroutine = null;
                yield break;
            }
        }
    }

    private void CompleteRotationStep(bool clockwise)
    {
        int direction = clockwise ? 1 : -1;

        int excessObjectIndex = clockwise ? _objects.Length - 1 : 0;
        Destroy(_objects[excessObjectIndex]);
        _objects[excessObjectIndex] = null;

        int newObjectIndex = clockwise ? 0 : _objects.Length - 1;

        GameObject[] newObjects = new GameObject[_objects.Length];

        for (int i = 0; i < _objects.Length; i++)
        {
            if (i == newObjectIndex)
                continue;
            int sourceIndex = i - direction;

            newObjects[i] = _objects[sourceIndex];
        }
        _objects = newObjects;

        _currentIndex = ConvertPlaceForCircle(_currentIndex - direction, 12, false);

        int virtualPlaceIndex = _currentIndex + (clockwise ? -2 : 2);
        SetNewObject(ConvertPlaceForCircle(virtualPlaceIndex, 12, false));
    }
}