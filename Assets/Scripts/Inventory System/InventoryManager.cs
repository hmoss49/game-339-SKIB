using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public ObservableValue<List<string>> inventory = new ObservableValue<List<string>>(new List<string>());
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory(string item)
    {
        var list = inventory.Value;
        if (!list.Contains(item))
        {
            list.Add(item);
            inventory.Value = new List<string>(list); // Trigger change event
        }
    }

    public void RemoveItemFromInventory(string item)
    {
        var list = inventory.Value;
        if (list.Contains(item))
        {
            list.Remove(item);
            inventory.Value = new List<string>(list); // Trigger change event
        }
    }
}
