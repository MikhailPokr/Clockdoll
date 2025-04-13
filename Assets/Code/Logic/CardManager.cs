using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class CardManager : MonoBehaviour, IInitializable
{
    [SerializeField] CardModel _showedCard;
    [SerializeField] Animator _animator;

    private HandData _handData;
    private CardModel _cardPrefab;
    private Game _game;

    public void Initialize()
    {
        _cardPrefab = ServiceLocator.Resolve<Palette>().CardPrefab;
        _handData = ServiceLocator.Resolve<HandData>();
        _game = ServiceLocator.Resolve<Game>();
        _handData.HandUpdated += Animate;
        Animate();
    }

    public void TakeCard(bool toPedro, int count) => _handData.TakeCard(toPedro, count);

    public void ClickCard(BaseCard card)
    {
        _game.ClickAnokCard(card);
    }

    public void GenerateHand()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        List<CardModel> cards = new();
        List<BaseCard> hand = _handData.GetCurrentHand();
        for (int i = 0; i < hand.Count; i++)
        {
            CardModel card = Instantiate(_cardPrefab, transform);
            card.Initialize(hand[i], this);
            cards.Add(card);
        }

        if (cards.Count == 0) 
            return;
        float degrees = -90 / hand.Count;
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
        //переписать на 2 анимации
        _animator.SetTrigger("Update");
    }
}
