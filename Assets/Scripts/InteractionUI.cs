using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject puzzleSolvedPanel;
    [SerializeField] private GameObject puzzleBackButton;

    public void ShowPrompt(string keyboardPrompt, string gamepadPrompt, bool usingGamepad)
    {
        promptUI.SetActive(true);

        if (usingGamepad)
            promptText.text = gamepadPrompt;
        else
            promptText.text = keyboardPrompt;
    }

    public void HidePrompt()
    {
        promptUI.SetActive(false);
    }

    public void ShowPuzzleSolved()
    {
        puzzleSolvedPanel.SetActive(true);
    }

    public void HidePuzzleSolved()
    {
        puzzleSolvedPanel.SetActive(false);
    }
    public void ShowBackButton(bool usingGamepad)
    {
        puzzleBackButton.SetActive(true);

        // Optional: swap icons based on device
        // keyboardIcon.SetActive(!usingGamepad);
        // gamepadIcon.SetActive(usingGamepad);
    }

    public void HideBackButton()
    {
        puzzleBackButton.SetActive(false);
    }

}

