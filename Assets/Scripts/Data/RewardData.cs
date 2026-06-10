using UnityEngine;

[CreateAssetMenu(fileName = "NewRewardData", menuName = "Vertigo/Reward Data")]
public class RewardData : ScriptableObject
{
    [Header("Reward Info")]
    public string rewardName;
    public RewardType type;
    public Sprite icon;

    [Header("Value Settings")]
    public int baseAmount;

    public float zoneMultiplier = 1f;
}