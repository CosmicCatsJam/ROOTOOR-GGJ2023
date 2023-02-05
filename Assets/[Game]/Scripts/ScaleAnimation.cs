using UnityEngine;
using DG.Tweening;

public class ScaleAnimation : MonoBehaviour
{
    public Ease Ease = Ease.Linear;
    public float Duration = 2f;
    public float ScaleMultiplier = .5f;
    Vector3 rot = new Vector3(0, 0,360);
    private void Start()
    {
        transform.DOScale(transform.localScale * ScaleMultiplier, Duration).SetEase(Ease).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(rot, 2f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear);
    }
}
