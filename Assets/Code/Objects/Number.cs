using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    // для прототипа, нужно исправить на изменение спрайта
    public void ChangeNumber(int number)
    {
        GetComponentInChildren<TextMeshPro>().text = number.ToString();
    }
}
