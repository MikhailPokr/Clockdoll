using UnityEngine;

internal abstract class AnokCard : BaseCard
{
    protected IReplaceManager _replaceManager;
    protected ITextHandler _textHandler;
    protected Palette _palette;
    protected int suitNumber;

    public AnokCard()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _replaceManager = ServiceLocator.Resolve<IReplaceManager>();
        _textHandler = ServiceLocator.Resolve<ITextHandler>();

        _color = Color.black;
    }

    protected void ApplyEffectText(int suitNumber)
    {
        _effect = _textHandler.ReturnJsonData("cards_anok", suitNumber).content;
        _suit = _palette.Numbers[suitNumber];
    }

    public override bool CheckCondition()
    { return true; }

    public abstract override void PlayEffect();
}
