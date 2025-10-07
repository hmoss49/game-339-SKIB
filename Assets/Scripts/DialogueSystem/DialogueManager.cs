using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [Header("UI References")] 
        public GameObject dialogueBox;
        public TextMeshProUGUI characterNameText;
        public TextMeshProUGUI dialogueText;
        public Button nextButton;
        public AudioSource audioSource;

        private Dialogue currentDialogue;
        private int currentLineIndex = 0;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        
        void Start()
        {
            dialogueBox.SetActive(false);
            nextButton.onClick.AddListener(DisplayNextLine);
        }

        public void StartDialogue(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentLineIndex = 0;
            dialogueBox.SetActive(true);
            
            characterNameText.text = dialogue.characterName;
            DisplayCurrentLine();
        }

        void DisplayCurrentLine()
        {
            if (currentLineIndex < currentDialogue.dialogueLines.Length)
            {
                DialogueLine line = currentDialogue.dialogueLines[currentLineIndex];
                dialogueText.text = line.text;
                
                // Play audio if available
                if (line.audioClip != null && audioSource != null)
                {
                    audioSource.clip = line.audioClip;
                    audioSource.Play();
                }
                
                // Update button text and visibility
                bool isLastLine = currentLineIndex >= currentDialogue.dialogueLines.Length - 1;
                nextButton.gameObject.SetActive(true);
                    
                TextMeshProUGUI buttonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = isLastLine ? "Close" : "Next";
                }
            }
        }

        public void DisplayNextLine()
        {
            currentLineIndex++;

            if (currentLineIndex < currentDialogue.dialogueLines.Length)
            {
                DisplayCurrentLine();
            }
            else
            {
                EndDialogue();
            }
        }

        void EndDialogue()
        {
            dialogueBox.SetActive(false);
            currentDialogue = null;
            currentLineIndex = 0;
            
            if (audioSource != null)
                audioSource.Stop();
        }
    }
}
