using UnityEngine;

internal class PedroCard : BaseCard
{
    public PedroCard(Palette palette, bool spades)
    {
        int index = Random.Range(1, 5);

        _suit = palette.SuitsSprites.GetSuit(index);
        _condition = $"Карточка {(spades ? "Пик" : "")} {System.Guid.NewGuid().ToString().Replace('-', ' ')}";
        _effect = $"Делает ужасные вещи с Аноком";
        _color = index % 2 == 1 ? Color.red : Color.gray;
    }

    public override bool Check()
    {
        return false;
    }

    public override void Play()
    {
        Debug.Log($"Сыграна карта {_condition}");
    }
}
