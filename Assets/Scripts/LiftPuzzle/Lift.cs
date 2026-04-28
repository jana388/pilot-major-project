using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] private CinemachineCamera insideCamera;
    [SerializeField] private CinemachineCamera destinationCamera;
    [SerializeField] Collider collisionCollider;
    [SerializeField] private GameContext context;
    [SerializeField] private LiftPuzzleManager liftPuzzle;


    [Header("State")]
    private bool isOpen = false;
    private bool isBusy = false; // prevents spamming the button

    [SerializeField] bool _isBeingOpened;

    // Called by InteractableObject
    public void ToggleDoor()
    {
        if (_isBeingOpened) return;

        isBusy = true;
        isOpen = !isOpen;

        // Disable collider when door opens
        collisionCollider.enabled = !isOpen;

        animator.SetBool("Open", isOpen);
        animator.SetTrigger("Toggle");

        StartCoroutine(DoorRoutine());
    }

    public void BeginLiftPuzzle()
    {
        context.puzzleManager.StartPuzzle(liftPuzzle);
    }
    private IEnumerator DoorRoutine()
    {
        // Wait for the door animation to finish
        yield return new WaitForSeconds(1.2f); // match your animation length

        isBusy = false;
    }

    // Called by puzzle manager when correct sequence is entered
    public void MoveLift()
    {
        StartCoroutine(LiftMovementRoutine());
    }

    private IEnumerator LiftMovementRoutine()
    {
        // Close doors if open
        if (isOpen)
        {
            ToggleDoor();
            yield return new WaitForSeconds(1.2f);
        }

        // Play lift movement animation
        animator.SetTrigger("RightDoorOpen");

        // Wait for lift animation
        yield return new WaitForSeconds(2f);

        // Switch camera to destination
        CameraSwitcher.SwitchCamera(destinationCamera);
    }
}
