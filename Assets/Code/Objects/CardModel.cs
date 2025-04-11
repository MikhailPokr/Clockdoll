using UnityEngine;
using UnityEngine.UI;
using TMPro;

internal class CardModel : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;

    private CardManager _cardManager;
    private BaseCard _card;

    public void Initialize(BaseCard card, CardManager cardManager)
    {
        _card = card;
        var data = card.GetData();
        _title.text = data.name;
        _description.text = data.description;
        _image.color = data.color;
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
        _cardManager.TryPlayCard(_card);
    }
}
