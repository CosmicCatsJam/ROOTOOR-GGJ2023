using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRootAnim : MonoBehaviour
{
    private MaterialPropertyBlock m_PropertyBlock;
    public Renderer myRenderer;
    bool isChange;

    float value;
    void Start()
    {
        myRenderer = GetComponentInChildren<Renderer>();
        m_PropertyBlock = new MaterialPropertyBlock();
    }

    private void OnEnable()
    {
        //EventManager.OnUpsideDownWorldTransition.AddListener(ActivateAnim);
        //myRenderer.sharedMaterial.SetFloat("_GhostFX_ClipDown_1", 0);
        ActivateAnim();
    }

    private void OnDisable()
    {
        //EventManager.OnUpsideDownWorldTransition.AddListener(ActivateAnim);
        myRenderer.sharedMaterial.SetFloat("_GhostFX_ClipDown_1", 0);
    }
    private void OnDestroy()
    {
        myRenderer.sharedMaterial.SetFloat("_GhostFX_ClipDown_1", 0);
    }
    void ActivateAnim()
    {
        StartCoroutine(SetFloat());
    }

    IEnumerator SetFloat()
    {
        isChange = true;
        DOTween.To(() => value, x => value = x, 1, 1);
        yield return new WaitForSeconds(1f);
        isChange = false;
    }

    private void Update()
    {
        if (isChange)
        {
            myRenderer.sharedMaterial.SetFloat("_GhostFX_ClipDown_1", value);
        }
    }
}
