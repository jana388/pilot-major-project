using UnityEngine;

public class CreditsMenuButtonsManager : MonoBehaviour
{
    [SerializeField] MainMenuManager.CreditsButtons _buttonType;

    public void ButtonClicked()
    {
        MainMenuManager._.CreditsButtonClicked ( _buttonType);
    }    

}
