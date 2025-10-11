using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Unity.UI.Shaders.Sample;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource soundFXObject;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        instance = this;
    }
    public void PlaySoundFXClip(AudioClip audioClip, Vector3 position)
    {
        AudioSource audioSource = Instantiate(soundFXObject, position, Quaternion.identity);
        audioSource.clip = audioClip;
        //audioSource.volume = sfxVolume;
        float clipLength = audioSource.clip.length;
        audioSource.Play();
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Vector3 position)
    {
        AudioSource audioSource = Instantiate(soundFXObject, position, Quaternion.identity);
        audioSource.clip = audioClip[UnityEngine.Random.Range(0, audioClip.Length)];
        float sfxVolume = 1f;
        audioMixer.GetFloat("SfxVolume", out sfxVolume);
        audioSource.volume = sfxVolume;
        float clipLength = audioSource.clip.length;
        audioSource.Play();
        Destroy(audioSource.gameObject, clipLength);
    }

    public void SetMasterVolume(CustomSlider slider)
    {
        audioMixer.SetFloat("MasterVolume", MapToDbValue(slider.Value));
    }
    public void SetMusicVolume(CustomSlider slider)
    {
        audioMixer.SetFloat("MusicVolume", MapToDbValue(slider.Value));
    }
    public void SetSfxVolume(CustomSlider slider)
    {
        audioMixer.SetFloat("SfxVolume", MapToDbValue(slider.Value));
    }

    private static float MapToDbValue(float value)
    {
        // 0 - 1 isn't working we need -80 to 0 (0 is -80 and 1 is 0)
        float correctedValue = -80 + (0 - (-80)) * value;
        return correctedValue;
    }
}
