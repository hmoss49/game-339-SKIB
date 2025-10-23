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

        if (items.Count == 0)
        {
            InventoryText.text = "Inventory is empty";
            return;
        }

        string display = "Inventory:\n";
        foreach (var item in items)
        {
            display += $"- {item} x 1\n";
        }

        InventoryText.text = display.TrimEnd();
    }


    private void OnDestroy()
    {
        if (inventoryManager != null)
            inventoryManager.inventory.ChangeEvent -= OnInventoryChanged;
    }
}