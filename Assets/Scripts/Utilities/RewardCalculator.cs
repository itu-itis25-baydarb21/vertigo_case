using UnityEngine;
using Game.Data;
using Game.Zones;

namespace Game.Utilities
{
    public static class RewardCalculator
    {
        public static int CalculateFinalAmount(RewardData data, int currentZone, ZoneType currentZoneType)
        {
            float zoneProgression = 1f + (currentZone * data.zoneMultiplier);
            int finalAmount = Mathf.RoundToInt(data.baseAmount * zoneProgression);

            if (currentZoneType == ZoneType.Safe)
            {
                finalAmount = Mathf.RoundToInt(finalAmount * EconomyConstants.SAFE_ZONE_MULTIPLIER);
            }
            else if (currentZoneType == ZoneType.Super)
            {
                finalAmount = Mathf.RoundToInt(finalAmount * EconomyConstants.SUPER_ZONE_MULTIPLIER);
            }

            return Mathf.Max(data.baseAmount, finalAmount);
        }
    }
}
