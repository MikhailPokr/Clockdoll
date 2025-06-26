internal struct RewardReceivedSignal : ISignal
{
    public Reward Reward { get; }

    public RewardReceivedSignal(Reward reward)
    {
        Reward = reward;
    }
}