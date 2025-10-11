using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction flashlightAction;
    private InputAction interactAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        flashlightAction = playerInput.actions.FindActionMap("Player").FindAction("Flashlight");
        interactAction = playerInput.actions.FindActionMap("Player").FindAction("Interact");
        flashlightAction.started += SwitchFlashlight;
        interactAction.started += Interact;
        interactAction.canceled += StopAllInteraction;
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
        FlashlightEvents.OnFlashLightStatusChanged?.Invoke();
    }

    void Interact(InputAction.CallbackContext callbackContext)
    {
        PlayerEvents.OnPlayerInteract?.Invoke();
    }

    void StopAllInteraction(InputAction.CallbackContext callbackContext)
    {
        PlayerEvents.OnPlayerStoppedInteracting?.Invoke();
    }
}
