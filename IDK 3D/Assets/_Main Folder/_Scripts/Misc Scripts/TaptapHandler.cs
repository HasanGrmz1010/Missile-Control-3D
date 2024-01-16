using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaptapHandler : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] PlayerMissile_CollideHandler p_col;
    [SerializeField] float tapLosePosX, tapWinPosX;

    [SerializeField] RectTransform TapTimerIndicator;
    [SerializeField] RectTransform taptapObj;

    float Tap_Value;
    int multiplier = 0;

    bool indicator_active;

    private void Start()
    {
        indicator_active = true;
        Tap_Value = 0f;
    }

    private void Update()
    {
        taptapObj.anchoredPosition += new Vector2(-100f, 0f) * Time.deltaTime;
        Tap_Value = taptapObj.anchoredPosition.x;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapTimerIndicator.DOPunchScale(Vector2.one / 6, .05f);

                taptapObj.anchoredPosition = new Vector2(taptapObj.anchoredPosition.x + 25f, taptapObj.anchoredPosition.y);
            }
        }

        if (Tap_Value <= -400f && indicator_active)
        {
            p_col.HandlePlayerExploded();
            SoundManager.instance.ExplodeSoundFX();
            Deactivate_TapTapIndicator();
        }
        if (Tap_Value >= 400f && indicator_active)
        {
            UI_Manager.instance.TapTapPlusCoin_HandleUI();
            Deactivate_TapTapIndicator();
        }
    }

    public int GetTaptapMultiplier()
    {
        GameManager.instance.SetMultiplierValue(multiplier);
        return Mathf.RoundToInt(Tap_Value);
    }

    void Deactivate_TapTapIndicator()
    {
        indicator_active = false;
        TapTimerIndicator.DOScale(.01f, .1f).SetEase(Ease.InOutBounce).OnComplete(() =>
        {
            TapTimerIndicator.gameObject.SetActive(false);
        });
    }
}
