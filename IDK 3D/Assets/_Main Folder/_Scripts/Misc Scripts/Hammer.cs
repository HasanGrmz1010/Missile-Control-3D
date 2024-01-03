using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] float swingTime;
    [SerializeField] Vector3 swingLeftWingRot, swingRightWingRot;

    private void Start()
    {
        SwingMovement();
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
