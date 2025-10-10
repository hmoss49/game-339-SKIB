using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    public void GoToRoom(string roomName)
    {
        if (!string.IsNullOrEmpty(roomName))
            SceneManager.LoadScene(roomName);
        else
            Debug.LogWarning("GoToRoom called with an empty room name.");
    }
}