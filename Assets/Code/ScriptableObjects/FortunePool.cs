using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FortunePool", menuName = "Game/FortunePool")]
internal partial class FortunePool : ScriptableObject
{
    public List<Reward> RewardList;
}
