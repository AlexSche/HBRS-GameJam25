using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float runningSpeed;
    public Transform orientation;
    [Header("Ground")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded = true;

    [Header("Private")]
    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindActionMap("Player").FindAction("Move");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded)
        {
            Debug.Log("Grounded");
        }
        MovePlayer();
        SpeedControl();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
            moveDirection.y = 0;
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void SpeedControl()
    {
        if (rb.maxLinearVelocity > movementSpeed)
        {
            Vector3 limitedVelocity = rb.linearVelocity.normalized * movementSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }
}
