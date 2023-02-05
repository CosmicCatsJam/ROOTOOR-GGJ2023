using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinalTree : MonoBehaviour
{
    public CollectablePanel CollectablePanel;
    public GameObject CollectableTexture;
    public Transform Pos;

    private MaterialPropertyBlock m_PropertyBlock;
    public Renderer myRenderer;
    bool isChange;
    bool isFade;

    float value;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {

            StartCoroutine(GoToTree());

            GetComponent<Animator>().SetTrigger("Grow");
        }
    }

    IEnumerator GoToTree()
    {
       var obj=  Instantiate(CollectableTexture);
        obj.transform.position = Pos.position;
        obj.transform.DOMove(transform.position, 1);
        CollectablePanel._score--;
        yield return new WaitForSeconds(0.5f);
        if (CollectablePanel._score >0)
        {
            GoToTree();
        }
        
    }

    IEnumerator SetFloat()
    {
        isChange = true;
        DOTween.To(() => value, x => value = x, 1, 1);
        yield return new WaitForSeconds(0.1f);
    

    }

    private void Update()
    {
        if (isChange)
        {
            myRenderer.sharedMaterial.SetFloat("_GhostFX_ClipDown_1", value);
        }
        if (isFade)
        {
            myRenderer.sharedMaterial.SetFloat("_GhostFX_ClipDown_1", value);

        }
    }
}
