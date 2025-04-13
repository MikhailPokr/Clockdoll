using UnityEngine;

internal partial class PedroCard : BaseCard
{
    public PedroCard(BaseCondition condition, BaseEffect effect)
    {
        
    }

    public override bool CheckCondition()
    {
        return false;
    }

    public override void PlayEffect()
    {
        Debug.Log($"Сыграна карта {_condition}");
    }
}
