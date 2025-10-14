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
}