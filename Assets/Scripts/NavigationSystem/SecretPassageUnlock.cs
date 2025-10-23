using System;
using UnityEngine;

public class SecretPassageUnlock : MonoBehaviour
{
    public GameObject secretPassage;
    private void Update()
    {
        if (InventoryManager.Instance.inventory.Value.Contains("Keys"))
        {
            secretPassage.SetActive(true);
        }
    }
}
