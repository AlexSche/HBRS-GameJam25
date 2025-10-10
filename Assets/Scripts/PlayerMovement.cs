using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Vector2 moveInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindActionMap("Player").FindAction("Move");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
            Debug.Log("Orient Forward :" + orientation.forward);
            Debug.Log("Orient Right :" + orientation.right);
            Debug.Log("Move direction :" + moveDirection);
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
            Debug.Log("Move");
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
