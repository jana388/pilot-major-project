using UnityEngine;
using UnityEngine.Events;

public class Dial : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float animationDuration;
    private bool isRotating = false;
    private int currentIndex;

    [Header("Events")]
    [SerializeField] private UnityEvent<Dial> onDialRotated;

    private void Start()
    {
        currentIndex = Random.Range(0, 10);
        transform.localRotation = Quaternion.Euler(currentIndex * -36, 0, 0 );
    }

    public void Rotate()
    {
        if (isRotating)
            return;

        isRotating = true;

        if (currentIndex >= 10)
            currentIndex = 0;

        //LeanTween.cancel(gameObject);
        //LeanTween.rotateAroundLocal(gameObject, Vector3.right, -36, animationDuration).setOnComplete(RotationCompleteCallback);
    }

    private void RotationCompleteCallback()
    {
        onDialRotated?.Invoke(this);
    }

    public int GetNumber()
    {
        return currentIndex;
    }

    public void Lock()
    {
        isRotating = true;
    }

}
