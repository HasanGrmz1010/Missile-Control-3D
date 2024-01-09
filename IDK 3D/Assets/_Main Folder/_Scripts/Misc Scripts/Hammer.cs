using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] float swingTime;
    [SerializeField] Vector3 swingLeftWingRot, swingRightWingRot;

    bool playerStartedLevel;

    private void Start()
    {
        playerStartedLevel = false;
    }

    private void Update()
    {
        if (playerStartedLevel) return;
        else if (GameManager.instance.gameState == GameManager.GameState.playing)
        {
            playerStartedLevel = true;
            SwingMovement();
        }
    }

    void SwingMovement()
    {
        transform.DORotate(swingLeftWingRot, swingTime, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>{
                transform.DORotate(swingRightWingRot, swingTime, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad);
            })
            .SetLoops(-1, LoopType.Yoyo);
    }
}
