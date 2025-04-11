using UnityEngine;

internal class PedroCard : BaseCard
{
    public PedroCard()
    {
        float randomValue = Random.value;
        float weightedValue = Mathf.Pow(randomValue, 2);
        int result = (int)(weightedValue * 99) + 1;

        float lowerBound = Mathf.Pow((result - 1) / 99, 2);
        float upperBound = Mathf.Pow(result / 99, 2);

        float probability = upperBound - lowerBound;
        int oneInXChance = (int)Mathf.Round(1 / probability);

        _title = $"Карта Педро {result}";
        _description = $"Карточка, шанс на выпадение которой 1 к {oneInXChance}";
        _color = Color.Lerp(new Color(1, 1, 1), new Color(1, 0.84f, 0), result / 100);
    }

    public override bool Check()
    {
        return true;
    }

    public override void Play()
    {
        Debug.Log($"Сыграна карта {_title}");
    }
}
