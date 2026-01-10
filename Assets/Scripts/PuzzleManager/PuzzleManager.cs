using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    private IPuzzleInputReceiver activePuzzle { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartPuzzle(IPuzzleInputReceiver puzzle)
    {
        Debug.Log("[PuzzleManager] StartPuzzle called with: " + puzzle);
        activePuzzle = puzzle;
        InteractionUI.Instance.ShowBackButton(PlayerController.Instance.UsingGamepad);
        PlayerController.ActivateInputState(PlayerController.InputState.Puzzle);
    }

    public void EndPuzzle()
    {
        activePuzzle = null;
        InteractionUI.Instance.HideBackButton();
        PlayerController.ActivateInputState(PlayerController.InputState.Player);
    }

    public void HandleRotate(float value)
    {
        activePuzzle?.OnRotateInput(value);
    }

    public void HandleMove(float value)
    {
        activePuzzle?.OnMoveInput(value);
    }

    public void HandleSubmit()
    {
        activePuzzle?.OnSubmitInput();
    }

    public void HandleCancel()
    {
        activePuzzle?.OnCancelInput();
    }
}
