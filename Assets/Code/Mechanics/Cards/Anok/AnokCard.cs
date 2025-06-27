using UnityEngine;

internal abstract class AnokCard : BaseCard
{

    public abstract string StringKey { get; }
    public abstract int Number { get; }

    public AnokCard() : base()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _textHandler = ServiceLocator.Resolve<ITextHandler>();

        _color = Color.black;

        _effect = _textHandler.ReturnJsonData(StringKey).content;
        _suit = _palette.Numbers[Number];
    }

    public override bool CheckCondition()
    { return true; }

    public abstract override void PlayEffect(out IRequireLock requireLock);
}
