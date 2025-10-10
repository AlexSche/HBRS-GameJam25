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

    void Start()
    {
        FlashlightEvents.OnFlashLightStatusChanged += BatteryLogic;
        flashlight = lightSource.GetComponent<Light>();
        intensity = flashlight.intensity;
        isOn = true;
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
            StartCoroutine(DechargeBatteryCo());
        }
        else
        {
            StopAllCoroutines();
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

    public IEnumerator DechargeBatteryCo()
    {
        while (battery > 0)
        {
            yield return new WaitForSeconds(1);
            battery -= 1;
            Debug.Log("Battery: " + battery);
            if (Random.Range(0, 15) == 0)
            {
                FlickerLight();
            }
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
