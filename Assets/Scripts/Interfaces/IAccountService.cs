using System;

namespace Game.Interfaces
{
    public interface IAccountService
    {
        event Action<int> OnTotalGoldChanged;
        int GetTotalGold();
        void AddGold(int amount);
        bool ConsumeGold(int amount);
    }
}
