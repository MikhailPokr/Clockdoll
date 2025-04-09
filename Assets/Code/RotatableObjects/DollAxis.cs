using UnityEngine;

internal class DollAxis : BaseRotatableAxis
{
    //исключительно для теста, потом удалить
    Color[] rainbowPalette = new[]
    {
        Color.red,                       // 0 Красный
        new Color(1f, 0.4f, 0f),         // 1 Красно-оранжевый
        new Color(1f, 0.6f, 0f),         // 2 Оранжевый
        Color.yellow,                    // 3 Желтый
        new Color(0.8f, 1f, 0f),         // 4 Желто-зеленый (лаймовый)
        Color.green,                     // 5 Зеленый
        new Color(0f, 0.9f, 0.5f),       // 6 Зелено-голубой
        Color.cyan,                      // 7 Голубой
        new Color(0f, 0.6f, 1f),         // 8 Небесно-синий
        Color.blue,                      // 9 Синий
        new Color(0.3f, 0f, 0.7f),       // 10 Индиго
        new Color(0.7f, 0f, 1f)          // 11 Фиолетовый
    };

    protected override GameObject Generate(int place)
    {
        DollModel doll = Instantiate(_palette.DollPrefab, transform);
        doll.ChangeNumber(_seccionData.GetDollIndex(place), rainbowPalette[place - 1]);
        doll.name = $"Doll {doll.Number} in place: {place}";
        return doll.gameObject;
    }
}
