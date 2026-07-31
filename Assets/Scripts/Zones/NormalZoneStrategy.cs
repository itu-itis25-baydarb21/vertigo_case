using Game.Utilities;
using UnityEngine;

namespace Game.Zones
{
    public class NormalZoneStrategy : IZoneStrategy
    {
        public ZoneType GetZoneType() => ZoneType.Normal;
        public string GetHeaderString() => UIConstants.BRONZE_SPIN_HEADER;
        public Color32 GetHeaderColor() => UIConstants.BRONZE_COLOR;
        public string GetInfoString() => UIConstants.INFO_SPIN_TO_WIN;
        public int GetZoneMultiplier() => 1;
    }
}
