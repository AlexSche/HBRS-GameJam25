using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction lookAction;
    [SerializeField] private float sensX = 1;
    [SerializeField] private float sensY = 1;
    public Transform orientation;
    float xRotation;
    float yRotation;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions.FindActionMap("Player").FindAction("Look");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        lookAction.Enable();
    }

    void OnDisable()
    {
        lookAction.Disable();
    }

    void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        float mouseX = lookInput.x * Time.deltaTime * sensX;
        float mouseY = lookInput.y * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        // negative value top - range look up / positive value down - range look down
        xRotation = Mathf.Clamp(xRotation, -50f, 60f);
        Camera.main.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
