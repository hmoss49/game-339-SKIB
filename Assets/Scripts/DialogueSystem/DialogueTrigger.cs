using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue[] availableDialogues;
        private Button button;

        void Awake()
        {
            button = GetComponent<Button>();
        }

        public void TriggerDialogue()
        {
            if (DialogueManager.Instance == null) return;

            Dialogue[] dialoguesToShow = GetAvailableDialogues();
            if (dialoguesToShow.Length == 0) return;

            if (button != null)
                button.Select();

            if (dialoguesToShow.Length == 1)
                DialogueManager.Instance.StartDialogue(dialoguesToShow[0], gameObject);
            else
                DialogueManager.Instance.ShowDialogueChoices(dialoguesToShow, gameObject);
        }

        Dialogue[] GetAvailableDialogues()
        {
            List<Dialogue> available = new List<Dialogue>();

            foreach (Dialogue dialogue in availableDialogues)
            {
                if (IsDialogueAvailable(dialogue))
                    available.Add(dialogue);
            }

            return available.ToArray();
        }

        bool IsDialogueAvailable(Dialogue dialogue)
        {
            if (!dialogue.requiresCondition)
                return true;

            // Check dialogue requirement
            if (!string.IsNullOrEmpty(dialogue.requiredDialogueID))
            {
                if (GameStateManager.Instance == null || 
                    !GameStateManager.Instance.HasCompletedDialogue(dialogue.requiredDialogueID))
                    return false;
            }

            // Check item requirement
            if (!string.IsNullOrEmpty(dialogue.requiredItemName))
            {
                if (InventoryManager.Instance == null || 
                    !InventoryManager.Instance.inventory.Value.Contains(dialogue.requiredItemName))
                    return false;
            }

            // Check location requirement
            if (!string.IsNullOrEmpty(dialogue.requiredLocationID))
            {
                if (GameStateManager.Instance == null || 
                    !GameStateManager.Instance.HasDiscoveredLocation(dialogue.requiredLocationID))
                    return false;
            }

            // Check accusation requirement
            if (dialogue.requiresCorrectAccusation)
            {
                if (GameStateManager.Instance == null || 
                    !GameStateManager.Instance.HasAccusedCorrectly())
                    return false;
            }

            return true;
        }
    }
}