using System.Collections;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private IPuzzleInputReceiver activePuzzle;
    [SerializeField] private GameContext context;
    [SerializeField] private InteractionUI interactionUI;

    public void StartPuzzle(IPuzzleInputReceiver puzzle)
    {
        Debug.Log("[PuzzleManager] StartPuzzle called with: " + puzzle);

        activePuzzle = puzzle;

        // Show correct control prompts depending on puzzle type
        bool usingGamepad = context.playerController.UsingGamepad;
        //for letter puzzle
        if (puzzle is LetterPuzzle)
        {
            interactionUI.ShowPuzzleControls(
                "Press Space to rotate",
                "Press X to rotate",
                usingGamepad
            );
        }
        //for lock puzzle
        else if (puzzle is LockPuzzle)
        {
            interactionUI.ShowLockControls(
       "W/S to rotate A/D to move",
       "Up/Down to rotate Left/Right to move",
       usingGamepad
        );
        }

        // Switch player input to puzzle mode
        context.playerController.ActivateInputState(PlayerController.InputState.Puzzle);
    }

    // Back button for all puzzles
    //interactionUI.ShowBackButton(usingGamepad);


    public void StartPuzzleFromObject(Object puzzleObject)
    {
        GameObject go = puzzleObject as GameObject;
        IPuzzleInputReceiver puzzle = go.GetComponent<IPuzzleInputReceiver>();

        if (puzzle == null)
        {
            Debug.LogError("[PuzzleManager] Assigned object does not implement IPuzzleInputReceiver!");
            return;
        }

        StartPuzzle(puzzle);
    }

    public void EndPuzzle()
    {
        activePuzzle = null;

        // Hide puzzle control prompts
        interactionUI.HidePuzzleControls();
        interactionUI.HideLockControls();
        interactionUI.HideBackButton();


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

    public void ShowPuzzleSolved()
    {
        StartCoroutine(PuzzleSolvedRoutine());
    }

    private IEnumerator PuzzleSolvedRoutine()
    {
        // Small pause so the player sees the final action
        yield return new WaitForSeconds(0.4f);

        interactionUI.ShowPuzzleSolved();

        // Keep the panel visible briefly
        yield return new WaitForSeconds(1.2f);

        interactionUI.HidePuzzleSolved();

        EndPuzzle();
    }
}