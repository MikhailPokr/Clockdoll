using System.Linq;
using UnityEngine;

internal class TableCardDollRing : MonoBehaviour, IInitializable
{
    [SerializeField] private float _radius;

    private DollCard[] _dollCards;

    private TableData _tableData;
    private Palette _palette;
    public void Initialize()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _tableData = ServiceLocator.Resolve<TableData>();
        _tableData.PlacementChanged += ReGenerate;
        _dollCards = new DollCard[13];

        Generate();
    }


    public void ReGenerate()
    {
        for (int i =  0; i < _dollCards.Length; i++)
        {
            if (_dollCards[i] == null)
                continue;
            Destroy(_dollCards[i].gameObject);
        }
        Generate();
    }
    private void  Generate()
    {

        for (int i = 1; i <= 12; i++)
        {
            DollCard dollCard = Instantiate(_palette.DollCardPrefab, transform);

            dollCard.Initialize(_palette.DollsData.First(x => x.Index == _tableData.GetDollIndex(i)).Symbol);

            float angle = (i * 360f / 12);
            float radians = angle * Mathf.Deg2Rad;

            Vector2 offset = new Vector2(
                Mathf.Sin(radians) * _radius,
                Mathf.Cos(radians) * _radius
            );

            dollCard.transform.localPosition = offset;

            Vector2 direction = transform.position - dollCard.transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            dollCard.transform.localEulerAngles = new Vector3(0, 0, targetAngle - 90f);

            _dollCards[i] = dollCard;
        }
    }
}