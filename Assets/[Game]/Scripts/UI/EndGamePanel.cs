using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : InGamePanel
{
    private void OnEnable()
    {
        EventManager.OnGameEnd.AddListener(()=>StartCoroutine(EndGamePanelOpen()));
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd.RemoveListener(() => StartCoroutine(EndGamePanelOpen()));

    }

    IEnumerator EndGamePanelOpen()
    {
        yield return new WaitForSeconds(4f);
        ShowPanel();
    }
}
