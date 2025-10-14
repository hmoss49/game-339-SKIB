using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private readonly HashSet<string> completedDialogues = new HashSet<string>();

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
}