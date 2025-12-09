using UnityEngine;

public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] private MainMenuManager.MainMenuButtonTypes _buttonType;

    public void ButtonClicked()
    {
        MainMenuManager._.MainMenuButtonClicked(_buttonType); 
    }
}
