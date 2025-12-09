using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager _;
    [SerializeField] private bool _debugMode;

    public enum MainMenuButtonTypes { play, settings, credits, exit };
    public enum SocialButtonTypes { website, twitter };
    public enum CreditsButtonTypes { back };
    public enum SettingsButtonTypes { back };


    [SerializeField] GameObject _MainMenuContainer;
    [SerializeField] GameObject _CreditsMenuContainer;
    [SerializeField] GameObject _SettingsMenuContainer;
    [SerializeField] private string _sceneToLoadAfterClickingPlay;

    [Header("Navigation")]
    [SerializeField] GameObject _startingButton;
    [Space]
    [SerializeField] GameObject[] _MainMenuButtons = new GameObject[4];
    [SerializeField] GameObject[] _SocialButtons = new GameObject[2];
    [SerializeField] GameObject[] _CreditsButtons = new GameObject[1];
    [SerializeField] GameObject[] _SettingsButtons = new GameObject[1];


    public void Awake()
    {
        if (_ == null)
        {
            _ = this;
        }

        else
        {
            Debug.LogError("There are more than 1 MainManagers in the scene");
        }
    }

    private void Start()
    {
        OpenMenu(_MainMenuContainer, _startingButton);
    }
    public void MainMenuButtonClicked(MainMenuButtonTypes buttonClicked)
    {
        DebugMessage("Button Clicked: " + buttonClicked.ToString());
        switch (buttonClicked)
        {
            case MainMenuButtonTypes.play:
                PlayClicked();
                break;
            case MainMenuButtonTypes.settings:
                SettingsClicked();
                break;
            case MainMenuButtonTypes.credits:
                CreditsClicked();
                break;
            case MainMenuButtonTypes.exit:
                QuitGame();
                break;
            default:
                Debug.Log("Button click that wasn't implemented in MainMenuManager Method");
                break;

        }
    }
    public void SocialButtonClicked(SocialButtonTypes buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case SocialButtonTypes.website:
                websiteLink = "https://janaradulaski.wixsite.com/myportfolio";
                break;
            case SocialButtonTypes.twitter:
                websiteLink = "https://x.com/RadulaskiJana";
                break;
            default:
                Debug.LogError("not yet implemented");
                break;
        }
        if (websiteLink != "")
        {
            Application.OpenURL(websiteLink);
        }
    }

    public void CreditsClicked()
    {
        OpenMenu(_CreditsMenuContainer, _CreditsButtons[(int)CreditsButtonTypes.back]);
    }

    public void SettingsClicked()
    {
        OpenMenu(_SettingsMenuContainer, _SettingsButtons[(int)SettingsButtonTypes.back]);
    }

    public void ReturnToMainMenu()
    {
        OpenMenu(_MainMenuContainer, _MainMenuButtons[(int)MainMenuButtonTypes.play]);
    }

    public void CreditsButtonClicked(CreditsButtonTypes buttonClicked)
    {
        switch (buttonClicked)
        {
            case CreditsButtonTypes.back:
                ReturnToMainMenu();
                break;

        }
    }

    public void SettingsButtonClicked(SettingsButtonTypes buttonClicked)
    {
        switch (buttonClicked)
        {
            case SettingsButtonTypes.back:
                ReturnToMainMenu();
                break;

        }
    }

    private void DebugMessage(string message)
    {
        if (_debugMode)
        {
            Debug.Log(message);
        }
    }

    public void PlayClicked()
    {
        SceneManager.LoadScene(_sceneToLoadAfterClickingPlay);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }

    public void OpenMenu(GameObject menuToOpen, GameObject enterButton = null)
    {
        _MainMenuContainer.SetActive(menuToOpen == _MainMenuContainer);
        _CreditsMenuContainer.SetActive(menuToOpen == _CreditsMenuContainer);
        _SettingsMenuContainer.SetActive(menuToOpen == _SettingsMenuContainer);

        if(enterButton != null)
            EventSystem.current.SetSelectedGameObject(enterButton);
    }
}
