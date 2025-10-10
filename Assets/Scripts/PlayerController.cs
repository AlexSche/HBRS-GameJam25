using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction flashlightAction;
    private bool isFlashlightActive = true;
    [SerializeField] private GameObject flashlightGameObject;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        flashlightAction = playerInput.actions.FindActionMap("Player").FindAction("Flashlight");
        flashlightAction.started += SwitchFlashlight;
    }
    void OnEnable()
    {
        flashlightAction.Enable();
    }

    void OnDisable()
    {
        flashlightAction.Disable();
    }

    void SwitchFlashlight(InputAction.CallbackContext callbackContext)
    {
        isFlashlightActive = !isFlashlightActive;
        flashlightGameObject.SetActive(isFlashlightActive);
    }
}
