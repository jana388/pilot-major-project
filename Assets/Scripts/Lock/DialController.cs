using UnityEngine;

public class DialController : MonoBehaviour
{
    public int CurrentStep { get; private set; } = 0;

    private const float StepAngle = 36f; // 360° / 10 numbers

    public void RotateUp()
    {
        CurrentStep = (CurrentStep + 1) % 10;
        ApplyRotation();
    }

    public void RotateDown()
    {
        CurrentStep = (CurrentStep - 1 + 10) % 10;
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(CurrentStep * StepAngle, 0f, 0f);
    }
}