using UnityEngine;

public class SocialButtonsManager : MonoBehaviour
{
    [SerializeField] private MainMenuManager.SocialButtons _buttonType;

    public void ButtonClicked()
    {
        MainMenuManager._.SocialButtonClicked(_buttonType);
    }
}
