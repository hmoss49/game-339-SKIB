using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;

        public void TriggerDialogue()
        {
            if (dialogue != null && DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(dialogue);
            }
        }
    }
}
