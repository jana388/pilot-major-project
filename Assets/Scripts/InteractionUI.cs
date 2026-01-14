using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject puzzleSolvedPanel;
    [SerializeField] private GameObject puzzleBackButton;
    [SerializeField] private TextMeshProUGUI backButtonText;
    [SerializeField] private GameObject puzzleControlsPanel;
    [SerializeField] private TextMeshProUGUI puzzleControlsText;
    [SerializeField] private GameObject lockControlsPanel;
    [SerializeField] private TextMeshProUGUI lockControlsText;

    public void ShowPuzzleControls(string keyboardText, string gamepadText, bool usingGamepad)
    {
        puzzleControlsPanel.SetActive(true);

        if (usingGamepad)
            puzzleControlsText.text = gamepadText;
        else
            puzzleControlsText.text = keyboardText;
    }

    public void HidePuzzleControls()
    {
        puzzleControlsPanel.SetActive(false);
    }


    public void ShowLockControls(string keyboardText, string gamepadText, bool usingGamepad)
    {
        lockControlsPanel.SetActive(true);
        lockControlsText.text = usingGamepad ? gamepadText : keyboardText;
    }

    public void HideLockControls()
    {
        lockControlsPanel.SetActive(false);
    }

    public void ShowBackButton(bool usingGamepad)
    {
        puzzleBackButton.SetActive(true);
        backButtonText.text = usingGamepad ? "Press o to escape" : "Press ENTER to escape";
    }

    public void HideBackButton()
    {
        puzzleBackButton.SetActive(false);
    }


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

    

}

