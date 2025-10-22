using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [Header("Dialogue UI")]
        public GameObject dialogueBox;
        public TextMeshProUGUI characterNameText;
        public TextMeshProUGUI dialogueText;
        public Button nextButton;
        public AudioSource audioSource;

        [Header("Choice Menu")]
        public GameObject choiceMenuPanel;
        public Transform choiceButtonContainer;
        public GameObject choiceButtonPrefab;

        private Dialogue currentDialogue;
        private int currentLineIndex;
        private List<GameObject> spawnedChoiceButtons = new List<GameObject>();

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
            choiceMenuPanel.SetActive(false);
            nextButton.onClick.AddListener(DisplayNextLine);
        }

        void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void ShowDialogueChoices(Dialogue[] dialogues)
        {
            ClearChoiceButtons();
            choiceMenuPanel.SetActive(true);
            dialogueBox.SetActive(false);

            foreach (Dialogue dialogue in dialogues)
            {
                GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
                spawnedChoiceButtons.Add(buttonObj);

                Button button = buttonObj.GetComponent<Button>();
                TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                    buttonText.text = dialogue.dialoguePrompt;

                button.onClick.AddListener(() => OnChoiceSelected(dialogue));
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentLineIndex = 0;
            dialogueBox.SetActive(true);
            choiceMenuPanel.SetActive(false);
            
            characterNameText.text = dialogue.characterName;
            
            GameLogger.Instance.LogGameEvent("DialogueStarted", $"DialogueID: {dialogue.dialogueID}, Character: {dialogue.characterName}");
            
            DisplayCurrentLine();
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

        void DisplayCurrentLine()
        {
            DialogueLine line = currentDialogue.dialogueLines[currentLineIndex];
            dialogueText.text = line.text;
            
            if (line.audioClip != null && audioSource != null)
            {
                audioSource.clip = line.audioClip;
                audioSource.Play();
            }
            
            bool isLastLine = currentLineIndex >= currentDialogue.dialogueLines.Length - 1;
            nextButton.gameObject.SetActive(true);
                
            TextMeshProUGUI buttonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
                buttonText.text = isLastLine ? "Close" : "Next";
        }

        void OnChoiceSelected(Dialogue dialogue)
        {
            ClearChoiceButtons();
            StartDialogue(dialogue);
        }

        void ClearChoiceButtons()
        {
            foreach (GameObject button in spawnedChoiceButtons)
                Destroy(button);
            
            spawnedChoiceButtons.Clear();
        }

        void EndDialogue()
        {
            if (currentDialogue != null && !string.IsNullOrEmpty(currentDialogue.dialogueID))
            {
                if (GameStateManager.Instance != null)
                    GameStateManager.Instance.MarkDialogueComplete(currentDialogue.dialogueID);
                
                GameLogger.Instance.LogGameEvent("DialogueCompleted", $"DialogueID: {currentDialogue.dialogueID}");
            }

            dialogueBox.SetActive(false);
            currentDialogue = null;
            currentLineIndex = 0;
            
            if (audioSource != null)
                audioSource.Stop();
        }
    }
}