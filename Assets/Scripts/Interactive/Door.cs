using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Collider collisionCollider;

    [SerializeField] bool _isBeingOpened;
    [SerializeField] bool _isOpen;

    [Header("Door Stats")]
    [SerializeField] float _doorOpenDuration = 1.5f;
    [SerializeField] float _doorCloseDuration = 1f;

    public void ToggleDoor(bool open)
    {
        if (_isBeingOpened) return;

        _isOpen = open;
        collisionCollider.enabled = !open;
        animator.SetBool("IsOpen", open); // TODO: Setup a door opening animation for any door objects
        animator.SetTrigger("Trigger");

        StartCoroutine(DoorMovingProcess());
    }

    IEnumerator DoorMovingProcess()
    {
        float duration = _isOpen ? _doorOpenDuration : _doorCloseDuration;
        animator.speed = duration;

        yield return new WaitForSeconds(duration);

        _isBeingOpened = false;
    }
}
