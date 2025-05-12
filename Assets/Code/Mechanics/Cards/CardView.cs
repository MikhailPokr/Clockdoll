using UnityEngine;
using UnityEngine.UI;
using TMPro;

internal class CardView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _suit;
    [SerializeField] private TextMeshProUGUI _condition;
    [SerializeField] private TextMeshProUGUI _effect;

    private HandView _cardManager;
    private BaseCard _card;

    public void Initialize(BaseCard card, HandView cardManager)
    {
        _card = card;
        var data = card.GetData();
        _condition.text = data.condition;
        _effect.text = data.effect;
        _suit.sprite = data.suit;
        _suit.color = data.color;
        _cardManager = cardManager;
    }

    public void Hower()
    {
        _cardManager.ShowCard(true, _card);
    }

    public void UnHover()
    {
        _cardManager.ShowCard(false);
    }

    public void Click()
    {
        _cardManager.ClickCard(_card);
    }
}
