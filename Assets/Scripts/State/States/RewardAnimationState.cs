using UnityEngine;
using Game.Interfaces;
using Game.Core;
using Game.Data;
using Game.UI;
using Game.Utilities;

namespace Game.State
{
    public class RewardAnimationState : IGameState
    {
        private RewardData wonData;

        public void SetReward(RewardData data)
        {
            wonData = data;
        }

        public void Enter()
        {
            Debug.Log("Entering RewardAnimationState");

            if (wonData == null)
            {
                GameStateMachine.Instance.ChangeState<IdleState>();
                return;
            }

            if (wonData.type == RewardType.Bomb)
            {
                GameStateMachine.Instance.ChangeState<GameOverState>();
            }
            else
            {
                ProcessReward(wonData);
            }
        }

        private void ProcessReward(RewardData data)
        {
            var zoneService = ServiceLocator.Get<IZoneService>();
            var inventoryService = ServiceLocator.Get<IInventoryService>();

            int finalAmount = RewardCalculator.CalculateFinalAmount(data, zoneService.CurrentZone, zoneService.CurrentZoneType);
            
            if (inventoryService != null)
            {
                inventoryService.AddReward(data, finalAmount);
            }

            var rewardPopup = Object.FindObjectOfType<RewardPopupController>();
            if (rewardPopup != null)
            {
                rewardPopup.ShowRewardPopup(data.icon, data.rewardName, finalAmount, () => 
                {
                    GameStateMachine.Instance.ChangeState<TransitionState>();
                });
            }
            else
            {
                GameStateMachine.Instance.ChangeState<TransitionState>();
            }
        }

        public void Update() { }
        public void Exit() { }
    }
}
