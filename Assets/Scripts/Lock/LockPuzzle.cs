using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class LockPuzzle : MonoBehaviour, IPuzzleInputReceiver
{
    [SerializeField] private DialController cylinder01;
    [SerializeField] private DialController cylinder02;
    [SerializeField] private DialController cylinder03;

    private DialController[] cylinders;
    private int selectedIndex = 0;
    private bool active;
    [SerializeField] private string correctCode = "111";
    [SerializeField] private CinemachineCamera lockCam;
    [SerializeField] Transform safeDoor;
    [SerializeField] private GameObject interactableObject;
    [SerializeField] private GameObject puzzleObject;




    private void Awake()
    {
        cylinders = new[] { cylinder01, cylinder02, cylinder03 };
    }

    public void Activate()
    {
        active = true;
        
        // Switch cameras
        lockCam.Priority = 20;

        // Disable the whole interactable object
        interactableObject.SetActive(false);

        puzzleObject.SetActive(true);
        PuzzleManager.Instance.StartPuzzle(this);
        Debug.Log("LockPuzzle.Activate() was called!");

    }

    public void Deactivate()
    {
        active = false;
        // Re-enable the interactable object
        interactableObject.SetActive(true);
        puzzleObject.SetActive(false);
        PuzzleManager.Instance.EndPuzzle();
        // Switch back
        lockCam.Priority = 1;
        // Re-enable collider so player can interact again
       
    }

    public void OnRotateInput(float input)
    {
        if (!active) return;

        if (input > 0.5f) cylinders[selectedIndex].RotateUp();
        else if (input < -0.5f) cylinders[selectedIndex].RotateDown();

        CheckCode();
    }

    public void OnMoveInput(float input)
    {
        if (!active) return;

        if (input > 0.5f) selectedIndex = (selectedIndex + 1) % 3;
        else if (input < -0.5f) selectedIndex = (selectedIndex - 1 + 3) % 3;
    }

    public void OnSubmitInput() { }
    public void OnCancelInput() => Deactivate();

    private void CheckCode()
    {
        string code =
            cylinder01.CurrentStep.ToString() +
            cylinder02.CurrentStep.ToString() +
            cylinder03.CurrentStep.ToString();

        if (code == "111")
        {
            Debug.Log("Lock puzzle solved!");
            StartCoroutine(OpenSafe());
            //safeAnimator.SetTrigger("Open");
            Deactivate();
        }
    }

    IEnumerator OpenSafe()
    {
        Quaternion start = safeDoor.localRotation;
        Quaternion end = Quaternion.Euler(0, 90, 0);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            safeDoor.localRotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }
    }

    private void PuzzleCompleted()
    {
        InteractionUI.Instance.ShowPuzzleSolved();

        // Optional: delay before closing puzzle
        StartCoroutine(CloseAfterDelay(1.5f));
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        InteractionUI.Instance.HidePuzzleSolved();
        Deactivate();
    }

}