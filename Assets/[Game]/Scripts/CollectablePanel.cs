using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectablePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _score;

    private void OnEnable()
    {
        EventManager.OnCollect.AddListener(IncreaseScore);
    }

    private void OnDisable()
    {
        EventManager.OnCollect.RemoveListener(IncreaseScore);
    }

    void IncreaseScore()
    {
        _score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        _text.text = _score.ToString();
    }
}
