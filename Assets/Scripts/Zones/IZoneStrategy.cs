using Game.Utilities;
using UnityEngine;

namespace Game.Zones
{
    public interface IZoneStrategy
    {
        ZoneType GetZoneType();
        string GetHeaderString();
        Color32 GetHeaderColor();
        string GetInfoString();
        int GetZoneMultiplier();
    }
}
