using UnityEngine;

public class SocialButtonTypesManager : MonoBehaviour
{
    [SerializeField] private MainMenuManager.SocialButtonTypes _buttonType;

    public void ButtonClicked()
    {
        MainMenuManager._.SocialButtonClicked(_buttonType);
    }
}
