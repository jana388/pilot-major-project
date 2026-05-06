using UnityEngine;
using UnityEngine.SceneManagement;


public class BackToMainMenu : MonoBehaviour
{
    public void ClickToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
