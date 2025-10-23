using UnityEngine;

public class LocationDiscovery : MonoBehaviour
{
    public string locationID = "SecretPassageFound";

    private void Start()
    {
        if (GameStateManager.Instance != null && !string.IsNullOrEmpty(locationID))
        {
            GameStateManager.Instance.MarkLocationDiscovered(locationID);
        }
    }
}