using UnityEngine;

public class LetterPuzzleUnlocker : MonoBehaviour
{
    [SerializeField] private int piecesRequired = 4;
    [SerializeField] private InteractableObject puzzleInteractable;

    private int piecesCollected = 0;

    public void RegisterPieceCollected()
    {
        piecesCollected++;

        if (piecesCollected >= piecesRequired)
        {
            puzzleInteractable.enabled = true;
            Debug.Log("Letter puzzle unlocked");
        }
    }
}