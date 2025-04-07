using TMPro;
using UnityEngine;

public class Doll : MonoBehaviour
{
    public int Number;
    // для прототипа, нужно исправить на отдельные объекты
    public void ChangeNumber(int number, Color color)
    {
        Number = number;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = color;
        }
        GetComponentInChildren<TextMeshPro>().text = number.ToString();
    }
}
