using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Interfaces;
using Game.Core;
using Game.Utilities;

namespace Game.UI
{
    public class EconomyController : MonoBehaviour
    {
        [Header("Dynamic Texts")]
        public TextMeshProUGUI ui_text_total_gold_value; 

        [Header("Bomb Pop-up Elements")]
        public GameObject ui_panel_bomb_popup;
        public Button ui_button_revive;

        private IAccountService accountService;

        private void Start()
        {
            accountService = ServiceLocator.Get<IAccountService>();
            if (accountService != null)
            {
                accountService.OnTotalGoldChanged += RefreshTotalGoldText;
                RefreshTotalGoldText(accountService.GetTotalGold());
            }

            if (ui_panel_bomb_popup != null)
                ui_panel_bomb_popup.SetActive(false);
        }

        private void OnDestroy()
        {
            if (accountService != null)
            {
                accountService.OnTotalGoldChanged -= RefreshTotalGoldText;
            }
        }

        private void RefreshTotalGoldText(int totalGold)
        {
            if (ui_text_total_gold_value != null)
            {
                ui_text_total_gold_value.text = totalGold.ToString();
            }
        }

        public void ShowBombPopup()
        {
            if (ui_panel_bomb_popup != null)
            {
                ui_panel_bomb_popup.SetActive(true);

                if (accountService != null && ui_button_revive != null)
                {
                    bool canRevive = accountService.GetTotalGold() >= EconomyConstants.DEFAULT_REVIVE_COST;
                    ui_button_revive.interactable = canRevive;
                }
            }
        }

        public void HideBombPopup()
        {
            if (ui_panel_bomb_popup != null)
            {
                ui_panel_bomb_popup.SetActive(false);
            }
        }
    }
}
