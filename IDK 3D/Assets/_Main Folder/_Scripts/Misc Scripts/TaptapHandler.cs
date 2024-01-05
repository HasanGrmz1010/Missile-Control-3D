using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaptapHandler : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] float tapLosePosX, tapWinPosX;

    [SerializeField] RectTransform TapTimerIndicator;
    [SerializeField] RectTransform taptapObj;

    private void Start()
    {

    }

    private void Update()
    {
        taptapObj.anchoredPosition += new Vector2(-100f, 0f) * Time.deltaTime; 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                TapTimerIndicator.DOPunchScale(Vector2.one / 6, .05f);

                taptapObj.anchoredPosition = new Vector2(taptapObj.anchoredPosition.x + 25f, taptapObj.anchoredPosition.y);
            }
        }
    }
}
