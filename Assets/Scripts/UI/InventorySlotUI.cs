using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemAmountText;

    public void SetupSlot(Sprite icon, int amount)
    {
        itemIcon.sprite = icon;
        itemAmountText.text = $"x{amount}";
    }
}