using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject ingameMenu;
    [SerializeField] private GameObject settingsMenu;
    bool isOpen = false;
    void Start()
    {
        pauseAction.action.started += MenuLogic;
    }

    void MenuLogic(InputAction.CallbackContext context)
    {
        if (isOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
        isOpen = !isOpen;
    }

    void OpenMenu()
    {
        Debug.Log("Open Menu!");
        Time.timeScale = 0;
        audioSource.Pause();
        ingameMenu.SetActive(true);
        ActivateCursor();
    }

    public void CloseMenu()
    {
        Debug.Log("Close Menu");
        Time.timeScale = 1;
        audioSource.Play();
        ingameMenu.SetActive(false);
        settingsMenu.SetActive(false);
        DeactivateCursor();
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    private void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
