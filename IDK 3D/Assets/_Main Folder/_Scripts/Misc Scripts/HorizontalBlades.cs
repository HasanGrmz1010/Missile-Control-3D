using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBlades : MonoBehaviour
{
    [SerializeField] Transform blade1;
    [SerializeField] Transform blade2;
    [SerializeField] Transform blade3;

    float tweenTime;

    private void Start()
    {
        tweenTime = 2f;
        HandleBladeMovement();
    }

    void HandleBladeMovement()
    {
        blade1.DOLocalMoveY(21f, tweenTime).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            blade1.DOLocalMoveY(-2f, tweenTime).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        });

        blade2.DOLocalMoveY(21f, tweenTime).SetEase(Ease.InOutFlash).SetDelay(1f).OnComplete(() =>
        {
            blade2.DOLocalMoveY(2f, tweenTime).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        });

        blade3.DOLocalMoveY(21f, tweenTime).SetEase(Ease.InOutFlash).SetDelay(2f).OnComplete(() =>
        {
            blade3.DOLocalMoveY(2f, tweenTime).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        });
    }
}
