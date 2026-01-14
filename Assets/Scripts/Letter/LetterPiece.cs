using UnityEngine;

public class LetterPiece : MonoBehaviour
{
    [SerializeField] private LetterPuzzleUnlocker unlocker;

    public void OnCollected()
    {
        Debug.Log("Collected!");

        unlocker.RegisterPieceCollected();
    }
}