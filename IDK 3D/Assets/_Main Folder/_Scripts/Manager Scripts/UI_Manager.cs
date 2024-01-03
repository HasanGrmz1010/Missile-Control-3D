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

    [Header("============ Panels ============")]
    [SerializeField] Image GameOverPanel;

    [Header("============ Game Over Texts ============")]
    [SerializeField] RectTransform NoFuelLeft_Text;
    [SerializeField] RectTransform Exploded_Text;

    [Header("============ Score Texts ============")]
    [SerializeField] TextMeshProUGUI LevelScore_Text;
    [SerializeField] TextMeshProUGUI LevelScoreValue_Text;
    [SerializeField] TextMeshProUGUI HighScore_Text;
    [SerializeField] TextMeshProUGUI HighScoreValue_Text;

    [Header("============ Game Over Buttons ============")]
    [SerializeField] Button TryAgain_Button;
    [SerializeField] Button GiveUp_Button;
    [SerializeField] Button DoublePrize_Button;

    public void Fuel_GameOverScreen()
    {
        Fuel_Indicator.SetActive(false);

        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.DOFade(1f, 2f);

        NoFuelLeft_Text.DOLocalMoveY(600f, .15f).SetEase(Ease.InCirc).SetDelay(.25f);
        LevelScore_Text.rectTransform.DOLocalMoveX(-120f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        LevelScoreValue_Text.rectTransform.DOLocalMoveX(175f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        HighScore_Text.rectTransform.DOLocalMoveX(-120f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        HighScoreValue_Text.rectTransform.DOLocalMoveX(175f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        TryAgain_Button.transform.DOLocalMoveY(-250f, .15f).SetEase(Ease.InCirc).SetDelay(.6f);
        GiveUp_Button.transform.DOLocalMoveY(-250f, .15f).SetEase(Ease.InCirc).SetDelay(.6f);
        DoublePrize_Button.transform.DOLocalMoveY(-500f, .15f).SetEase(Ease.InCirc).SetDelay(.8f);
    }

    public void Explode_GameOverScreen()
    {
        Fuel_Indicator.SetActive(false);

        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.DOFade(1f, 2f);

        Exploded_Text.DOLocalMoveY(600f, .15f).SetEase(Ease.InCirc).SetDelay(.25f);
        LevelScore_Text.rectTransform.DOLocalMoveX(-120f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        LevelScoreValue_Text.rectTransform.DOLocalMoveX(175f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        HighScore_Text.rectTransform.DOLocalMoveX(-120f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        HighScoreValue_Text.rectTransform.DOLocalMoveX(175f, .15f).SetEase(Ease.InCirc).SetDelay(.4f);
        TryAgain_Button.transform.DOLocalMoveY(-250f, .15f).SetEase(Ease.InCirc).SetDelay(.6f);
        GiveUp_Button.transform.DOLocalMoveY(-250f, .15f).SetEase(Ease.InCirc).SetDelay(.6f);
        DoublePrize_Button.transform.DOLocalMoveY(-500f, .15f).SetEase(Ease.InCirc).SetDelay(.8f);
    }
}
