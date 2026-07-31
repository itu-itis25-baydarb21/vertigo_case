using System;
using Game.Zones;

namespace Game.Interfaces
{
    public interface IZoneService
    {
        event Action<int, ZoneType> OnZoneChanged;
        int CurrentZone { get; }
        ZoneType CurrentZoneType { get; }
        void MoveToNextZone();
        void ResetZone();
        void RefreshCurrentZone();
        ZoneType GetZoneType(int zoneNumber);
    }
}
