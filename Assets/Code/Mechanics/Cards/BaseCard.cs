using UnityEngine;

internal abstract class BaseCard
{
    protected ITextHandler _textHandler;
    protected Palette _palette;

    protected string _condition;
    protected string _effect;
    protected Sprite _suit;
    protected Color _color;

    public BaseCard()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _textHandler = ServiceLocator.Resolve<ITextHandler>();
    }

    public abstract void PlayEffect();
    public abstract bool CheckCondition();

    public (string condition, string effect) GetDescription() => (_condition, _effect);

    public virtual (string condition, string effect, Sprite suit, Color color) GetData()
        => (_condition, _effect, _suit, _color);
}