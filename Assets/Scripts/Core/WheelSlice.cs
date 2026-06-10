using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WheelSlice : MonoBehaviour
{
    public RewardData sliceData;

    [Header("UI References")]
    public Image iconImage;
    public TextMeshProUGUI amountText;

    public void SetupSlice(RewardData data, int currentZone)
    {
        sliceData = data;
        iconImage.sprite = data.icon;

        
        int finalAmount = Mathf.RoundToInt(data.baseAmount * (1 + (currentZone * data.zoneMultiplier)));

        if (data.type == RewardType.Bomb)
        {
            amountText.text = "";
        }
        else
        {
            amountText.text = "x" + finalAmount.ToString();
        }
    }
}