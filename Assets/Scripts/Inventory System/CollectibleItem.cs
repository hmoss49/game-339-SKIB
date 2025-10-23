using UnityEditor;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;

    public void PickUp()
    {
        InventoryManager.Instance.AddItemToInventory(itemName);
        Destroy(gameObject);
    }
}
