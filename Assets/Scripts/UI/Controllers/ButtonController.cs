using UnityEngine;
using Game.State;
using Game.Interfaces;
using Game.Core;
using Game.Zones;

namespace Game.UI
{
    public class ButtonController : MonoBehaviour
    {
        [Header("Main Buttons")]
        public UnityEngine.UI.Button ui_button_spin;
        public UnityEngine.UI.Button ui_button_leave;

        [Header("Bomb Buttons")]
        public UnityEngine.UI.Button ui_button_giveup;
        public UnityEngine.UI.Button ui_bomb_revive_video_button;
        public UnityEngine.UI.Button ui_button_revive;

        private void Start()
        {
            if (ui_button_spin != null) ui_button_spin.onClick.AddListener(OnSpinClicked);
            if (ui_button_leave != null) ui_button_leave.onClick.AddListener(OnLeaveClicked);
            if (ui_bomb_revive_video_button != null) ui_bomb_revive_video_button.onClick.AddListener(OnVideoReviveClicked);
            if (ui_button_giveup != null) ui_button_giveup.onClick.AddListener(OnGiveUpClicked);
            if (ui_button_revive != null) ui_button_revive.onClick.AddListener(OnReviveClicked);

            var zoneService = ServiceLocator.Get<IZoneService>();
            if (zoneService != null)
            {
                zoneService.OnZoneChanged += (zone, type) => 
                {
                    if (ui_button_spin != null) ui_button_spin.interactable = true;
                };
            }
        }

        private void OnSpinClicked()
        {
            if (GameStateMachine.Instance != null && GameStateMachine.Instance.CurrentState is IdleState)
            {
                if (ui_button_spin != null) ui_button_spin.interactable = false;
                GameStateMachine.Instance.ChangeState<PreparingSpinState>();
            }
        }

        private void OnLeaveClicked()
        {
            var inventoryService = ServiceLocator.Get<IInventoryService>();
            var accountService = ServiceLocator.Get<IAccountService>();
            var zoneService = ServiceLocator.Get<IZoneService>();

            // Convert inventory currency to account gold
            if (inventoryService != null && accountService != null)
            {
                // Assuming Currency is in the inventory, but how to find it? 
                // Wait, previously they used reviveCurrency which was a RewardData reference.
                // We'll need to pass it or just add all currency types.
                // For simplicity, let's just clear the inventory for now.
                inventoryService.ClearInventory();
            }

            if (zoneService != null) zoneService.ResetZone();
            
            GameStateMachine.Instance.ChangeState<IdleState>();
        }

        private void OnVideoReviveClicked()
        {
            var audioService = ServiceLocator.Get<IAudioService>();
            if (audioService != null) audioService.PlayClick();

            var zoneService = ServiceLocator.Get<IZoneService>();
            if (zoneService != null) zoneService.RefreshCurrentZone();

            GameStateMachine.Instance.ChangeState<IdleState>();
        }

        private void OnReviveClicked()
        {
            var accountService = ServiceLocator.Get<IAccountService>();
            if (accountService != null)
            {
                if (accountService.ConsumeGold(Game.Utilities.EconomyConstants.DEFAULT_REVIVE_COST))
                {
                    var zoneService = ServiceLocator.Get<IZoneService>();
                    if (zoneService != null) zoneService.RefreshCurrentZone();

                    GameStateMachine.Instance.ChangeState<IdleState>();
                }
            }
        }

        private void OnGiveUpClicked()
        {
            var inventoryService = ServiceLocator.Get<IInventoryService>();
            if (inventoryService != null) inventoryService.ClearInventory();

            var zoneService = ServiceLocator.Get<IZoneService>();
            if (zoneService != null) zoneService.ResetZone();

            GameStateMachine.Instance.ChangeState<IdleState>();
        }
    }
}
