using Game.Utilities;
using UnityEngine;

namespace Game.Zones
{
    public class SuperZoneStrategy : IZoneStrategy
    {
        public ZoneType GetZoneType() => ZoneType.Super;
        public string GetHeaderString() => UIConstants.GOLDEN_SPIN_HEADER;
        public Color32 GetHeaderColor() => UIConstants.GOLDEN_COLOR;
        public string GetInfoString() => UIConstants.INFO_UP_TO_10X;
        public int GetZoneMultiplier() => (int)EconomyConstants.SUPER_ZONE_MULTIPLIER;
    }
}
