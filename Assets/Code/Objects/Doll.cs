using TMPro;
using UnityEngine;

public class Doll : MonoBehaviour
{
    // для прототипа, нужно исправить на отдельные объекты
    public void ChangeNumber(int number, Color color)
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = color;
        }
        GetComponentInChildren<TextMeshPro>().text = number.ToString();
    }
}
