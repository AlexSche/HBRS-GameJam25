using UnityEngine;

[RequireComponent(typeof(PathTrail))]
public class ChargingStation : MonoBehaviour
{
    private bool isPlayerNextToChargingStation = false;
    private bool isFirstUse = true;
    [SerializeField] private PathTrail pathTrail;

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
        if (isFirstUse)
        {
            isFirstUse = false;
            pathTrail.DrawLitTrailToCheckpoint();
        }
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
