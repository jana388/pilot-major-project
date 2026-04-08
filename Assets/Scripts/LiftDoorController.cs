using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class LiftDoorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineCamera insideCamera;
    [SerializeField] private CinemachineCamera destinationCamera;
    [SerializeField] private Collider collisionCollider;

    [Header("State")]
    private bool isOpen = false;
    private bool isBusy = false; // prevents spamming the button

    // Called by InteractableObject
    public void ToggleDoor()
    {
        if (isBusy) return;

        isBusy = true;
        isOpen = !isOpen;

        // Disable collider when door opens
        collisionCollider.enabled = !isOpen;

        animator.SetBool("Open", isOpen);
        animator.SetTrigger("Toggle");

        StartCoroutine(DoorRoutine());
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
        animator.SetTrigger("LiftMove");

        // Wait for lift animation
        yield return new WaitForSeconds(2f);

        // Switch camera to destination
        CameraSwitcher.SwitchCamera(destinationCamera);
    }
}