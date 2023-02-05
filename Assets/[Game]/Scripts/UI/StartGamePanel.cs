using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StartGamePanel : InGamePanel
{
    public  TextMeshProUGUI GameName;
    private Image gamePanelImage;
    public GamePanel GamePanel;

    private void Start()
    {
        gamePanelImage = GetComponent<Image>();
        ShowPanel();
        gamePanelImage.DOFade(1, 0.1f);
        GameName.transform.localScale = Vector3.one;
    }
    private void OnDisable()
    {
        gamePanelImage.DOFade(1, 0.1f);
        GameName.transform.localScale= Vector3.one;
    }
    public void  StartGame()
    {
        GamePanel.ShowPanel();
        StartCoroutine(ScaleAndTransparent());

    }
    IEnumerator ScaleAndTransparent()
    {
        GameName.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.1f);
        gamePanelImage.DOFade(0, 0.3f).OnComplete(() => HidePanel());
        
        yield return new WaitForSeconds(0.1f);
    }
}
