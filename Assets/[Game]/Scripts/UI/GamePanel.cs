using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : InGamePanel
{
    private void OnEnable()
    {
        EventManager.OnGameEnd.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd.RemoveListener(HidePanel);
    }
}
