using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.Interfaces;
using Game.Core;

namespace Game.UI
{
    public class InventoryUIController : MonoBehaviour
    {
        [Header("Inventory UI")]
        public Transform ui_panel_inventory_content; 
        public InventorySlotUI inventorySlotPrefab;  

        private IInventoryService inventoryService;

        private void Start()
        {
            inventoryService = ServiceLocator.Get<IInventoryService>();
            if (inventoryService != null)
            {
                inventoryService.OnInventoryUpdated += RefreshInventoryText;
            }
        }

        private void OnDestroy()
        {
            if (inventoryService != null)
            {
                inventoryService.OnInventoryUpdated -= RefreshInventoryText;
            }
        }

        private void RefreshInventoryText(Dictionary<RewardData, int> rewards)
        {
            if (ui_panel_inventory_content == null || inventorySlotPrefab == null) return;

            foreach (Transform child in ui_panel_inventory_content)
            {
                Destroy(child.gameObject);
            }

            foreach (var item in rewards)
            {
                InventorySlotUI newSlot = Instantiate(inventorySlotPrefab, ui_panel_inventory_content);
                newSlot.SetupSlot(item.Key.icon, item.Value);
            }
        }
    }
}
