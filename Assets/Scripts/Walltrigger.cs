using UnityEngine;

public class Walltrigger : MonoBehaviour
{
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySoundFXClip(AudioSounds.instance.DirtDrop, transform.position);
        }
    }
}
