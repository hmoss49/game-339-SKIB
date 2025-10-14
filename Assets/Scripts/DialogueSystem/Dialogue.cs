using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [Header("Identification")]
        public string dialogueID;

        [Header("Display Info")]
        public string characterName;
        public string dialoguePrompt;

        [Header("Content")]
        public DialogueLine[] dialogueLines;

        [Header("Unlock Conditions")]
        public bool requiresCondition = false;
        public string requiredDialogueID;
        
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(dialogueID))
            {
                Debug.LogWarning($"Dialogue '{name}' is missing a Dialogue ID!", this);
                return false;
            }

            if (dialogueLines == null || dialogueLines.Length == 0)
            {
                Debug.LogWarning($"Dialogue '{dialogueID}' has no dialogue lines!", this);
                return false;
            }

            return true;
        }
    }
}