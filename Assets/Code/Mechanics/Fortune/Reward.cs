using UnityEngine;

[System.Serializable]
internal class Reward
{
    //потом заменить на ключ
    public string Description;
    public string AlertDescription;
    public RewardType Type;
    public int Count;
    [Header("Рандом записывать через ~")]
    [SerializeField] private string _value;
    private bool _newValue = true;
    private int _fixedValue = 0;

    public int Value
    {
        get
        {
            if (!_newValue)
                return _fixedValue;
            string[] raw = _value.Split("~");
            if (raw.Length == 2 &&
                int.TryParse(raw[0], out int min) &&
                int.TryParse(raw[1], out int max))
            {
                _newValue = false;
                _fixedValue = Random.Range(min, max + 1);
                return _fixedValue;
            }
            else if (raw.Length == 1 && int.TryParse(raw[0], out int num))
            {
                _newValue = false;
                _fixedValue = num;
                return num;
            }
            else
            {
                return 0;
            }
        }
    }

    public Reward Clone()
    {
        return new Reward
        {
            Description = Description,
            AlertDescription = AlertDescription,
            Type = Type,
            Count = Count,
            _value = _value,
            _newValue = true
        };
    }

    public void Lock()
    {
        _ = Value; //получаем значение, тем самым его однозначно определяя
    }
}