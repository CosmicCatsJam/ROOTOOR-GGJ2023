using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
     AudioSource musicSources;

    private void Awake()
    {
        musicSources = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        EventManager.OnUpsideDownWorldTransition.AddListener(ChangePitch);
    }

    private void OnDisable()
    {
        EventManager.OnUpsideDownWorldTransition.AddListener(ChangePitch);
    }

    void ChangePitch(int value)
    {
        if (value ==0)
        {
            musicSources.pitch = 1.5f;

        }
        else
        {
            musicSources.pitch = 1f;

        }
    }
}
