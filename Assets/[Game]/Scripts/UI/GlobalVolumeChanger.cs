using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolumeChanger : MonoBehaviour
{
    Volume volume;
    private void Start()
    {
        volume = GetComponent<Volume>();
    }
    private void OnEnable()
    {
        EventManager.OnLevelUp.AddListener(LightTurnOff);
    }
    private void OnDisable()
    {
        EventManager.OnLevelUp.RemoveListener(LightTurnOff);
    }

    public void LightTurnOff()
    {
        DOTween.To(() => volume.weight, x => volume.weight = x, 1, 1).OnComplete(LightTurnOn);
    }
    public void LightTurnOn()
    {
        DOTween.To(() => volume.weight, x => volume.weight = x, 0, 1);
    }

}
