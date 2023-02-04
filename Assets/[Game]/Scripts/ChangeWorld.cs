using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeWorld : MonoBehaviour
{
    CapsuleCollider[] colliders ;
    Rigidbody2D rb;
    Transform _transform;
    public GameObject Body;

    private void Start()
    {
        colliders = GetComponents<CapsuleCollider>();
        rb = GetComponentInChildren<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }
    public void ActivateUpsideDownWorld()
    {

        //foreach (var item in colliders)
        //{
        //    item.isTrigger = true;
        //}
        Body.transform.DORotate(new Vector3(0, 0, -90), 0.5f);
        //transform.DOScale(new Vector3(0.1f,0.1f,0.1f),2);
        Body.transform.DOMoveY(-14, 0.2f).OnComplete(SizeUp);
        EventManager.OnUpsideDownWorldTransition.Invoke();


    }

    void SizeUp()
    {
        var value = Body.transform.localScale.y;
        value *= -1;

        //DOTween.To(()=> value, x=> value = x, (value*-1), 1);
        //transform.DOScale(Vector3.one,2 );
        //foreach (var item in colliders)
        //{
        //    item.isTrigger = false;
        //}
        rb.gravityScale = -1;

    }
}
