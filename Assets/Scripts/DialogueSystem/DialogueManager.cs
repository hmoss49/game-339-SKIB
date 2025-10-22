using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

        [Header("Choice Button Colors")]
        public Color unplayedButtonColor = Color.white;
        public Color playedButtonColor = new Color(0.5f, 0.5f, 0.5f, 1f); // Dark grey

        private Dialogue currentDialogue;
        private int currentLineIndex;
        private List<GameObject> spawnedChoiceButtons = new List<GameObject>();
        private GameObject currentSpeakingCharacter;
        private Button currentSpeakingCharacterButton;

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

        public void ShowDialogueChoices(Dialogue[] dialogues, GameObject characterObject = null)
        {
            // Store character reference and keep it selected
            if (characterObject != null)
            {
                currentSpeakingCharacter = characterObject;
                currentSpeakingCharacterButton = characterObject.GetComponent<Button>();
                
                // Keep the button selected and disabled while showing choices
                if (currentSpeakingCharacterButton != null)
                {
                    currentSpeakingCharacterButton.interactable = false;
                }
            }
            
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

                // Check if dialogue has been completed and change colors
                bool isCompleted = GameStateManager.Instance != null && 
                                   !string.IsNullOrEmpty(dialogue.dialogueID) && 
                                   GameStateManager.Instance.HasCompletedDialogue(dialogue.dialogueID);

                ColorBlock colors = button.colors;
                Color baseColor = isCompleted ? playedButtonColor : unplayedButtonColor;
                
                colors.normalColor = baseColor;
                colors.highlightedColor = baseColor * 1.1f; // Slightly lighter when hovering
                colors.pressedColor = baseColor * 0.9f; // Slightly darker when pressed
                colors.selectedColor = baseColor;
                
                button.colors = colors;

                button.onClick.AddListener(() => OnChoiceSelected(dialogue));
            }
        }

        public void StartDialogue(Dialogue dialogue, GameObject characterObject = null)
        {
            currentDialogue = dialogue;
            currentLineIndex = 0;
            
            // Only set character reference if not already set (from choice menu)
            if (currentSpeakingCharacter == null && characterObject != null)
            {
                currentSpeakingCharacter = characterObject;
                currentSpeakingCharacterButton = characterObject.GetComponent<Button>();
            }
            
            // Disable the character button while dialogue is playing
            if (currentSpeakingCharacterButton != null)
            {
                currentSpeakingCharacterButton.interactable = false;
            }
            
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
            StartDialogue(dialogue); // Don't pass character - it's already stored
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

            // Re-enable and deselect the character button
            if (currentSpeakingCharacterButton != null)
            {
                currentSpeakingCharacterButton.interactable = true;
                
                if (EventSystem.current != null)
                    EventSystem.current.SetSelectedGameObject(null);
                
                currentSpeakingCharacterButton = null;
            }
            
            currentSpeakingCharacter = null;

            dialogueBox.SetActive(false);
            currentDialogue = null;
            currentLineIndex = 0;
            
            if (audioSource != null)
                audioSource.Stop();
        }
    }
}