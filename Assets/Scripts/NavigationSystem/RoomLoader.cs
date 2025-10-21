using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameLogger.Instance.LogGameEvent("SceneLoaded", $"SceneName: {scene.name}");
    }

    public void GoToRoom(string roomName)
    {
        if (!string.IsNullOrEmpty(roomName))
        {
            GameLogger.Instance.LogGameEvent("SceneChangeRequested", $"ToScene: {roomName}");
            SceneManager.LoadScene(roomName);
        }
        else
        {
            Debug.LogWarning("GoToRoom called with an empty room name.");
        }
    }
}