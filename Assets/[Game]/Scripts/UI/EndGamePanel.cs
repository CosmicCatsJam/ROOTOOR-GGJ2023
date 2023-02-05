using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : InGamePanel
{
    private void OnEnable()
    {
        EventManager.OnGameEnd.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd.RemoveListener(ShowPanel);

    }
}
