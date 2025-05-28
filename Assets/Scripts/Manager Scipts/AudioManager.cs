using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip whooshed;
    public AudioClip alarm;
    public AudioClip brush;
    public AudioClip carHorn;
    public AudioClip coinCollected;
    public AudioClip energyCollected;

    public bool audioLoaded = false;

    //add more clip
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator LoadAudio()
    {
        if (!audioLoaded)
        {
            musicSource.clip = background;

            audioLoaded = true;
            yield return new WaitUntil(() => GameManager.instance.gameState == GameState.Playing);
            SFXSource.PlayOneShot(alarm);
            PlayMusic();
            Debug.Log("Audio Loaded!!!");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlayMusic()
    {
        musicSource.Play();
    }
}
