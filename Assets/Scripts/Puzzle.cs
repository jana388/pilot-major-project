using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public LetterChecker puzzlePiece;

    public bool SolvePuzzle()
    {
        puzzlePiece = puzzlePiece.SolvePuzzle();
        return !puzzlePiece;
    }

    public void Begin()
    {
        gameObject.SetActive(true);
    }

    public void End()
    {
        gameObject.SetActive(false);
    }
}
