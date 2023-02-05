using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using static UnityEngine.Rendering.DebugUI;

public class ChangeWorld : MonoBehaviour
{
    public CinemachineVirtualCamera NormalCam;
    public CinemachineVirtualCamera UpsideCam;
    public CinemachineVirtualCamera EndGameCam;
    CapsuleCollider[] colliders;
    Rigidbody2D rb;
    Transform _transform;
    public GameObject CurrentPlayer;

    public Button ChangeWorldButton;



    public bool isUpsideDown;

    public enum WorldType { Normal, UpsideDown };
    public WorldType worldType;

    public GameObject UpsideDownPrefab;
    public GameObject NormalPrefab;



    private void OnEnable()
    {
        EventManager.OnGameEnd.AddListener(EndGameCameraView);
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd.RemoveListener(EndGameCameraView);
    }
    void EndGameCameraView()
    {
        DOTween.To(() => NormalCam.m_Lens.OrthographicSize, x => NormalCam.m_Lens.OrthographicSize = x, 10.4f, 2);
        NormalCam.Follow = null;
        NormalCam.transform.DOMoveY(7.13f,2);

    }
    private void Start()
    {
        colliders = GetComponents<CapsuleCollider>();
        rb = GetComponentInChildren<Rigidbody2D>();
        _transform = GetComponent<Transform>();


    }
    public void ActivateUpsideDownWorld()
    {
        if (!CurrentPlayer.GetComponent<Player>().canRootable)
        {
            return;
        }
        if (worldType == WorldType.Normal)
        {

            EventManager.OnUpsideDownWorldTransition.Invoke(0);
            //CurrentPlayer.transform.root.gameObject.SetActive(false);
            Destroy(CurrentPlayer.transform.root.gameObject);

            worldType = WorldType.UpsideDown;
            var oppositeObj = Instantiate(UpsideDownPrefab);
            oppositeObj.transform.position = CurrentPlayer.transform.position + new Vector3(0, -3, 0);
            CurrentPlayer = oppositeObj.transform.GetChild(1).GetChild(0).gameObject;
            UpsideCam.Priority = 11;
            UpsideCam.m_Follow = CurrentPlayer.transform;
            //UpsideCam.m_LookAt = CurrentPlayer.transform;


        }
        else
        {
            EventManager.OnUpsideDownWorldTransition.Invoke(1);

            //CurrentPlayer.transform.root.gameObject.SetActive(false);
            Destroy(CurrentPlayer.transform.root.gameObject);


            worldType = WorldType.Normal;

            UpsideCam.Priority = 9;
            var oppositeObj = Instantiate(NormalPrefab);
            oppositeObj.transform.position = CurrentPlayer.transform.position + new Vector3(0, 5, 0);
            CurrentPlayer = oppositeObj.transform.GetChild(1).GetChild(0).gameObject;
            NormalCam.m_Follow = CurrentPlayer.transform;
            //NormalCam.m_LookAt = CurrentPlayer.transform;


        }






    }

    public void ButtonActivity(bool ca)
    {

    }



}
