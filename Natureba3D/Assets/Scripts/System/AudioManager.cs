using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip, AudioSource source)
    {
        if (clip == null || source == null) return;

        source.PlayOneShot(clip);
    }
}
