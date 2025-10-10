using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float runningSpeed;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform orientation;
    private Vector3 velocity;

    [Header("Ground")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded = true;

    [Header("Private")]
    private Vector3 moveDirection;
    private CharacterController characterController;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    bool startJumping = false;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindActionMap("Player").FindAction("Move");
        jumpAction = playerInput.actions.FindActionMap("Player").FindAction("Jump");
        characterController = GetComponent<CharacterController>();
        jumpAction.started += Jump;
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        // if moveInput == Vector2.zero -> with this check I can completly stop all movement of the player!
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext callbackContext)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
