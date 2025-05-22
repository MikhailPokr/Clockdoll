using UnityEngine;

internal abstract class AnokCard : BaseCard
{
    protected IReplaceManager _replaceManager;
    protected Palette _palette;
    protected IDialogueSystem _dialogueSystem;
    protected int _suitNumber; 
    public AnokCard()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _replaceManager = ServiceLocator.Resolve<IReplaceManager>();
        _dialogueSystem = ServiceLocator.Resolve<IDialogueSystem>();
        _color = Color.black;
    }

    protected void ApplySuitText()
    {
        _effect = _dialogueSystem.ReturnJsonData("cards_anok", _suitNumber).content;
        _suit = _palette.Numbers[_suitNumber];
    }

    public override bool CheckCondition()
    { return true; }

    public abstract override void PlayEffect();
}
