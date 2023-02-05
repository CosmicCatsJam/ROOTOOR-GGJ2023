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
        EventManager.OnUpsideDownWorldTransition.AddListener(ActivateAnim);
    }

    private void OnDisable()
    {
        EventManager.OnUpsideDownWorldTransition.AddListener(ActivateAnim);
    }

    void ActivateAnim()
    {
        DOTween.To(() => value, x => value = x, (value * -1), 1).OnUpdate(() => myRenderer.sharedMaterial.SetFloat("_GhostFX_ClipDown_1", value));

        StartCoroutine(SetFloat());

    }

    IEnumerator SetFloat()
    {
        isChange = true;
        yield return new WaitForSeconds(0.2f);


    }

    private void Update()
    {
        if (isChange)
        {

            
        }

    }
}
