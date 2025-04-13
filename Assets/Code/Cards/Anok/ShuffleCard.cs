using System.Collections.Generic;

internal class ShuffleCard : AnokCard
{
    private TableData _tableData;

    public ShuffleCard() : base()
    {
        _tableData = ServiceLocator.Resolve<TableData>();

        _suit = _palette.Numbers[5];
    }

    public override void PlayEffect()
    {
        _tableData.GeneratePlaces();
    }
}