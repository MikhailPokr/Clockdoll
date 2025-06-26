using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class HandView : MonoBehaviour, IInitializable
{
    [SerializeField] CardView _showedCard;
    [SerializeField] RectTransform _rectTransform;

    [Header("Animation Settings")]
    [SerializeField] private float _animationDuration;
    [SerializeField] private Vector2 _startPosition = new Vector2(-853f, -612f);
    [SerializeField] private Vector2 _midPositionX = new Vector2(-903f, -689f);
    [SerializeField] private Vector3 _maxRotation = new Vector3(0f, 0f, -85.934f);
    [SerializeField] private Ease _rotationOutEase = Ease.OutSine;
    [SerializeField] private Ease _rotationInEase = Ease.InSine;

    private IGame _game;
    private ICardSystem _cardSystem;
    private CardView _cardPrefab;

    private List<BaseCard> _cards;

    private Sequence _sequence;

    public void Initialize()
    {
        _game = ServiceLocator.Resolve<IGame>();
        _cardSystem = ServiceLocator.Resolve<ICardSystem>();

        _cardPrefab = ServiceLocator.Resolve<Palette>().CardPrefab;
        SignalBus.Subscribe<HandUpdatedSignal>(this, OnHandUpdated);

        OnHandUpdated();
    }

    private void OnHandUpdated()
    {
        List<BaseCard> cards = _cardSystem.GetCurrentHandForView();
        if (_cards == null || !_cards.SequenceEqual(cards))
        {
            _cards = cards;
            Animate();
        }
    }

    public void TakeCard(bool toPedro, int count) => _cardSystem.TakeCard(toPedro, count);

    public void ClickCard(BaseCard card)
    {
        _game.CardClick(false, card);
    }

    public void GenerateHand()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        List<CardView> cards = new();
        for (int i = 0; i < _cards.Count; i++)
        {
            CardView card = Instantiate(_cardPrefab, transform);
            card.Initialize(_cards[i], this);
            cards.Add(card);
        }

        if (cards.Count == 0)
            return;
        float degrees = -90 / _cards.Count;
        degrees = Mathf.Clamp(degrees, -30, 0);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.localEulerAngles = new Vector3(0, 0, i * degrees);
        }
    }

    public void ShowCard(bool show, BaseCard card = null)
    {
        _showedCard.gameObject.SetActive(show);
        if (!show)
            return;
        _showedCard.Initialize(card, this);

    }

    private void Animate()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _rectTransform.anchoredPosition = _startPosition;
        _rectTransform.localEulerAngles = Vector3.zero;

        _sequence.Append(
            _rectTransform.DOLocalRotate(_maxRotation, _animationDuration * 0.5f)
                .SetEase(_rotationOutEase)
        );
        _sequence.Join(
            _rectTransform.DOAnchorPosY(_midPositionX.y, _animationDuration * 0.5f)
                .SetEase(Ease.OutSine)
        );
        _sequence.Join(
            _rectTransform.DOAnchorPosX(_midPositionX.x, _animationDuration * 0.5f)
                .SetEase(Ease.OutSine)
        );

        _sequence.AppendCallback(GenerateHand);

        _sequence.Append(
            _rectTransform.DOLocalRotate(Vector3.zero, _animationDuration * 0.5f)
                .SetEase(_rotationInEase)
        );
        _sequence.Join(
            _rectTransform.DOAnchorPosY(_startPosition.y, _animationDuration * 0.5f)
                .SetEase(Ease.InSine)
        );
        _sequence.Join(
            _rectTransform.DOAnchorPosX(_startPosition.x, _animationDuration * 0.5f)
                .SetEase(Ease.InSine)
        );
    }
}
