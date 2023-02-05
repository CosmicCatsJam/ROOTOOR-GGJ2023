using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class ChangeWorld : MonoBehaviour
{
    public CinemachineVirtualCamera NormalCam;
    public CinemachineVirtualCamera UpsideCam;
    CapsuleCollider[] colliders;
    Rigidbody2D rb;
    Transform _transform;
    public GameObject CurrentPlayer;

    public bool isUpsideDown;

    public enum WorldType { Normal, UpsideDown};
    public WorldType worldType;

    public GameObject UpsideDownPrefab;
    public GameObject NormalPrefab;

    private void Start()
    {
        colliders = GetComponents<CapsuleCollider>();
        rb = GetComponentInChildren<Rigidbody2D>();
        _transform = GetComponent<Transform>();

    }
    public void ActivateUpsideDownWorld()
    {
        if (worldType == WorldType.Normal)
        {
            CurrentPlayer.transform.root.gameObject.SetActive(false);

            worldType = WorldType.UpsideDown;
            var oppositeObj = Instantiate(UpsideDownPrefab);
            oppositeObj.transform.position = CurrentPlayer.transform.position + new Vector3(0, -14, 0);
            CurrentPlayer = oppositeObj.transform.GetChild(1).GetChild(0).gameObject;
            UpsideCam.Priority = 11;
            UpsideCam.m_Follow = CurrentPlayer.transform;
            //UpsideCam.m_LookAt = CurrentPlayer.transform;
        }
        else
        {
            CurrentPlayer.transform.root.gameObject.SetActive(false);

            worldType = WorldType.Normal;

            UpsideCam.Priority = 9;
            var oppositeObj = Instantiate(NormalPrefab);
            oppositeObj.transform.position = CurrentPlayer.transform.position + new Vector3(0, 14, 0);
            CurrentPlayer = oppositeObj.transform.GetChild(1).GetChild(0).gameObject;
            NormalCam.m_Follow = CurrentPlayer.transform;
            //NormalCam.m_LookAt = CurrentPlayer.transform;


        }

        //isUpsideDown = true;

        //transform.DOLocalRotate(new Vector3(0, 0, -90), 0.5f);
        //transform.DOScale(new Vector3(0.1f,0.1f,0.1f),2);

        //var rot = transform.rotation.eulerAngles;
        //rot = new Vector3(0,0,-90);
        //transform.DOMoveY(-14, 0.2f).OnComplete(SizeUp);
        //EventManager.OnUpsideDownWorldTransition.Invoke();




    }

    void SizeUp()
    {
        var value = transform.localScale.y;
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
