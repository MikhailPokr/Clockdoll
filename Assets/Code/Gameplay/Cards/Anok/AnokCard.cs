using UnityEngine;

internal abstract class AnokCard : BaseCard
{
    protected ReplaceManager _replaceManager;
    protected Palette _palette;

    public AnokCard()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _replaceManager = ServiceLocator.Resolve<ReplaceManager>();
        _color = Color.black;
    }

    public override bool CheckCondition()
    {  return true; }

    public abstract override void PlayEffect();
}
