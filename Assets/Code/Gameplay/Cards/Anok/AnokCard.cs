using UnityEngine;

internal abstract class AnokCard : BaseCard
{
    protected IReplaceManager _replaceManager;
    protected Palette _palette;

    public AnokCard()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _replaceManager = ServiceLocator.Resolve<IReplaceManager>();
        _color = Color.black;
    }

    public override bool CheckCondition()
    {  return true; }

    public abstract override void PlayEffect();
}
