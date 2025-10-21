using UnityEngine;

public class Setup : MonoBehaviour
{
    private void Awake()
    {
        GameLogger.Initialize();
        GameLogger.Instance.LogInfo("Game started!");
    }
}