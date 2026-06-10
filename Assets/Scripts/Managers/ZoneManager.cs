using UnityEngine;
using System;

public enum ZoneType
{
    Normal, 
    Safe,   
    Super  
}

public class ZoneManager : MonoBehaviour
{
    public int currentZone = 1;
    public ZoneType currentZoneType;

    public Action<int, ZoneType> OnZoneChanged;

    private void Start()
    {
        UpdateZoneState();
    }

    public void MoveToNextZone()
    {
        currentZone++;
        UpdateZoneState();
    }

    public void ResetZone()
    {
        currentZone = 1;
        UpdateZoneState();
    }

    public void RefreshCurrentZone()
    {
        UpdateZoneState();
    }

    public ZoneType GetZoneType(int zoneNumber)
    {
        if (zoneNumber % 30 == 0) return ZoneType.Super;
        if (zoneNumber % 5 == 0) return ZoneType.Safe;
        return ZoneType.Normal;
    }

    private void UpdateZoneState()
    {
        currentZoneType = GetZoneType(currentZone);

        OnZoneChanged?.Invoke(currentZone, currentZoneType);
    }
}