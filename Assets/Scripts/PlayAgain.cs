using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayAgain : MonoBehaviour
{
    public void ClickToPlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
