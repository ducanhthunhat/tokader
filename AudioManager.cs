using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //[SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    
    public AudioClip iceBall;
    public AudioClip throwball;
    public AudioClip jump;
    public AudioClip die;
    public AudioClip takedamage;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
