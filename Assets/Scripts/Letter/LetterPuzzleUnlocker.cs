using UnityEngine;

public class LetterPuzzleUnlocker : MonoBehaviour
{
    [SerializeField] private int piecesRequired = 4;

    private int piecesCollected = 4;

    public void RegisterPieceCollected()
    {
        piecesCollected++;

        if (piecesCollected >= piecesRequired)
        {
            Debug.Log("Letter puzzle unlocked");
        }
    }
}