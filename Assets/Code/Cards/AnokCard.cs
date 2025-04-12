using UnityEngine;

internal class AnokCard : BaseCard
{
    public AnokCard(Palette palette)
    {
        int index = Random.Range(1, 5);
        
        _suit = palette.SuitsSprites.GetSuit(index);
        _condition = $"Карточка {System.Guid.NewGuid().ToString().Replace('-', ' ')}";
        _effect = $"Делает ужасные вещи с Педро.";
        _color = index%2 == 1 ? Color.red : Color.gray;
    }

    public override bool Check()
    {
        return true;
    }

    public override void Play()
    {
        Debug.Log($"Сыграна карта {_condition}");
    }
}
