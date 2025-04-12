using UnityEngine;
using UnityEngine.UI;

public class DiceModel : MonoBehaviour
{
    [SerializeField] private Image _number;
    public void Initialize(Sprite number)
    {
        _number.sprite = number;
        //_number.SetNativeSize();
    }
}
