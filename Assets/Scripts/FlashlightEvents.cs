using UnityEngine.Events;


public static class FlashlightEvents
{
    public static UnityAction OnFlashLightStatusChanged;
    public static UnityAction OnFlashlightBatteryEmpty;
    public static UnityAction<bool> OnChargingFlashlight;
    public static UnityAction<float> OnCurrentBatteryStatus;
}
