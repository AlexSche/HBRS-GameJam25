using System;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform endScenePosition;
    [SerializeField] GameObject door;
    public bool isWorking = false;
    private bool isPlayerInElevator;
    private bool isMoving = false;


    void Start()
    {
        PlayerEvents.OnPlayerInteract += StartElevator;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            if (rb.position.y < 73)
            {
                rb.MovePosition(rb.position + Vector3.up * 5 * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + (endScenePosition.position - rb.position).normalized * 10 * Time.fixedDeltaTime);
                if (Vector3.Distance(rb.position, endScenePosition.position) < 1)
                {
                    isMoving = false;
                    door.SetActive(false);
                }
            }
        }
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
            door.SetActive(true);
            isMoving = true;
        }
    }
}
