using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

internal class MovableSprite : BaseRotatableObject
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _leftCopy;
    private SpriteRenderer _rightCopy;

    private Dictionary<int, float> _partCenters;

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

        _spriteRenderer.transform.position = new(_partCenters[_placementController.CurrentPlace], _spriteRenderer.transform.position.y);
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

        for (int i = ClockNum.MinValue - 1; i <= ClockNum.MaxValue + 1; i++)
        {
            float centerX = bounds.max.x - (i * partWidth) - (partWidth / 2);

            _partCenters[i] = centerX;
        }
    }

    public override void StartRotation(bool clockwise)
    {
        if (_isRotating) return;
        _isRotating = true;

        int currentIndex = _placementController.CurrentPlace;
        int nextIndex = clockwise ? currentIndex + 1 : currentIndex - 1;
        float targetX = _partCenters[nextIndex];

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_spriteRenderer.transform.DOMoveX(targetX, _duration).SetEase(Ease.Linear));

        sequence.OnComplete(() =>
        {
            if (nextIndex == ClockNum.MinValue - 1)
                _spriteRenderer.transform.position = new Vector2(_partCenters[ClockNum.MaxValue], _spriteRenderer.transform.position.y);
            else if (nextIndex == ClockNum.MaxValue + 1)
                _spriteRenderer.transform.position = new Vector2(_partCenters[ClockNum.MinValue], _spriteRenderer.transform.position.y);

            _isRotating = false;
        });
    }
}