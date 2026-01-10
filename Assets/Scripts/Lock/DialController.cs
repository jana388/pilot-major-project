using UnityEngine;

public class DialController : MonoBehaviour
{
    public int CurrentStep { get; private set; }

    public void RotateUp()
    {
        CurrentStep = (CurrentStep + 1) % 10;
        transform.localRotation = Quaternion.Euler(CurrentStep * 36f, 0, 0);
    }

    public void RotateDown()
    {
        CurrentStep = (CurrentStep - 1 + 10) % 10;
        transform.localRotation = Quaternion.Euler(CurrentStep * 36f, 0, 0);
    }
}