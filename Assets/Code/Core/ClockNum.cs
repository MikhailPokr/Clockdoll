using System;
using UnityEngine;

[Serializable]
internal struct ClockNum : IEquatable<ClockNum>, IComparable<ClockNum>
{
    [SerializeField] private int _value { get; set; }

    public int Value
    {
        get => _value;
        set => _value = IntToCorrectNum(value);
    }

    public static int MinValue = 1;
    public static int MaxValue = 12;
    public override string ToString() => _value.ToString();
    /// <summary>
    /// Преобразование для массивов, поскольку в них минимальное значение 0.
    /// </summary>
    public int ToIndex() => _value - 1;

    public static int IntToCorrectNum(int integer)
    {
        integer %= 12;
        return integer <= 0 ? 12 + integer : integer;
    }

    public ClockNum(int value) => this._value = IntToCorrectNum(value);

    public static implicit operator ClockNum(int value) => new ClockNum(value);
    public static implicit operator int(ClockNum clockNum) => clockNum._value;

    public override int GetHashCode() => _value.GetHashCode();

    public override bool Equals(object obj)
    {
        if ((obj is not ClockNum) && (obj is not int))
            return false;

        if (obj is ClockNum)
        {
            ClockNum other = (ClockNum)obj;
            return other._value == _value;
        }
        else
        {
            int other = (int)obj;
            return IntToCorrectNum(other) == _value;
        }
    }
    public bool Equals(ClockNum other) => this.Value == other.Value;
    public static bool operator ==(ClockNum a, ClockNum b) => a.Equals(b);
    public static bool operator !=(ClockNum a, ClockNum b) => !(a == b);

    public readonly int CompareTo(ClockNum other) => _value.CompareTo(other._value);



    public static bool operator >(ClockNum left, ClockNum right) => left._value > right._value;
    public static bool operator <(ClockNum left, ClockNum right) => left._value < right._value;
    public static bool operator >=(ClockNum left, ClockNum right) => left._value >= right._value;
    public static bool operator <=(ClockNum left, ClockNum right) => left._value <= right._value;

    public static ClockNum operator +(ClockNum left, ClockNum right) => new ClockNum(IntToCorrectNum(left._value + right._value));
    public static ClockNum operator -(ClockNum left, ClockNum right) => new ClockNum(IntToCorrectNum(left._value - right._value));
    public static ClockNum operator *(ClockNum left, ClockNum right) => new ClockNum(IntToCorrectNum(left._value * right._value));
    public static ClockNum operator /(ClockNum left, ClockNum right)
    {
        if (right == 0)
            return MaxValue;
        return new ClockNum(IntToCorrectNum(left._value / right._value));
    }
    public static ClockNum operator ++(ClockNum num) => new(num + 1);
    public static ClockNum operator --(ClockNum num) => new(num - 1);
}