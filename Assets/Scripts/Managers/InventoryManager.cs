using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Game.Interfaces;
using Game.Core;

namespace Game.Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryService
    {
        private Dictionary<RewardData, int> collectedRewards = new Dictionary<RewardData, int>();

        public event Action<Dictionary<RewardData, int>> OnInventoryUpdated;

        private void Awake()
        {
            ServiceLocator.Register<IInventoryService>(this);
        }

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
}