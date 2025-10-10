using UnityEngine.Events;


public static class FlashlightEvents
{
    public static UnityAction<bool> OnFlashLightStatusChanged;
    public static UnityAction OnFlashlightBatteryEmpty;
    public static UnityAction<bool> OnChargingFlashlight;
}
