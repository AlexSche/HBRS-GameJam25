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
    private bool isOn = false;

    private Coroutine dechargingCo;
    private Coroutine chargingCo;

    void Start()
    {
        flashlight = lightSource.GetComponent<Light>();
        intensity = flashlight.intensity;
        SwitchBatteryOnOff();
        FlashlightEvents.OnFlashLightStatusChanged += SwitchBatteryOnOff;
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
            if (!isFlickering)
            {
                flashlight.intensity = intensity;
            }
        }
    }

    void SwitchBatteryOnOff()
    {
        isOn = !isOn;
        AudioManager.instance.PlaySoundFXClip(AudioSounds.instance.BatteryOnOff, transform.position);
        lightSource.SetActive(isOn);
        if (isOn)
        {
            flashlight.intensity = intensity;
            dechargingCo = StartCoroutine(DechargeBatteryCo());
        }
        else
        {
            if (dechargingCo != null) StopCoroutine(dechargingCo);
            dechargingCo = null;
        }
    }

    public void FlickerLight()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            Vector3 followingPos = transform.position;
            followingPos.x -= 10;
            followingPos.z -= 10;
            AudioManager.instance.PlayRandomSoundFXClip(AudioSounds.instance.Rise, followingPos);
            StartCoroutine(FlickeringLightCo());
        }
    }

    public void ChargeBattery(bool isCharging)
    {
        if (chargingCo == null && isCharging)
        {
            // start playing looping sound
            AudioManager.instance.PlaySoundFXClipLoop(AudioSounds.instance.ChargingBattery, transform.position);
            chargingCo = StartCoroutine(ChargeBatteryCo());
        }
        else
        {
            if (chargingCo != null)
            {
                // stop playing looping sound
                AudioManager.instance.StopSoundFXClipLoop();
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
            AudioManager.instance.PlaySoundFXClip(AudioSounds.instance.BatteryOnOff, transform.position);
            yield return new WaitForSeconds(waiting);
            flashlight.intensity = intensity;
            AudioManager.instance.PlaySoundFXClip(AudioSounds.instance.BatteryOnOff, transform.position);
            waiting = Random.Range(0.1f, 0.6f);
            yield return new WaitForSeconds(waiting);
        }
        isFlickering = false;
    }
}
