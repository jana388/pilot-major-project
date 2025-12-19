using UnityEngine;

public class Shackle : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float yMovement;
    [SerializeField] private float yMovementDuration;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float rotationDuration;

    public void Open()
    {
        //LeanTween.moveLocalY(gameObject, yMovement, yMovementDuration).setEase(LeanTweenType.easeOutBack).setOnComplete(
        //()=> LeanTween.rotateAroundLocal(gameObject, Vector3.up, rotationAngle, rotationDuration).setEase(LeanTweenType.easeOutBack));
    }
}
