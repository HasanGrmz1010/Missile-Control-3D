using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Manager : MonoBehaviour
{
    #region Singleton
    public static UI_Manager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    #endregion
    [SerializeField] GameObject Fuel_Indicator;

    [Header("------------ Panels ------------")]
    [SerializeField] Image GameOverPanel;

    [Header("------------ Game Over Texts ------------")]
    [SerializeField] RectTransform NoFuelLeft_Text;
    [SerializeField] RectTransform Exploded_Text;

    [Header("------------ Score Texts ------------")]
    [SerializeField] TextMeshProUGUI LevelScore_Text;
    [SerializeField] TextMeshProUGUI LevelScoreValue_Text;
    [SerializeField] TextMeshProUGUI HighScore_Text;
    [SerializeField] TextMeshProUGUI HighScoreValue_Text;

    [Header("------------ Game Over Buttons ------------")]
    [SerializeField] Button TryAgain_Button;
    [SerializeField] Button GiveUp_Button;
    [SerializeField] Button DoublePrize_Button;

    [Header("/////////////////////////////////////////////////////////////////////////////////\n")]
    [SerializeField] RectTransform CoinIndicator;
    [SerializeField] RectTransform FuelIndicator;
    [SerializeField] RectTransform TapTapIndicator;

    public void Fuel_GameOverScreen()
    {
        Fuel_Indicator.SetActive(false);

        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.DOFade(1f, 2f);

        float duration = .15f;
        NoFuelLeft_Text.DOLocalMoveY(600f, duration).SetEase(Ease.InCirc).SetDelay(.25f);
        LevelScore_Text.rectTransform.DOLocalMoveX(-120f, duration).SetEase(Ease.InCirc).SetDelay(.4f);
        LevelScoreValue_Text.rectTransform.DOLocalMoveX(175f, duration).SetEase(Ease.InCirc).SetDelay(.4f);
        HighScore_Text.rectTransform.DOLocalMoveX(-120f, duration).SetEase(Ease.InCirc).SetDelay(.4f);
        HighScoreValue_Text.rectTransform.DOLocalMoveX(175f, duration).SetEase(Ease.InCirc).SetDelay(.4f);
        TryAgain_Button.transform.DOLocalMoveY(-250f, duration).SetEase(Ease.InCirc).SetDelay(.6f);
        GiveUp_Button.transform.DOLocalMoveY(-250f, duration).SetEase(Ease.InCirc).SetDelay(.6f);
        DoublePrize_Button.transform.DOLocalMoveY(-500f, duration).SetEase(Ease.InCirc).SetDelay(.8f);
    }

    public void Explode_GameOverScreen()
    {
        Fuel_Indicator.SetActive(false);

        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.DOFade(1f, 1f);

        float duration = .075f;
        Exploded_Text.DOLocalMoveY(600f, duration).SetEase(Ease.InCirc).SetDelay(.125f);
        LevelScore_Text.rectTransform.DOLocalMoveX(-120f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        LevelScoreValue_Text.rectTransform.DOLocalMoveX(175f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        HighScore_Text.rectTransform.DOLocalMoveX(-120f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        HighScoreValue_Text.rectTransform.DOLocalMoveX(175f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        TryAgain_Button.transform.DOLocalMoveY(-250f, duration).SetEase(Ease.InCirc).SetDelay(.3f);
        GiveUp_Button.transform.DOLocalMoveY(-250f, duration).SetEase(Ease.InCirc).SetDelay(.3f);
        DoublePrize_Button.transform.DOLocalMoveY(-500f, duration).SetEase(Ease.InCirc).SetDelay(.4f);
    }

    public void TapTapPhase_HandleUI()
    {
        TapTapIndicator.gameObject.SetActive(true);
        TapTapIndicator.DOLocalMoveY(-300f, .15f).SetEase(Ease.InElastic).OnComplete(() =>
        {
            FuelIndicator.DOLocalMoveX(-700f, .15f);
        });
    }


    #region Tween UI Functions
    public void CoinGained_Tween()
    {
        CoinIndicator.DOPunchScale((Vector3.one / 6), .05f, 1, 1f);
    }

    public void FuelGained_Tween()
    {
        FuelIndicator.DOPunchScale((Vector3.one / 5), .05f, 1, 1f);
    }
    #endregion

}
