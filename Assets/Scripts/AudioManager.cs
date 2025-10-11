using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 0.5f;
    [Header("AudioSource")]
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
        audioSource.volume = sfxVolume;
        float clipLength = audioSource.clip.length;
        audioSource.Play();
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Vector3 position)
    {
        AudioSource audioSource = Instantiate(soundFXObject, position, Quaternion.identity);
        audioSource.clip = audioClip[Random.Range(0, audioClip.Length)];
        audioSource.volume = sfxVolume;
        float clipLength = audioSource.clip.length;
        audioSource.Play();
        Destroy(audioSource.gameObject, clipLength);
    }
}
