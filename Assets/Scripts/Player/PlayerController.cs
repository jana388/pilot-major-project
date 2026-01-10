using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoTimeBehaviour
{
    #region Input

    [SerializeField] InputActionAsset input;
    [Header("Movement Inputs")]
    [SerializeField] InputActionReference moveAction;
    [Header("Interaction Inputs")]
    [SerializeField] InputActionReference interactAction;
    [Header("Dialogue Inputs")]
    [SerializeField] InputActionReference nextDialogueAction;
    [Header("Puzzle Inputs")]
    [SerializeField]  InputActionReference puzzleRotateAction;
    [SerializeField]  InputActionReference puzzleMoveAction;
    [SerializeField]  InputActionReference puzzleSubmitAction;
    [SerializeField]  InputActionReference puzzleCancelAction;


    private InputActionMap player;
    private InputActionMap ui;
    private InputActionMap dialogue;
    private InputActionMap puzzle;

    public static PlayerController Instance;

    public enum InputState
    {
        Player,
        UI,
        Dialogue,
        Puzzle
    }

    public InputState inputState;

    public static void ActivateInputState(InputState state)
    {
        Debug.Log("[PlayerController] Switching input state to: " + state);
        Instance.inputState = state;
        Instance.player.Disable();
        Instance.ui.Disable();
        Instance.puzzle.Disable();
        Instance.dialogue.Disable();

        // Hide interaction prompt whenever we leave Player mode
        if (state != InputState.Player)
            Instance.interactionUI.HidePrompt();


        switch (state)
        {
            case InputState.Player:
                Instance.DisablePuzzleInput();
                Instance.player.Enable();
                break;
            case InputState.UI:
                Instance.DisablePuzzleInput();
                Instance.ui.Enable();
                break;
            case InputState.Dialogue:
                Instance.DisablePuzzleInput();
                Instance.dialogue.Enable();
                break;
            case InputState.Puzzle:
                Instance.puzzle.Enable();
                Instance.EnablePuzzleInput();
                break;
        }
    }

    #endregion

    #region Variables

    [Header("Stats")]
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] float gravityValue = -9.81f;

    private Vector3 playerVelocity;

    [Space]

    [SerializeField] float _detectRadius = 4f;
    [SerializeField] LayerMask _interactableMask;

    [Header("Items")]
    [SerializeField] List<string> heldItems = new();

    [Header("Checks")]
    [SerializeField] bool _isGrounded;
    [SerializeField] bool _canInteract;

    [Header("Components")]
    [SerializeField] CharacterController characterController;

    [SerializeField] private InteractionUI interactionUI;
    private InteractableObject currentInteractable;
    //private string currentControlScheme;
    private bool usingGamepad;


    #endregion

    private void Awake()
    {
        Instance = this;

        player = input.FindActionMap("Player");
        ui = input.FindActionMap("UI");
        dialogue = input.FindActionMap("Dialogue");
        puzzle = input.FindActionMap("Puzzle");

        ActivateInputState(InputState.Player);

        //detect for control scheme changes
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            // We cannot use currentControlScheme not successful so will use the gamepad boolean
            // So we detect manually
            if (playerInput != null)
            {
                // Initial detection
                usingGamepad = Gamepad.current != null;

                // Listen for changes
                playerInput.onControlsChanged += OnControlsChanged;
            }
            else
            {
                Debug.LogWarning("PlayerInput component missing! Control scheme detection disabled.");
                usingGamepad = false; // default to keyboard
            }

           
        }
    }

    private void OnControlsChanged(PlayerInput input)
    {
        // If the gamepad was updated this frame, it is connected to the controllers scheme
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
            usingGamepad = true;
        else
            usingGamepad = false;

        Debug.Log("Using gamepad: " + usingGamepad);
        RefreshPrompt();

    }




    public override void TimeUpdate()
    {
        Movement();
        HandleInteractionPrompt();
        // DEVICE SWITCHING CHECKS
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame && !usingGamepad)
        {
            usingGamepad = true;
            RefreshPrompt();
        }

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame && usingGamepad)
        {
            usingGamepad = false;
            RefreshPrompt();
        }

        Interact();
    }

    void Movement()
    {
        _isGrounded = characterController.isGrounded;
        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        // Vector3 move = new Vector3(input.x, 0, input.y);
        Vector3 move = right * input.x + forward * input.y;
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        characterController.Move(finalMove * Time.deltaTime);
    }

    void HandleInteractionPrompt()
    {
        if (inputState != InputState.Player)
        {
            interactionUI.HidePrompt();
            return;
        }

        InteractableObject detectedObject = DetectInteractables(transform.position, _detectRadius, _interactableMask);

        if (detectedObject != null)
        {
            if (currentInteractable != detectedObject)
            {
                currentInteractable = detectedObject;

                interactionUI.ShowPrompt(
                    detectedObject.KeyboardPrompt,
                    detectedObject.GamepadPrompt,
                    usingGamepad
                );
            }
        }
        else
        {
            currentInteractable = null;
            interactionUI.HidePrompt();
        }
    }

    private void RefreshPrompt()
    {
        if (currentInteractable != null)
        {
            interactionUI.ShowPrompt(
                currentInteractable.KeyboardPrompt,
                currentInteractable.GamepadPrompt,
                usingGamepad
            );
        }
    }


    void Interact()
    { 
        if (_canInteract && interactAction.action.WasCompletedThisFrame())
        {
            
            InteractableObject detectedObject = currentInteractable;

            if (_canInteract && detectedObject != null)
                {
                    if (detectedObject.gameObject.CompareTag("Item"))
                    {
                        InteractableObject_Item item = detectedObject as InteractableObject_Item;

                        heldItems.Add(item.ItemName);
                    }
                    switch (detectedObject.CheckItemRequirement())
                    {
                        case ItemRequirement.NoItem:
                            detectedObject.Interacted();
                            break;
                        case ItemRequirement.ItemRequired:
                            detectedObject.Interacted(heldItems.ToArray());
                            break;
                    }
                }
            
        }
        if (inputState == InputState.Dialogue &&
    nextDialogueAction.action.WasCompletedThisFrame())
        {
            DialogueManager.Instance.DisplayNextDialogueLine();
        }

    }

    InteractableObject DetectInteractables(Vector3 checkPosition, float radius, LayerMask mask)
    {
        Collider[] hits = Physics.OverlapSphere(checkPosition, radius, mask);

        if (hits.Length > 0)
        {
            float distance = Mathf.Infinity;
            Collider closestItem = null;

            foreach (var item in hits)
            {
                float d = Vector3.Distance(item.transform.position, checkPosition);
                if (d < distance) // update if this hit item is closer
                {
                    closestItem = item;
                    distance = d;
                }
            }

            if (closestItem != null)
            {
                //Debug.Log($"Hit {closestItem.name} at {checkPosition}");
                return closestItem.GetComponent<InteractableObject>();
            }
                    
            return null;
        }
        else
        {
            //Debug.Log($"No hit at: {checkPosition}");
            return null;
           
        }
    }

    private void EnablePuzzleInput()
    {
        Debug.Log("[PlayerController] EnablePuzzleInput");

        if (puzzleMoveAction == null) Debug.LogError("puzzleMoveAction is NULL");
        if (puzzleRotateAction == null) Debug.LogError("puzzleRotateAction is NULL");
        if (puzzleSubmitAction == null) Debug.LogError("puzzleSubmitAction is NULL");
        if (puzzleCancelAction == null) Debug.LogError("puzzleCancelAction is NULL");

        puzzleRotateAction.action.performed += OnPuzzleRotate;
        puzzleMoveAction.action.performed += OnPuzzleMove;
        puzzleSubmitAction.action.performed += OnPuzzleSubmit;
        puzzleCancelAction.action.performed += OnPuzzleCancel;


        puzzleRotateAction.action.Enable();
        puzzleMoveAction.action.Enable();
        puzzleSubmitAction.action.Enable();
        puzzleCancelAction.action.Enable();
    }

    private void OnPuzzleRotate(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        Debug.Log("[PuzzleInput] Rotate value: " + value);
        PuzzleManager.Instance.HandleRotate(value);
    }

    private void OnPuzzleMove(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        Debug.Log("[PuzzleInput] Move value: " + value);
        PuzzleManager.Instance.HandleMove(value);
    }

    private void OnPuzzleSubmit(InputAction.CallbackContext ctx)
    {
        Debug.Log("[PuzzleInput] Submit");
        PuzzleManager.Instance.HandleSubmit();
    }

    private void OnPuzzleCancel(InputAction.CallbackContext ctx)
    {
        Debug.Log("[PuzzleInput] Cancel");
        PuzzleManager.Instance.HandleCancel();
    }

    private void DisablePuzzleInput()
    {
        Debug.Log("[PlayerController] DisablePuzzleInput");

        puzzleRotateAction.action.performed -= OnPuzzleRotate;
        puzzleMoveAction.action.performed -= OnPuzzleMove;
        puzzleSubmitAction.action.performed -= OnPuzzleSubmit;
        puzzleCancelAction.action.performed -= OnPuzzleCancel;


        puzzleRotateAction.action.Disable();
        puzzleMoveAction.action.Disable();
        puzzleSubmitAction.action.Disable();
        puzzleCancelAction.action.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }
}
