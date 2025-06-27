internal class PedroCard : BaseCard
{
    protected BaseCondition _logicCondition;
    protected BaseEffect _logicEffect;
    public PedroCard(BaseCondition condition, BaseEffect effect)
    {
        _logicCondition = condition;
        _logicEffect = effect;

        _condition = _textHandler.ReturnJsonData(condition.StringKey).content;
        _effect = _textHandler.ReturnJsonData(effect.StringKey).content;
    }

    public override bool CheckCondition() => _logicCondition.Check();

    public override void PlayEffect(out IRequireLock requireLock) => _logicEffect.PlayEffect(out requireLock);
}
