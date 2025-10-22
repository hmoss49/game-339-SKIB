using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewInventory : MonoBehaviour
{
    public TextMeshProUGUI InventoryText;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;

        // Subscribe to inventory changes
        inventoryManager.inventory.ChangeEvent += OnInventoryChanged;

        // Initialize display
        OnInventoryChanged(inventoryManager.inventory.Value);
    }

    private void OnInventoryChanged(List<string> items)
    {
        if (InventoryText == null)
            return;

        InventoryText.text = items.Count == 0
            ? "Inventory is empty"
            : "Inventory:\n" + string.Join("\n", items);
    }

    private void OnDestroy()
    {
        if (inventoryManager != null)
            inventoryManager.inventory.ChangeEvent -= OnInventoryChanged;
    }
}