using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StartGamePanel : MonoBehaviour
{
    public  TextMeshProUGUI GameName;
    private Image gamePanelImage;


    private void Start()
    {
        gamePanelImage = GetComponent<Image>();
        gamePanelImage.material.DOFade(1, 0.1f);
        GameName.transform.localScale = Vector3.one;
    }
    private void OnDisable()
    {
        gamePanelImage.material.DOFade(1, 0.1f);
        GameName.transform.localScale= Vector3.one;
    }
    public void  StartGame()
    {

        StartCoroutine(ScaleAndTransparent());

    }
    IEnumerator ScaleAndTransparent()
    {
        GameName.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.4f);
        gamePanelImage.material.DOFade(0, 0.5f).OnComplete(()=> gameObject.SetActive(false));
        yield return new WaitForSeconds(0.2f);
    }
}
