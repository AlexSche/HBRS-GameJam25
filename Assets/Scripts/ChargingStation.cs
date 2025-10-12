using UnityEngine;

[RequireComponent(typeof(PathTrail))]
public class ChargingStation : MonoBehaviour
{
    private bool isPlayerNextToChargingStation = false;
    private bool isFirstUse = true;
    [SerializeField] private PathTrail pathTrail;
    [SerializeField] private GameObject chargingLight;

    void Start()
    {
        PlayerEvents.OnPlayerInteract += ChargeBattery;
        PlayerEvents.OnPlayerStoppedInteracting += StopCharging;
        GameEvents.OnGameFinished += ShowStatus;
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
        if (isPlayerNextToChargingStation)
        {
            if (isFirstUse)
            {
                isFirstUse = false;
                pathTrail.DrawLitTrailToCheckpoint();
                chargingLight.SetActive(true);
            }
            FlashlightEvents.OnChargingFlashlight?.Invoke(true);
        }
    }

    void StopCharging()
    {
        FlashlightEvents.OnChargingFlashlight?.Invoke(false);
    }

    void ShowStatus()
    {
        if (isFirstUse)
        {
            chargingLight.SetActive(true);
            chargingLight.GetComponent<Light>().color = Color.red;
            pathTrail.DrawRedLitTrailToCheckpoint();
        }
    }
}
