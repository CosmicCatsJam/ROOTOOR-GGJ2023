using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] musicSources;

    public static AudioManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip musicClip,int i)
    {
        musicSources[i].clip = musicClip;
        musicSources[i].Play();
    }

}