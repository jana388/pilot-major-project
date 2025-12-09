using UnityEngine;

public class SettingsMenuButtonManager : MonoBehaviour
{
    [SerializeField] MainMenuManager.SettingsButtonTypes _buttonType;

    public void ButtonClicked()
    {
        MainMenuManager._.SettingsButtonClicked(_buttonType);
    }

}
