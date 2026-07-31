using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Utilities;

namespace Game.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        public Image itemIcon;
        public TextMeshProUGUI itemAmountText;

        public void SetupSlot(Sprite icon, int amount)
        {
            if (itemIcon != null) itemIcon.sprite = icon;
            if (itemAmountText != null) itemAmountText.text = RewardFormatter.FormatAmount(amount);
        }
    }
}