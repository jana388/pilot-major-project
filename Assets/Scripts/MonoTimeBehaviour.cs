using UnityEngine;

public abstract class MonoTimeBehaviour : MonoBehaviour
{
    /// <summary>
    /// Coroutine suspension supplied with the check for if the game is paused
    /// </summary>
    public WaitUntil PauseWait { get; } = new(() => !GameManager.Instance.IsGamePaused);
    void Update()
    {
        if(!GameManager.Instance.IsGamePaused) TimeUpdate();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGamePaused) FixedTimeUpdate();
    }

    /// <summary>
    /// Update function that will only trigger if the game is not paused
    /// </summary>
    public virtual void TimeUpdate() { }

    /// <summary>
    /// Fixed Update function that will only trigger if the game is not paused
    /// </summary>
    public virtual void FixedTimeUpdate() { }
}
