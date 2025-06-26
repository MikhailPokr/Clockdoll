using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class PopUpView : MonoBehaviour, IInitializable
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    private IGame _game;
    private IGameSubStateMachine _subStateMachine;
    private IFortuneSystem _fortuneSystem;
    private ICardSystem _cardSystem;

    public void Initialize()
    {
        _game = ServiceLocator.Resolve<IGame>();
        _subStateMachine = ServiceLocator.Resolve<IGameSubStateMachine>();
        _fortuneSystem = ServiceLocator.Resolve<IFortuneSystem>();
        _cardSystem = ServiceLocator.Resolve<ICardSystem>();

        SignalBus.Subscribe<SubStateChangedSignal>(this, OnStateChanged);
        SignalBus.Subscribe<RewardReceivedSignal>(this, signal => OnRewardReceived(signal.Reward));
        SignalBus.Subscribe<CardPlayedSignal>(this, OnCardPlayed);
    }

    private void OnCardPlayed(CardPlayedSignal signal)
    {
        _image.gameObject.SetActive(true);

        if (_subStateMachine.IsPedroTurn)
        {
            if (signal.Card == null)
            {
                _text.text = $"Педро ничего не сыграл";
                return;
            }
            PedroCard pedroCard = signal.Card as PedroCard;
            var description = signal.Card.GetDescription();
            _text.text = $"{description.condition} / {description.effect}";
        }
    }

    private void OnRewardReceived(Reward reward)
    {
        _image.gameObject.SetActive(true);

        switch (reward.Type)
        {
            case RewardType.RegenerateAnok:
            case RewardType.SpadeCard:
                _text.text = string.Format(reward.AlertDescription, reward.Value);
                break;
            case RewardType.Card:
                _text.text = string.Format(reward.AlertDescription, _subStateMachine.IsPedroTurn ? "Педро" : "Анок", reward.Value);
                break;
            case RewardType.OrderTasties:
                _text.text = string.Format(reward.AlertDescription, _subStateMachine.CurrentPlaceNumber);
                break;
        }
    }

    private void OnStateChanged(SubStateChangedSignal signal)
    {
        if (signal.GameSubState.ToString().EndsWith("StartTurn"))
        {
            _image.gameObject.SetActive(true);
            _text.text = signal.GameSubState.ToString();
        }
    }

    public void Click()
    {
        if (_game.AlertClick())
            _image.gameObject.SetActive(false);
    }
}
