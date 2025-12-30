using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoTimeBehaviour
{
    #region Input

    [SerializeField] InputActionAsset input;
    [Header("Movement Inputs")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference interactAction;
    [Header("Dialogue Inputs")]
    [SerializeField] InputActionReference nextDialogueAction;

    //private PlayerInput playerInput;
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
        Instance.inputState = state;
        Instance.player.Disable();
        Instance.ui.Disable();
        Instance.puzzle.Disable();
        Instance.dialogue.Disable();
        switch (state)
        {
            case InputState.Player:
                Instance.player.Enable();
                break;
            case InputState.UI:
                Instance.ui.Enable();
                break;
            case InputState.Dialogue:
                Instance.dialogue.Enable();
                break;
            case InputState.Puzzle:
                Instance.puzzle.Enable();
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
    [SerializeField] bool _canInteract = true;

    [Header("Components")]
    [SerializeField] CharacterController characterController;

    #endregion

    private void Awake()
    {
        //playerInput = GetComponent<PlayerInput>();

        Instance = this;
        player = input.FindActionMap("Player");
        ui = input.FindActionMap("UI");
        dialogue = input.FindActionMap("Dialogue");
        puzzle = input.FindActionMap("Puzzle");
        ActivateInputState(InputState.Player); // Start in movement state

    }

    public override void TimeUpdate()
    {
        Movement();
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

        // Jump
        //if (jumpAction.action.triggered && groundedPlayer)
        //{
        //    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        //}

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        characterController.Move(finalMove * Time.deltaTime);
    }

    void Interact()
    {
        if (_canInteract && interactAction.action.WasCompletedThisFrame())
        {
            InteractableObject detectedObject = DetectInteractables(transform.position, _detectRadius, _interactableMask);

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
        if (nextDialogueAction.action.WasCompletedThisFrame())
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }
}
