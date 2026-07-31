using Game.Utilities;
using UnityEngine;

namespace Game.Zones
{
    public class SafeZoneStrategy : IZoneStrategy
    {
        public ZoneType GetZoneType() => ZoneType.Safe;
        public string GetHeaderString() => UIConstants.SILVER_SPIN_HEADER;
        public Color32 GetHeaderColor() => UIConstants.SILVER_COLOR;
        public string GetInfoString() => UIConstants.INFO_UP_TO_2X;
        public int GetZoneMultiplier() => (int)EconomyConstants.SAFE_ZONE_MULTIPLIER;
    }
}
