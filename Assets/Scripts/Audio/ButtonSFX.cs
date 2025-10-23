using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    public AudioClip clickSound;
    
    [Range(0f, 1f)]
    public float volume = 1f;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            if (SFXManager.Instance != null && clickSound != null)
                SFXManager.Instance.PlaySound(clickSound, volume);
        });
    }
}