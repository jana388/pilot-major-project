using System.Collections;
using UnityEngine;

public class Buttons : InteractableObject
{
    [SerializeField] private int buttonNumber;
[SerializeField] private LiftPuzzleManager puzzleManager;

    public override void Interacted()
    {
    puzzleManager.AddInput(buttonNumber);
    }
}




