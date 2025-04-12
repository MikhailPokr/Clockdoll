using UnityEngine;

internal abstract class BaseCard
{
    protected string _condition;
    protected string _effect;
    protected Sprite _suit;
    protected Color _color;

    public abstract void Play();
    public abstract bool Check();

    public virtual (string condition, string effect, Sprite suit, Color color) GetData()
        => (_condition, _effect, _suit, _color);
}