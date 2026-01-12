using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private IPuzzleInputReceiver activePuzzle;
    [SerializeField] private GameContext context;

    public void StartPuzzle(IPuzzleInputReceiver puzzle)
    {
        Debug.Log("[PuzzleManager] StartPuzzle called with: " + puzzle);

        activePuzzle = puzzle;

        // Switch player input to puzzle mode
        context.playerController.ActivateInputState(PlayerController.InputState.Puzzle);
    }

    public void EndPuzzle()
    {
        activePuzzle = null;

        // Return player input to normal
        context.playerController.ActivateInputState(PlayerController.InputState.Player);
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