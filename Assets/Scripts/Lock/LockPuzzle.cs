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

    [SerializeField] private string correctCode;
    [SerializeField] private CinemachineCamera lockCam;
    [SerializeField] private Transform safeDoor;

    [SerializeField] private GameObject interactableObject;
    [SerializeField] private GameObject puzzleObject;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private GameContext context;

    private void Awake()
    {
        cylinders = new[] { cylinder01, cylinder02, cylinder03 };
    }

    public void Activate()
    {
        active = true;
        // Switch to puzzle camera
        lockCam.Priority = 20;

        // Hide interactable object and show puzzle UI
        interactableObject.SetActive(false);
        puzzleObject.SetActive(true);
        context.playerController.ActivateInputState(PlayerController.InputState.Puzzle);


        // Tell PuzzleManager we started a puzzle
        context.puzzleManager.StartPuzzle(this);

        //show back button
        context.interactionUI.ShowBackButton(context.playerController.UsingGamepad);


        Debug.Log("LockPuzzle.Activate() was called!");
    }

    public void Deactivate()
    {
        active = false;

        // Restore interactable object and hide puzzle UI
        interactableObject.SetActive(true);
        puzzleObject.SetActive(false);

        context.playerController.ActivateInputState(PlayerController.InputState.Player);


        // Tell PuzzleManager we ended the puzzle
        context.puzzleManager.EndPuzzle();

        // Switch camera back
        lockCam.Priority = 1;

        //hide back button
        context.interactionUI.HideBackButton();

        //also hide puzzle solved 
        context.interactionUI.HidePuzzleSolved();
    }

    public void OnRotateInput(float input)
    {
        if (!active) return;

        if (input > 0.5f)
            cylinders[selectedIndex].RotateUp();
        else if (input < -0.5f)
            cylinders[selectedIndex].RotateDown();

        CheckCode();
    }

    public void OnMoveInput(float input)
    {
        if (!active) return;

        if (input > 0.5f)
            selectedIndex = (selectedIndex + 1) % 3;
        else if (input < -0.5f)
            selectedIndex = (selectedIndex - 1 + 3) % 3;
    }

    public void OnSubmitInput() { }

    public void OnCancelInput()
    {
        Deactivate();
    }

    private void CheckCode()
    {
        string code =
            cylinder01.CurrentStep.ToString() +
            cylinder02.CurrentStep.ToString() +
            cylinder03.CurrentStep.ToString();
        Debug.Log("[PUZZLE] Checking code: " + code);
        Debug.Log("[PUZZLE] Correct code is: " + correctCode);


        if (code == correctCode)
        {
            Debug.Log("Lock puzzle solved!");
            //show puzzle completed UI
            context.interactionUI.ShowPuzzleSolved();
            StartCoroutine(HandlePuzzleSolved());
            StartCoroutine(OpenSafe());
            Deactivate();
        }
    }

    private IEnumerator HandlePuzzleSolved()
    {
        // Optional: play a sound or animation here

        context.interactionUI.ShowPuzzleSolved();

        yield return new WaitForSeconds(20f); // adjust the delay as you like

       

        // Optional: wait again before opening the safe
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(OpenSafe());
        Deactivate();
    }

    private IEnumerator OpenSafe()
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
}