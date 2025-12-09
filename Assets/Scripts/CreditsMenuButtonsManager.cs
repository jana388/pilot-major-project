using UnityEngine;

public class CreditsMenuButtonsManager : MonoBehaviour
{
    [SerializeField] MainMenuManager.CreditsButtonTypes _buttonType;

    public void ButtonClicked()
    {
        MainMenuManager._.CreditsButtonClicked ( _buttonType);
    }    

}
