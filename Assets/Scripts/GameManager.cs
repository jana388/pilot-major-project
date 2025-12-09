using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public bool IsGamePaused { get; private set; }

    public void SetPauseState(bool pauseState)
    {
        IsGamePaused = pauseState;
    }
}
