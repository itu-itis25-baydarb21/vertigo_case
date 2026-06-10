using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<RewardData, int> collectedRewards = new Dictionary<RewardData, int>();

    public Action<Dictionary<RewardData, int>> OnInventoryUpdated;

    public void AddReward(RewardData reward, int amount)
    {
        if (collectedRewards.ContainsKey(reward))
        {
            collectedRewards[reward] += amount;
        }
        else
        {
            collectedRewards.Add(reward, amount);
        }

        OnInventoryUpdated?.Invoke(collectedRewards);
    }

    public int GetItemAmount(RewardData item)
    {
        if (collectedRewards.ContainsKey(item))
        {
            return collectedRewards[item];
        }
        return 0;
    }

    public void ClearInventory()
    {
        collectedRewards.Clear();
        OnInventoryUpdated?.Invoke(collectedRewards);
    }

    public bool HasItem(RewardData item, int amount)
    {
        if (collectedRewards.ContainsKey(item))
        {
            return collectedRewards[item] >= amount;
        }
        return false;
    }

    public void ConsumeItem(RewardData item, int amount)
    {
        if (HasItem(item, amount))
        {
            collectedRewards[item] -= amount;

            if (collectedRewards[item] <= 0)
            {
                collectedRewards.Remove(item);
            }

            OnInventoryUpdated?.Invoke(collectedRewards);
        }
    }
}