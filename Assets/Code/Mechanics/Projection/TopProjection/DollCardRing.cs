using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class DollCardRing : MonoBehaviour, IInitializable
{
    [SerializeField] private float _radius;

    private Dictionary<ClockNum, DollCardView> _dollCards;

    private IDollPlacementController _placementController;
    private Palette _palette;
    public void Initialize()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
        SignalBus.Subscribe<PlacementChangedSignal>(this, ReGenerate);
        SignalBus.Subscribe<CurrentPlaceChangedSignal>(this, signal => PushCard(signal.CurrentPlace));
        _dollCards = new();

        Generate();
    }


    public void ReGenerate()
    {
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            if (_dollCards[i] == null)
                continue;
            Destroy(_dollCards[i].gameObject);
        }
        Generate();
    }
    private void  Generate()
    {
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            DollCardView dollCard = Instantiate(_palette.DollCardPrefab, transform);

            dollCard.Initialize(_palette.DollsData.First(x => x.Index == _placementController.GetDollIndex(i)).Symbol);

            float angle = (i * 360f / 12);
            float radians = angle * Mathf.Deg2Rad;

            Vector2 offset = new Vector2(
                Mathf.Sin(radians) * _radius,
                Mathf.Cos(radians) * _radius
            );

            dollCard.transform.localPosition = offset;

            Vector2 direction = transform.position - dollCard.transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            dollCard.transform.localEulerAngles = new Vector3(0, 0, targetAngle + 90f);

            _dollCards[i] = dollCard;
        }
    }

    private void PushCard(ClockNum place)
    {
        float angle;
        float radians;
        Vector2 offset;

        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            if (_dollCards[i] == null) continue;

            angle = (i * 360f / 12);
            radians = angle * Mathf.Deg2Rad;

            offset = new Vector2(
                Mathf.Sin(radians) * _radius,
                Mathf.Cos(radians) * _radius
            );

            _dollCards[i].transform.localPosition = offset;
        }

        var currentCard = _dollCards[place];
        angle = (place * 360f / 12);
        radians = angle * Mathf.Deg2Rad;

        float pushedRadius = _radius * 1.2f;

        offset = new Vector2(
            Mathf.Sin(radians) * pushedRadius,
            Mathf.Cos(radians) * pushedRadius
        );

        currentCard.transform.localPosition = offset;
    }
}