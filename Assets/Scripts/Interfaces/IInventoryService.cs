using System;
using System.Collections.Generic;
using Game.Data;

namespace Game.Interfaces
{
    public interface IInventoryService
    {
        event Action<Dictionary<RewardData, int>> OnInventoryUpdated;
        void AddReward(RewardData data, int amount);
        int GetItemAmount(RewardData data);
        void ClearInventory();
        bool HasItem(RewardData item, int amount);
        void ConsumeItem(RewardData item, int amount);
    }
}
