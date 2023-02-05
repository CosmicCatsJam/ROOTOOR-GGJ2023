using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSources;
    public AudioClip[] clips;

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

    public void PlayMusic( int i)
    {

        musicSources.PlayOneShot(clips[i], i);
    }

    private void OnEnable()
    {
        EventManager.OnUpsideDownWorldTransition.AddListener(ChangePitch);
    }

    private void OnDisable()
    {
        EventManager.OnUpsideDownWorldTransition.AddListener(ChangePitch);

    }

    void ChangePitch()
    {
        musicSources.pitch = 1.5f;
    }

}