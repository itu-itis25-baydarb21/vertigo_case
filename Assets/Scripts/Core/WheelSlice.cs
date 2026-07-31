using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Data;
using Game.Utilities;

namespace Game.Wheel
{
    public class WheelSlice : MonoBehaviour
    {
        public Image iconImage;
        public TextMeshProUGUI amountText;
        public RewardData sliceData;

        public void SetupSlice(RewardData data, int currentZone)
        {
            sliceData = data;
            
            if (iconImage != null)
                iconImage.sprite = data.icon;
            
            if (amountText != null)
            {
                if (data.type == RewardType.Bomb)
                {
                    amountText.text = "";
                }
                else
                {
                    // Use standard calculation but without zone type multipliers for base display (as original did)
                    // Actually, the original did simple progression:
                    float zoneProgression = 1f + (currentZone * data.zoneMultiplier);
                    int displayAmount = Mathf.RoundToInt(data.baseAmount * zoneProgression);
                    amountText.text = RewardFormatter.FormatAmount(displayAmount);
                }
            }
        }
    }
}