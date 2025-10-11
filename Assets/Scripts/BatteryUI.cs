using UnityEngine;
using Unity.UI.Shaders.Sample;
public class BatteryUI : MonoBehaviour
{
    [SerializeField] private CustomSlider customSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (customSlider == null) customSlider = GetComponent<CustomSlider>();
        FlashlightEvents.OnCurrentBatteryStatus += ChangeSliderValue;
    }

    public void ChangeSliderValue(float value)
    {
        customSlider.SetValue(value / 100);
    }
}
