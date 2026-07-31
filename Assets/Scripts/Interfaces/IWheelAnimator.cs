using System;
using Game.Data;

namespace Game.Interfaces
{
    public interface IWheelAnimator
    {
        void SpinWheel(Action<RewardData> onComplete);
    }
}
