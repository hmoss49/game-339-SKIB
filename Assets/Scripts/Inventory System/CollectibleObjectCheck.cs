using UnityEngine;

public class CollectibleObjectCheck : MonoBehaviour
{
    public GameObject collectibleObject;
    public string collectibleObjectTag;
    private void Update()
    {
        if (InventoryManager.Instance.inventory.Value.Contains(collectibleObjectTag))
        {
            collectibleObject.SetActive(false);
        }
    }
}
