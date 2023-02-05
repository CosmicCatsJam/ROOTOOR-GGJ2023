using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public bool isUpsideDown;
    public bool canRootable;

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
        if (value == 0)
        {
            var sprites = Instantiate(Sprites);
            sprites.transform.position = transform.position + new Vector3(0, -5, 0);
        }
        else
        {
            var sprites = Instantiate(Sprites);
            sprites.transform.DORotate(new Vector3(0, 0, 180), 0.1f);
            sprites.transform.position = transform.position + new Vector3(0, 3, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.gameObject.tag =="Rootable")
        {
            Debug.Log("rootable true");
            canRootable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.gameObject.tag == "Rootable")
        {
            Debug.Log("rootable false");
            canRootable = false;

        }
    }


}
