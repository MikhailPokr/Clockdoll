using UnityEngine;

internal class AnokCard : BaseCard
{
    public AnokCard()
    {
        // Генерация взвешенного случайного числа (квадратичное распределение)
        float randomValue = Random.value;
        float weightedValue = Mathf.Pow(randomValue, 2);
        int result = Mathf.Clamp((int)(weightedValue * 99) + 1, 1, 100);

        // Расчет реальной вероятности выпадения этого числа
        float lowerBound = Mathf.Pow((float)(result - 1) / 99f, 2f);
        float upperBound = Mathf.Pow((float)result / 99f, 2f);
        float probability = upperBound - lowerBound;

        // Расчет шанса 1 к X с защитой от деления на ноль
        int oneInXChance = probability > 0 ? (int)Mathf.Round(1f / probability) : int.MaxValue;

        _title = $"Карта Анока {result}";

        // Форматирование описания с учетом крайних случаев
        if (oneInXChance >= 1000000)
        {
            _description = "Исключительно редкая карта (шанс < 0.0001%)";
        }
        else if (oneInXChance >= 1000)
        {
            float chancePercent = 100f / oneInXChance;
            _description = $"Невероятно редкая карта (шанс ~{chancePercent:0.###}%)";
        }
        else
        {
            float chancePercent = 100f / oneInXChance;
            _description = $"Шанс выпадения: ~{chancePercent:0.##}% (1 к {oneInXChance})";
        }

        // Градиент от белого к золотому
        _color = Color.Lerp(new Color(1, 1, 1), new Color(1, 0.84f, 0), (float)result / 100f);
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
