using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OpenAnim()
    {
        var sprites = Instantiate(Sprites);
        sprites.transform.position = transform.position + new Vector3(0,-5,0);
    }


}
