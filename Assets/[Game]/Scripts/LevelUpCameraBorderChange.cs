using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpCameraBorderChange : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnLevelUp.AddListener(OnBecameInvisible);
    }
    private void OnDisable()
    {
        EventManager.OnLevelUp.RemoveListener(OnBecameInvisible);
    }

    public void OnBecameInvisible()
    {
        transform.localPosition += new Vector3(40, 0, 0);
    }
}
