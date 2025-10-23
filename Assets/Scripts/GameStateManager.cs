using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private readonly HashSet<string> completedDialogues = new HashSet<string>();
    private readonly HashSet<string> discoveredLocations = new HashSet<string>();
    private bool hasAccusedCorrectly = false;

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

    public void MarkDialogueComplete(string dialogueID)
    {
        if (!string.IsNullOrEmpty(dialogueID))
        {
            completedDialogues.Add(dialogueID);
        }
    }

    public bool HasCompletedDialogue(string dialogueID)
    {
        return !string.IsNullOrEmpty(dialogueID) && completedDialogues.Contains(dialogueID);
    }

    public void MarkLocationDiscovered(string locationID)
    {
        if (!string.IsNullOrEmpty(locationID))
        {
            discoveredLocations.Add(locationID);
            Debug.Log($"[GameStateManager] Location Discovered: {locationID}");
            GameLogger.Instance.LogGameEvent("LocationDiscovered", $"LocationID: {locationID}");
        }
    }

    public bool HasDiscoveredLocation(string locationID)
    {
        return !string.IsNullOrEmpty(locationID) && discoveredLocations.Contains(locationID);
    }

    public void MarkCorrectAccusation()
    {
        hasAccusedCorrectly = true;
        Debug.Log($"[GameStateManager] Correct Accusation Made!");
        GameLogger.Instance.LogGameEvent("CorrectAccusation", "Player accused the correct character");
    }

    public bool HasAccusedCorrectly()
    {
        return hasAccusedCorrectly;
    }
}