using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction flashlightAction;
    private InputAction interactAction;
    private bool isFlashlightActive = true;
    [SerializeField] private GameObject flashlightGameObject;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        flashlightAction = playerInput.actions.FindActionMap("Player").FindAction("Flashlight");
        interactAction = playerInput.actions.FindActionMap("Player").FindAction("Interact");
        flashlightAction.started += SwitchFlashlight;
        interactAction.started += Interact;
        interactAction.canceled += StopAllInteraction;
    }

    void Start()
    {
        isFlashlightActive = true;
        FlashlightEvents.OnFlashLightStatusChanged?.Invoke(isFlashlightActive);
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
        FlashlightEvents.OnFlashLightStatusChanged?.Invoke(isFlashlightActive);
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
