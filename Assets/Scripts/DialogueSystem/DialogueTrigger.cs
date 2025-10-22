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

            if (dialoguesToShow.Length == 0)
                return;

            if (button != null)
                button.Select();

            if (dialoguesToShow.Length == 1)
                DialogueManager.Instance.StartDialogue(dialoguesToShow[0], gameObject);
            else
                DialogueManager.Instance.ShowDialogueChoices(dialoguesToShow);
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

            if (GameStateManager.Instance != null)
                return GameStateManager.Instance.HasCompletedDialogue(dialogue.requiredDialogueID);

            return false;
        }
    }
}