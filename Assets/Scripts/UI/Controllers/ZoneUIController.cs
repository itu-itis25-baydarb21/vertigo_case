using UnityEngine;
using TMPro;
using Game.Interfaces;
using Game.Core;
using Game.Zones;

namespace Game.UI
{
    public class ZoneUIController : MonoBehaviour
    {
        [Header("Dynamic Texts")]
        public TextMeshProUGUI ui_text_zone_value;
        public TextMeshProUGUI ui_text_header_value; 
        public TextMeshProUGUI ui_text_info_value;

        private IZoneService zoneService;

        private void Start()
        {
            zoneService = ServiceLocator.Get<IZoneService>();
            if (zoneService != null)
            {
                zoneService.OnZoneChanged += RefreshZoneText;
                RefreshZoneText(zoneService.CurrentZone, zoneService.CurrentZoneType);
            }
        }

        private void OnDestroy()
        {
            if (zoneService != null)
            {
                zoneService.OnZoneChanged -= RefreshZoneText;
            }
        }

        private void RefreshZoneText(int zone, ZoneType type)
        {
            if (ui_text_zone_value != null)
            {
                ui_text_zone_value.text = $"ZONE {zone}";
            }

            var strategy = ((ZoneManager)zoneService).CurrentStrategy;
            if (strategy != null && ui_text_header_value != null)
            {
                ui_text_header_value.text = strategy.GetHeaderString();
                ui_text_header_value.color = strategy.GetHeaderColor();
                if (ui_text_info_value != null) 
                    ui_text_info_value.text = strategy.GetInfoString();
            }
        }
    }
}
