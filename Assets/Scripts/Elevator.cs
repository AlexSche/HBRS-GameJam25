using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool isWorking = false;
    private bool isPlayerInElevator;

    void Start()
    {
        PlayerEvents.OnPlayerInteract += StartElevator;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInElevator = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInElevator = false;
        }
    }

    void StartElevator()
    {
        if (isWorking && isPlayerInElevator)
        {
            Debug.Log("Ending game!");
        }
    }
}
