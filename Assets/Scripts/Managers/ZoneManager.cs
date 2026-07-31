using UnityEngine;
using System;
using Game.Interfaces;
using Game.Core;
using Game.Utilities;

namespace Game.Zones
{
    public class ZoneManager : MonoBehaviour, IZoneService
    {
        public int CurrentZone { get; private set; } = 1;
        public ZoneType CurrentZoneType { get; private set; }
        
        public IZoneStrategy CurrentStrategy { get; private set; }

        public event Action<int, ZoneType> OnZoneChanged;

        private void Awake()
        {
            ServiceLocator.Register<IZoneService>(this);
        }

        private void Start()
        {
            UpdateZoneState();
        }

        public void MoveToNextZone()
        {
            CurrentZone++;
            UpdateZoneState();
        }

        public void ResetZone()
        {
            CurrentZone = 1;
            UpdateZoneState();
        }

        public void RefreshCurrentZone()
        {
            UpdateZoneState();
        }

        public ZoneType GetZoneType(int zoneNumber)
        {
            if (zoneNumber % GameConstants.SUPER_ZONE_INTERVAL == 0) return ZoneType.Super;
            if (zoneNumber % GameConstants.SAFE_ZONE_INTERVAL == 0) return ZoneType.Safe;
            return ZoneType.Normal;
        }

        private IZoneStrategy GetStrategyForZone(int zoneNumber)
        {
            if (zoneNumber % GameConstants.SUPER_ZONE_INTERVAL == 0) return new SuperZoneStrategy();
            if (zoneNumber % GameConstants.SAFE_ZONE_INTERVAL == 0) return new SafeZoneStrategy();
            return new NormalZoneStrategy();
        }

        private void UpdateZoneState()
        {
            CurrentZoneType = GetZoneType(CurrentZone);
            CurrentStrategy = GetStrategyForZone(CurrentZone);

            OnZoneChanged?.Invoke(CurrentZone, CurrentZoneType);
        }
    }
}