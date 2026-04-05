using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip dayTrack, nightTrack;
    public enum MusicTrack
    {
        Day,
        Night
    };

    public MusicTrack CurrentTrack;

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

    public void SwitchTrack(MusicTrack track)
    {
        if (CurrentTrack == track) return;

        CurrentTrack = track;

        AudioClip newTrack = null;

        switch (track)
        {
            case MusicTrack.Day:
                newTrack = dayTrack;
                break;

            case MusicTrack.Night:
                newTrack = nightTrack;
                break;
        }

        StartCoroutine(FadeAndSwitch(newTrack));
    }

    private IEnumerator FadeAndSwitch(AudioClip newClip)
    {
        float duration = 1f;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= Time.deltaTime / duration;
            yield return null;
        }

        musicSource.clip = newClip;
        musicSource.Play();

        while (musicSource.volume < 1)
        {
            musicSource.volume += Time.deltaTime / duration;
            yield return null;
        }
    }
}
