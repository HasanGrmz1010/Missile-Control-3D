using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriWall : MonoBehaviour
{
    [SerializeField] Transform wall1;
    [SerializeField] Transform wall2;
    [SerializeField] Transform wall3;

    float tweenTime;

    private void Start()
    {
        tweenTime = 2f;
        HandleTriWallMovement();
    }

    void HandleTriWallMovement()
    {
        wall1.DOLocalMoveY(9f, tweenTime).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            wall1.DOLocalMoveY(-9f, tweenTime).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        });

        wall2.DOLocalMoveY(9f, tweenTime).SetEase(Ease.InOutFlash).SetDelay(1f).OnComplete(() =>
        {
            wall2.DOLocalMoveY(-9f, tweenTime).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        });

        wall3.DOLocalMoveY(9f, tweenTime).SetEase(Ease.InOutFlash).SetDelay(2f).OnComplete(() =>
        {
            wall3.DOLocalMoveY(-9f, tweenTime).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        });
    }
}
