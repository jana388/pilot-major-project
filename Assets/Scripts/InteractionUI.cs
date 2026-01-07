using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptText;

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
}

