using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerMovement : MonoBehaviour
{
    // Ground Movement
    private PlayerMovement movement;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    private float moveHorizontal;
    [SerializeField] private float playerSpeed = 2.0f;
    private float moveForward;
    private bool groundedPlayer;
    bool canpickup;
    GameObject ObjectIwantToPickUp;
    bool hasItem;
    
    public LayerMask groundLayer;
    private bool isGrounded;

    void Start()
    {
        movement = gameObject.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canpickup = false;
        hasItem = false;
       
    }

    void Update()
    {
        groundedPlayer = movement.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");

        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * verticalAxis + right * horizontalAxis;
        movement.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.position = move;
        }


    
        if (canpickup == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces

            }
        }

    }

    private void Move(Vector3 vector3)
    {
        throw new NotImplementedException();
    }

    public void SetHorizontalMovement(float horizontal)
    {
        moveHorizontal = horizontal;
    }

    public void SetVerticalMovement(float vertical)
    {
        moveForward = vertical;
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
        canpickup = false;

    }

  
    
}
