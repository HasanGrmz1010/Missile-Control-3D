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

    [SerializeField] GameObject PauseButton;

    [Header("------------ Panels ------------")]
    [SerializeField] Image GameOverPanel;
    [SerializeField] Image LevelPassedPanel;
    [SerializeField] Image PauseMenuPanel;

    [Header("------------ Game Over Texts ------------")]
    [SerializeField] RectTransform NoFuelLeft_Text;
    [SerializeField] RectTransform Exploded_Text;
    [SerializeField] RectTransform LevelPassed_Text;

    [Header("------------ Score Texts ------------")]
    [SerializeField] TextMeshProUGUI LevelScore_Text;
    [SerializeField] TextMeshProUGUI LevelScoreValue_Text;
    [SerializeField] TextMeshProUGUI HighScore_Text;
    [SerializeField] TextMeshProUGUI HighScoreValue_Text;

    [Header("------------ Buttons ------------")]
    [SerializeField] Button TryAgain_Button;
    [SerializeField] Button GiveUp_Button;
    [SerializeField] Button DoublePrize_Button;
    [SerializeField] Button Continue_Button;
    [SerializeField] Button MainMenu_Button;

    [Header("/////////////////////////////////////////////////////////////////////////////////\n")]
    [SerializeField] RectTransform CoinIndicator;
    [SerializeField] RectTransform FuelIndicator;
    [SerializeField] RectTransform TapTapIndicator;
    [SerializeField] Color LevelWin_Color;
    [SerializeField] Color LevelLose_Color;

    public void Handle_Fuel_GameOverScreen()
    {
        FuelIndicator.gameObject.SetActive(false);
        PauseButton.SetActive(false);
        CoinIndicator.gameObject.SetActive(false);

        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.DOColor(LevelLose_Color, 1.25f);
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

    public void Handle_Explode_GameOverScreen()
    {
        FuelIndicator.gameObject.SetActive(false);
        PauseButton.SetActive(false);
        CoinIndicator.gameObject.SetActive(false);

        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.DOColor(LevelLose_Color, .4f);
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

    public void Handle_LevelPassedScreen()
    {
        FuelIndicator.gameObject.SetActive(false);
        CoinIndicator.gameObject.SetActive(false);
        TapTapIndicator.gameObject.SetActive(false);
        PauseButton.SetActive(false);

        LevelPassedPanel.gameObject.SetActive(true);
        LevelPassedPanel.DOColor(LevelWin_Color, .4f);
        LevelPassedPanel.DOFade(1f, 1f);

        float duration = .075f;
        LevelPassed_Text.DOLocalMoveY(600f, duration).SetEase(Ease.InCirc).SetDelay(.125f);
        LevelScore_Text.rectTransform.DOLocalMoveX(-120f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        LevelScoreValue_Text.rectTransform.DOLocalMoveX(175f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        HighScore_Text.rectTransform.DOLocalMoveX(-120f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        HighScoreValue_Text.rectTransform.DOLocalMoveX(175f, duration).SetEase(Ease.InCirc).SetDelay(.2f);
        Continue_Button.transform.DOLocalMoveY(-250f, duration).SetEase(Ease.InCirc).SetDelay(.3f);
        MainMenu_Button.transform.DOLocalMoveY(-250f, duration).SetEase(Ease.InCirc).SetDelay(.3f);
        DoublePrize_Button.transform.DOLocalMoveY(-500f, duration).SetEase(Ease.InCirc).SetDelay(.4f);
    }

    public void Handle_PauseMenuScreen(string mode)
    {
        switch (mode)
        {
            case "open":
                PauseMenuPanel.gameObject.SetActive(true);
                PauseMenuPanel.DOFade(1f, .25f);
                PauseMenuPanel.rectTransform.DOLocalMoveX(0f, .25f).SetEase(Ease.OutBack);
                break;

            case "close":
                PauseMenuPanel.DOFade(.3f, .3f);
                PauseMenuPanel.rectTransform.DOLocalMoveX(1400f, .3f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    PauseMenuPanel.rectTransform.anchoredPosition = new Vector2(-1400f, 0f);
                    PauseMenuPanel.gameObject.SetActive(false);
                });
                break;

            default:
                break;
        }
        
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
        FuelIndicator.DOPunchScale((Vector3.one / 5), .5f, 1, 1f);
    }
    #endregion

}
