using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    private bool isPlayerNextToChargingStation = false;

    void Start()
    {
        PlayerEvents.OnPlayerInteract += ChargeBattery;
        PlayerEvents.OnPlayerStoppedInteracting += StopCharging;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerNextToChargingStation = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerNextToChargingStation = false;
            StopCharging();
        }
    }

    void ChargeBattery()
    {
        Debug.Log("Can interact: " + isPlayerNextToChargingStation);
        if (isPlayerNextToChargingStation)
        {
            FlashlightEvents.OnChargingFlashlight?.Invoke(true);
        }
    }

    void StopCharging()
    {
        FlashlightEvents.OnChargingFlashlight?.Invoke(false);
    }
}
