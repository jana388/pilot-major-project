using UnityEngine;

public class LetterPuzzle : MonoBehaviour, IPuzzleInputReceiver
    {
        public LetterChecker puzzlePiece;
    [SerializeField] private PuzzleManager puzzleManager;

    public void Begin()
    {
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        Debug.Log("LetterPuzzle Awake on: " + gameObject.name);
    }

    public void OnSubmitInput()
        {
          bool solved = SolvePuzzle();
          if (solved)
          {
            puzzleManager.ShowPuzzleSolved(); // handles timing + EndPuzzle()
            End();  // hides the puzzle UI
          }
        }

        public void OnRotateInput(float input) { }
        public void OnMoveInput(float input) { }
        public void OnCancelInput() { }

        public bool SolvePuzzle()
        {
            puzzlePiece = puzzlePiece.SolvePuzzle();
            return puzzlePiece == null;
        }

       

        public void End()
        {
            gameObject.SetActive(false);
        }
    }