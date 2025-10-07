using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 5)] 
        public string text;
        public AudioClip audioClip;
    }

    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public string characterName;
        public DialogueLine[] dialogueLines;
    }
}
