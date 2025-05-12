using UnityEngine;
using UnityEngine.UI;

internal class DollCardView : MonoBehaviour
{
    [SerializeField] private Image _Image;
    [SerializeField] private Image _symbol;

    public void Initialize(Sprite symbol)
    {
        _symbol.sprite = symbol;
    }
}
