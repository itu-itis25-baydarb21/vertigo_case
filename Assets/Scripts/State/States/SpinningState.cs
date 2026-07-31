using UnityEngine;
using Game.Interfaces;
using Game.Core;
using Game.Data;

namespace Game.State
{
    public class SpinningState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering SpinningState");
            var wheelAnimator = ServiceLocator.Get<IWheelAnimator>();
            if (wheelAnimator != null)
            {
                wheelAnimator.SpinWheel(OnSpinComplete);
            }
            else
            {
                Debug.LogError("No IWheelAnimator found!");
                GameStateMachine.Instance.ChangeState<IdleState>();
            }
        }

        public void Update() { }

        public void Exit()
        {
            Debug.Log("Exiting SpinningState");
        }

        private void OnSpinComplete(RewardData wonData)
        {
            var rewardState = new RewardAnimationState();
            rewardState.SetReward(wonData);
            GameStateMachine.Instance.RegisterState(rewardState);
            GameStateMachine.Instance.ChangeState<StoppingState>();
        }
    }
}
