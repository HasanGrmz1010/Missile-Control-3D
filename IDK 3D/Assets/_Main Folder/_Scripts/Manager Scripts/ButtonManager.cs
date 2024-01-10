using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    #region Singleton
    private static ButtonManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameData_SO gameData;
    [SerializeField] PlayerMissile_Move p_move;

    [Header("\n-------------- Main Menu Buttons --------------")]
    [SerializeField] Button MM_startGameButton;
    [Header("\n-------------- Pause Menu Buttons --------------")]
    [SerializeField] Button PM_pauseButton;
    [SerializeField] Button PM_resumeButton;
    [SerializeField] Button PM_returnMenuButton;
    [SerializeField] Button PM_exitGameButton;
    [Header("\n-------------- Settings Menu Buttons --------------")]
    [SerializeField] Button SM_settingButton;
    [SerializeField] Button SM_closeButton;
    

    [SerializeField] Button startLevelButton;
    bool isGamePaused;

    private void Start()
    {
        isGamePaused = false;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MM_startGameButton.onClick.AddListener(StartGame_LastLevel);
        }

        else
        {
            PM_pauseButton.onClick.AddListener(PauseMenu);
            PM_resumeButton.onClick.AddListener(PauseMenu_Resume);
            PM_returnMenuButton.onClick.AddListener(PauseMenu_ReturnMenu);
            PM_exitGameButton.onClick.AddListener(PauseMenu_ExitGame);

            startLevelButton.onClick.AddListener(ToggleStartLevelButton);
        }
    }

    public void GameOver_GiveUp()
    {
        // LOSE LEVEL COINS AND RETURN MAIN MENU
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GameOver_TryAgain()
    {
        // RELOAD CURRENT SCENE
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DoublePrize_Button()
    {
        // WATCH ADD AND x2 THE COINS
    }

    public void LevelPassed_NextLevel()
    {
        // ADD COINS TO -TOTAL COINS- AND LOAD NEXT SCENE
        EconomyManager.instance.IncreaseMainCoin(EconomyManager.instance.GetLevelCoinAmount());
    }

    public void LevelPassed_MainMenu()
    {
        // RETURN THE MAIN MENU
        EconomyManager.instance.IncreaseMainCoin(EconomyManager.instance.GetLevelCoinAmount());
        SceneManager.LoadScene(0);
    }

    #region Main Menu Button Function
    void StartGame_LastLevel()
    {
        SceneManager.LoadScene(gameData.GetGameLevel());
    }
    #endregion

    #region Pause Menu Button Function
    void PauseMenu()
    {
        isGamePaused = true;
        UI_Manager.instance.Handle_PauseMenuScreen("open");
    }

    void PauseMenu_Resume()
    {
        isGamePaused = false;
        UI_Manager.instance.Handle_PauseMenuScreen("close");
    }

    void PauseMenu_ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    void PauseMenu_ExitGame()
    {
        Application.Quit();
    }
    #endregion


    void ToggleStartLevelButton()
    {
        p_move.ableToMove = true;
        DOTween.To(() => p_move.MainCamera.fieldOfView, set => p_move.MainCamera.fieldOfView = set, 35f, 1f).SetEase(Ease.OutQuad);
        PM_pauseButton.transform.DOLocalMoveX(-1250f, .4f).SetEase(Ease.InBack).OnComplete(() =>
        {
            PM_pauseButton.gameObject.SetActive(false);
        });
        startLevelButton.transform.DOScale(.01f, .25f).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            startLevelButton.gameObject.SetActive(false);
        });
    }
}
