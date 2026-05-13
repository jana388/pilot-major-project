using Unity.VisualScripting;
using UnityEngine;

public class MorleyAppears : MonoBehaviour
{
    public Animator morleyAnimator;

    public bool MainMenu;

    GameObject Morley;

    public static class GameState
    {
        public static bool MainMenu = false;
    }

    private void Start()
    {
        GameState.MainMenu = true;

        {
            if (!GameState.MainMenu)
            {
                morleyAnimator.Play("MorleySmallState");
            }

        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            morleyAnimator.SetTrigger("Appears");
        }
    }

}