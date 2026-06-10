using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Main Buttons")]
    public Button ui_button_spin;
    public Button ui_button_leave;

    [Header("Dynamic Texts")]
    public TextMeshProUGUI ui_text_zone_value;
    public TextMeshProUGUI ui_text_header_value; 
    public TextMeshProUGUI ui_text_total_gold_value; 
    public TextMeshProUGUI ui_text_info_value; 

    [Header("Bomb Pop-up Elements")]
    public GameObject ui_panel_bomb_popup;
    public Button ui_button_giveup;
    public Button ui_button_revive;
    public Button ui_bomb_revive_video_button;

    [Header("Managers")]
    public WheelManager wheelManager;
    public InventoryManager inventoryManager;
    public ZoneManager zoneManager;
    public AccountManager accountManager; 

    [Header("Reward Pop-up Elements")]
    public GameObject ui_panel_reward_popup;
    public Image ui_image_shine;
    public Image ui_image_reward_icon;
    public TextMeshProUGUI ui_text_reward_popup; 

    [Header("Revive Economy")]
    public RewardData reviveCurrency; 
    public int reviveCost = 100;      

    [Header("Inventory UI")]
    public Transform ui_panel_inventory_content; 
    public InventorySlotUI inventorySlotPrefab;  

    private void OnValidate()
    {
        Button[] allButtons = GetComponentsInChildren<Button>(true);
        foreach (Button btn in allButtons)
        {
            if (btn.gameObject.name == "ui_button_spin") ui_button_spin = btn;
            else if (btn.gameObject.name == "ui_button_leave") ui_button_leave = btn;
            else if (btn.gameObject.name == "ui_button_revive") ui_button_revive = btn;
            else if (btn.gameObject.name == "ui_button_giveup") ui_button_giveup = btn;
            else if (btn.gameObject.name == "ui_bomb_revive_video_button") ui_bomb_revive_video_button = btn;
        }

        TextMeshProUGUI[] allTexts = GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI txt in allTexts)
        {
            if (txt.gameObject.name == "ui_text_zone_value") ui_text_zone_value = txt;
            else if (txt.gameObject.name == "ui_text_header_value") ui_text_header_value = txt; 
            else if (txt.gameObject.name == "ui_text_total_gold_value") ui_text_total_gold_value = txt; 
            else if (txt.gameObject.name == "ui_text_info_value") ui_text_info_value = txt; 
        }
    }

    public void ShowRewardPopup(Sprite rewardSprite, string rewardName, int amount, Action onAnimationComplete)
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayWin();

        ui_panel_reward_popup.SetActive(true);
        ui_image_reward_icon.sprite = rewardSprite;

        ui_text_reward_popup.text = $"{amount}x {rewardName}";

        ui_image_reward_icon.transform.localScale = Vector3.zero;
        ui_text_reward_popup.transform.localScale = Vector3.zero;
        ui_image_shine.transform.localRotation = Quaternion.identity;

        ui_image_shine.transform.DORotate(new Vector3(0, 0, -360), 4f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

        Sequence seq = DOTween.Sequence();


        seq.Append(ui_image_reward_icon.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBack));
        seq.Join(ui_text_reward_popup.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));

        seq.AppendInterval(1.5f);

        seq.Append(ui_image_reward_icon.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack));
        seq.Join(ui_text_reward_popup.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack));

        seq.OnComplete(() =>
        {
            ui_image_shine.transform.DOKill();
            ui_panel_reward_popup.SetActive(false);
            onAnimationComplete?.Invoke();
        });
    }

    private void Start()
    {
        if (ui_button_spin != null) ui_button_spin.onClick.AddListener(OnSpinClicked);
        if (ui_button_leave != null) ui_button_leave.onClick.AddListener(OnLeaveClicked);
    }

    private void OnEnable()
    {
        if (inventoryManager != null) inventoryManager.OnInventoryUpdated += RefreshInventoryText;
        if (zoneManager != null) zoneManager.OnZoneChanged += RefreshZoneText; 
        if (accountManager != null) accountManager.OnTotalGoldChanged += RefreshTotalGoldText;

        if (ui_bomb_revive_video_button != null) ui_bomb_revive_video_button.onClick.AddListener(OnVideoReviveClicked);
        if (ui_button_giveup != null) ui_button_giveup.onClick.AddListener(OnGiveUpClicked);
        if (ui_button_revive != null) ui_button_revive.onClick.AddListener(OnReviveClicked);
    }

    private void RefreshZoneText(int zone, ZoneType type)
    {
        if (ui_button_spin != null)
        {
            ui_button_spin.interactable = true;
        }

        if (ui_text_zone_value != null)
        {
            ui_text_zone_value.text = $"ZONE {zone}";
        }

        if (ui_text_header_value != null)
        {
            switch (type)
            {
                case ZoneType.Normal:
                    ui_text_header_value.text = "BRONZE SPIN";
                    ui_text_header_value.color = new Color32(205, 127, 50, 255);
                    if (ui_text_info_value != null) ui_text_info_value.text = "SPIN TO WIN";
                    break;

                case ZoneType.Safe: 
                    ui_text_header_value.text = "SILVER SPIN";
                    ui_text_header_value.color = new Color32(192, 192, 192, 255);
                    if (ui_text_info_value != null) ui_text_info_value.text = "UP TO 2X";
                    break;

                case ZoneType.Super: 
                    ui_text_header_value.text = "GOLDEN SPIN";
                    ui_text_header_value.color = new Color32(255, 215, 0, 255);
                    if (ui_text_info_value != null) ui_text_info_value.text = "UP TO 10X";
                    break;
            }
        }
    }

    private void OnVideoReviveClicked()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();

        if (ui_panel_bomb_popup != null)
        {
            ui_panel_bomb_popup.SetActive(false);
        }

        if (zoneManager != null)
        {
            zoneManager.RefreshCurrentZone();
        }
    }

    private void OnDisable()
    {
        if (inventoryManager != null) inventoryManager.OnInventoryUpdated -= RefreshInventoryText;
        if (zoneManager != null) zoneManager.OnZoneChanged -= RefreshZoneText; 
        if (accountManager != null) accountManager.OnTotalGoldChanged -= RefreshTotalGoldText;

        if (ui_bomb_revive_video_button != null) ui_bomb_revive_video_button.onClick.RemoveListener(OnVideoReviveClicked);
        if (ui_button_giveup != null) ui_button_giveup.onClick.RemoveListener(OnGiveUpClicked);
        if (ui_button_revive != null) ui_button_revive.onClick.RemoveListener(OnReviveClicked);
    }

    private void RefreshTotalGoldText(int totalGold)
    {
        if (ui_text_total_gold_value != null)
        {
            ui_text_total_gold_value.text = totalGold.ToString();
        }
    }

    private void OnSpinClicked()
    {
        if (ui_button_spin != null)
        {
            ui_button_spin.interactable = false;
        }

        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();

        if (wheelManager != null)
        {
            wheelManager.SpinWheel();
        }
    }

    private void OnLeaveClicked()
    {
        if (inventoryManager != null && accountManager != null && reviveCurrency != null)
        {
            int sessionGold = inventoryManager.GetItemAmount(reviveCurrency);

            if (sessionGold > 0)
            {
                accountManager.AddGold(sessionGold);
            }

            inventoryManager.ClearInventory();
        }

        if (zoneManager != null)
        {
            zoneManager.ResetZone();
        }
    }

    private void OnReviveClicked()
    {
        if (accountManager != null)
        {
            if (accountManager.ConsumeGold(reviveCost))
            {
                ui_panel_bomb_popup.SetActive(false);
                if (zoneManager != null) zoneManager.RefreshCurrentZone();
            }
        }
    }

    private void OnGiveUpClicked()
    {
        ui_panel_bomb_popup.SetActive(false);
        if (inventoryManager != null) inventoryManager.ClearInventory();
        if (zoneManager != null) zoneManager.ResetZone();
    }

    private void RefreshInventoryText(Dictionary<RewardData, int> rewards)
    {
        if (ui_panel_inventory_content == null || inventorySlotPrefab == null) return;

        foreach (Transform child in ui_panel_inventory_content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in rewards)
        {
            InventorySlotUI newSlot = Instantiate(inventorySlotPrefab, ui_panel_inventory_content);
            newSlot.SetupSlot(item.Key.icon, item.Value);
        }
    }

    public void ShowBombPopup()
    {
        if (ui_panel_bomb_popup != null)
        {
            ui_panel_bomb_popup.SetActive(true);

            if (accountManager != null && ui_button_revive != null)
            {
                bool canRevive = accountManager.GetTotalGold() >= reviveCost;

                ui_button_revive.interactable = canRevive;
            }
        }
    }

    private void OnDestroy()
    {
        if (ui_button_spin != null) ui_button_spin.onClick.RemoveAllListeners();
        if (ui_button_leave != null) ui_button_leave.onClick.RemoveAllListeners();
        if (ui_button_revive != null) ui_button_revive.onClick.RemoveAllListeners();
        if (ui_button_giveup != null) ui_button_giveup.onClick.RemoveAllListeners();
    }
}