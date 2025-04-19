using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MovableSprite : BaseRotatableObject
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _leftCopy;
    private SpriteRenderer _rightCopy;

    private Dictionary<ClockNum, float> _partCenters;

    private bool _isRotating;
    public override bool IsRotating => _isRotating;

    private IDollPlacementController _placementController;
    private float _duration;


    public override void Initiate(Palette palette, IDollPlacementController placementController, float duration)
    {
        _placementController = placementController;
        _duration = duration;

        CreateSideCopies();
        CalculatePartCenters();

        transform.position = new(_partCenters[_placementController.GetCurrentDollIndex()], transform.position.y);
    }

    private void CreateSideCopies()
    {
        _leftCopy = Instantiate(_spriteRenderer, _spriteRenderer.transform);
        _leftCopy.transform.localPosition = Vector3.zero;
        _leftCopy.transform.localScale = Vector3.one;

        _rightCopy = Instantiate(_spriteRenderer, _spriteRenderer.transform);
        _rightCopy.transform.localPosition = Vector3.zero;
        _rightCopy.transform.localScale = Vector3.one;

        UpdateCopiesPosition();
    }

    private void UpdateCopiesPosition()
    {
        Bounds bounds = _spriteRenderer.bounds;
        float spriteWidth = bounds.size.x;

        _leftCopy.transform.position = _spriteRenderer.transform.position - new Vector3(spriteWidth, 0, 0);
        _rightCopy.transform.position = _spriteRenderer.transform.position + new Vector3(spriteWidth, 0, 0);
    }

    private void CalculatePartCenters()
    {
        Bounds bounds = _spriteRenderer.bounds;
        float spriteWidth = bounds.size.x;
        float partWidth = spriteWidth / ClockNum.MaxValue;

        _partCenters = new();

        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            float centerX = bounds.min.x + (i * partWidth) + (partWidth / 2);

            _partCenters[i] = centerX;
        }

        for (int i = ClockNum.MinValue; i < ClockNum.MaxValue; i++)
        {
            GameObject a = new GameObject(i.ToString());
            a.transform.position = new(_partCenters[i], transform.position.y);
            a.transform.parent = _spriteRenderer.transform;
        }
    }

    public override void StartRotation(bool clockwise)
    {
        if (_isRotating) return;

        _isRotating = true;
        StartCoroutine(RotateCoroutine(clockwise));
    }

    private IEnumerator RotateCoroutine(bool clockwise)
    {
        float elapsedTime = 0f;
        Vector2 startPos = transform.position;

        ClockNum currentIndex = _placementController.GetCurrentDollIndex();
        ClockNum nextIndex = clockwise ? currentIndex - 1 : currentIndex + 1;
        float targetX = _partCenters[nextIndex];

        Vector2 endPos = new Vector2(targetX, startPos.y);

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _duration);
            transform.position = Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        _isRotating = false;
    }
}