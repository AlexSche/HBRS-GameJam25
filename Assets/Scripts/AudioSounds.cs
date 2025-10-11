using UnityEngine;

public class AudioSounds : MonoBehaviour
{
    [field: Header("Main theme")]
    [field: SerializeField] public AudioClip MainTheme { get; private set; }
    [field: Header("Chainsaw SFX")]
    [field: SerializeField] public AudioClip ChargingBattery { get; private set; }
    [field: SerializeField] public AudioClip DirtDrop { get; private set; }
    [field: SerializeField] public AudioClip FootStep { get; private set; }
    [field: SerializeField] public AudioClip BatteryOnOff { get; private set; }
    [field: SerializeField] public AudioClip[] Rise { get; private set; }

    public static AudioSounds instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Sounds in the scene");
        }
        instance = this;
    }
}
