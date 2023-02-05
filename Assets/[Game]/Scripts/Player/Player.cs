using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public bool isUpsideDown;

    public GameObject Sprites;

    private void OnEnable()
    {
        EventManager.OnUpsideDownWorldTransition.AddListener(OpenAnim);
    }

    private void OnDisable()
    {
        EventManager.OnUpsideDownWorldTransition.AddListener(OpenAnim);
    }

    void OpenAnim(int value)
    {
        if (value ==0 )
        {
            var sprites = Instantiate(Sprites);
            sprites.transform.position = transform.position + new Vector3(0, -5, 0);
        }
        else 
        {
            var sprites = Instantiate(Sprites);
            sprites.transform.DORotate(new Vector3(0,0,180),0.1f);
            sprites.transform.position = transform.position + new Vector3(0, 3, 0);
        }
       
    }


}
