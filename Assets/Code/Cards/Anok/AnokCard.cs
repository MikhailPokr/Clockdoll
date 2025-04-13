using UnityEngine;

internal abstract class AnokCard : BaseCard
{
    public AnokCard(Palette palette)
    {
        
    }

    public abstract override bool CheckCondition();

    public abstract override void PlayEffect();
}
