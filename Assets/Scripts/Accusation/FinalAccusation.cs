using TMPro;
using UnityEngine;

public class FinalAccusation : MonoBehaviour
{
    public GameObject accusationBoard;
    public GameObject resultsPanel;
    public TextMeshProUGUI finalAccusationText;
    public GameObject retryButton;
    public GameObject returnButton;
    
    public void SubmitAccusation(string target)
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        
        if (target == "Dr. Purple")
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.MarkCorrectAccusation();
            }
            
            finalAccusationText.text = "Good job detective. You have found out what the scientist has done for his fame. Go confront the doctor.";
            returnButton.SetActive(true);
            resultsPanel.SetActive(true);
        } 
        else if (target == "Lord DuPont") 
        {
            finalAccusationText.text = "You have failed detective. The kind lord of this manor was in fact MURDERED...";
            retryButton.SetActive(true);
            resultsPanel.SetActive(true);
            Destroy(clickedButton);
        } 
        else 
        {
            finalAccusationText.text = "You have failed detective. " + target + " was innocent by all accounts! You now have no choice but to...";
            retryButton.SetActive(true);
            resultsPanel.SetActive(true);
            Destroy(clickedButton);
        }
    }
    
    public void Return()
    {
        accusationBoard.SetActive(false);
        resultsPanel.SetActive(false);
    }
    
    public void Retry()
    {
        retryButton.SetActive(false);
        resultsPanel.SetActive(false);
    }
}