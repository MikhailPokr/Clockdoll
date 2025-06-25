using UnityEngine;

internal abstract class AnokCard : BaseCard
{
    protected IReplaceManager _replaceManager;

    public abstract string StringKey { get; }
    public abstract int Number { get; }

    public AnokCard() : base()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _replaceManager = ServiceLocator.Resolve<IReplaceManager>();
        _textHandler = ServiceLocator.Resolve<ITextHandler>();

        _color = Color.black;

        _effect = _textHandler.ReturnJsonData(StringKey).content;
        _suit = _palette.Numbers[Number];
    }

    public override bool CheckCondition()
    { return true; }

    public abstract override void PlayEffect();
}
