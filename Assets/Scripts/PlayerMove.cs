// This first example shows how to move using Input System Package (New)

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Input Actions")]
    public InputActionReference moveAction; // expects Vector2
    public InputActionReference jumpAction; // expects Button

    bool canpickup;
    GameObject ObjectIwantToPickUp;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        //jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        //jumpAction.action.Disable();
    }

    void Update()
    {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
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
        controller.Move(finalMove * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            canpickup = false;  //set the pick up bool to true
            ObjectIwantToPickUp = null; //set the gameobject you collided with to one you can reference
        }
    }
}
