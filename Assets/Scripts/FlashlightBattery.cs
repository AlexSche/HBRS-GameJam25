using System.Collections;
using UnityEngine;

public class FlashlightBattery : MonoBehaviour
{
    [SerializeField] private GameObject lightSource;
    private Light flashlight;
    private float battery = 100f;
    private float intensity = 200;
    private float flickeringIntensity = 0f;
    private bool isFlickering = false;
    private bool isOn;

    private Coroutine dechargingCo;
    private Coroutine chargingCo;

    void Start()
    {
        flashlight = lightSource.GetComponent<Light>();
        intensity = flashlight.intensity;
        isOn = true;
        FlashlightEvents.OnFlashLightStatusChanged += BatteryLogic;
        FlashlightEvents.OnChargingFlashlight += ChargeBattery;
    }

    void Update()
    {
        if (battery == 10 || battery == 3)
        {
            FlickerLight();
        }
        if (battery <= 0)
        {
            if (isOn)
            {
                FlashlightEvents.OnFlashlightBatteryEmpty?.Invoke();
            }
            isOn = false;
            flashlight.intensity = 0;
        }
        else
        {
            isOn = true;
            if (!isFlickering)
            {
                flashlight.intensity = intensity;
            }
        }
    }

    void BatteryLogic(bool isActive)
    {
        if (isActive)
        {
            if (isOn) flashlight.intensity = intensity;
            dechargingCo = StartCoroutine(DechargeBatteryCo());
        }
        else
        {
            StopCoroutine(dechargingCo);
        }
    }

    public void FlickerLight()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(FlickeringLightCo());
        }
    }

    public void ChargeBattery(bool isCharging)
    {
        if (chargingCo == null && isCharging)
        {
            chargingCo = StartCoroutine(ChargeBatteryCo());
        }
        else
        {
            if (chargingCo != null)
            {
                StopCoroutine(chargingCo);
                chargingCo = null;
            }
        }
    }

    public IEnumerator DechargeBatteryCo()
    {
        while (battery > 0)
        {
            yield return new WaitForSeconds(1);
            battery -= 1;
            FlashlightEvents.OnCurrentBatteryStatus?.Invoke(battery);
            if (Random.Range(0, 15) == 0)
            {
                FlickerLight();
            }
        }
    }

    public IEnumerator ChargeBatteryCo()
    {
        while (battery < 100)
        {
            yield return new WaitForSeconds(0.1f);
            battery += 1;
            FlashlightEvents.OnCurrentBatteryStatus?.Invoke(battery);
        }
    }

    public IEnumerator FlickeringLightCo()
    {
        float waiting = 0;
        for (int i = 0; i < 4; i++)
        {
            flashlight.intensity = flickeringIntensity;
            waiting = Random.Range(0.1f, 0.6f);
            yield return new WaitForSeconds(waiting);
            flashlight.intensity = intensity;
            waiting = Random.Range(0.1f, 0.6f);
            yield return new WaitForSeconds(waiting);
        }
        isFlickering = false;
    }
}
