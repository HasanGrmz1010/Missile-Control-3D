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

    [SerializeField] ParticleSystem coinBurst_FX;

    [SerializeField] GameData_SO gameData;
    [SerializeField] SettingsData_SO setData;
    [SerializeField] PlayerMissile_Move p_move;

    [Header("\n-------------- Main Menu Buttons --------------")]
    [SerializeField] Button MM_startGameButton;
    [SerializeField] Button MM_toggleMusicButton;
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
            p_move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMissile_Move>();

        isGamePaused = false;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MM_startGameButton.onClick.AddListener(StartGame_LastLevel);
            //MM_toggleMusicButton.onClick.AddListener(ToggleMusic);
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
        SoundManager.instance.PlaySoundFX("button_a", 1f);
        Time.timeScale = 1f;
        SoundManager.instance.ChangeVolume_Music(.4f);
        SoundManager.instance.ResetTaptapPitch();
        SceneManager.LoadScene(0);
        EconomyManager.instance.ResetCoinAndScore();
    }

    public void GameOver_TryAgain()
    {
        // RELOAD CURRENT SCENE
        GameManager.instance.gameState = GameManager.GameState.startPhase;
        GameManager.instance.playerState = GameManager.PlayerState.ableToPlay;
        EconomyManager.instance.ResetCoinAndScore();
        SoundManager.instance.PlaySoundFX("button", 1f);
        SoundManager.instance.ResetTaptapPitch();
        SoundManager.instance.ChangeVolume_Music(.1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CollectCoinsButton()
    {
        SoundManager.instance.PlaySoundFX("collect", 1f);
        SoundManager.instance.ResetTaptapPitch();
        float burstFX_duration = coinBurst_FX.main.duration;
        coinBurst_FX.Play();
        EconomyManager.instance.IncreaseMainCoin(EconomyManager.instance.GetLevelCoinAmount());
        EconomyManager.instance.ResetCoinAndScore();
        Invoke("LoadNextLevel", burstFX_duration);
    }

    void LoadNextLevel()
    {
        GameManager.instance.levelPlayed++;
        if (GameManager.instance.levelPlayed % 3 == 0)
        {
            GameManager.instance.levelPlayed = 1;
            AdsManager.instance.interstitialAd.ShowAd();
        }

        GameManager.instance.gameState = GameManager.GameState.startPhase;
        GameManager.instance.playerState = GameManager.PlayerState.ableToPlay;
        EconomyManager.instance.IncreaseMainCoin(EconomyManager.instance.GetLevelCoinAmount());
        SoundManager.instance.ChangeVolume_Music(.1f);

        if (SceneManager.GetActiveScene().buildIndex == 20)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            gameData.IncreaseGameLevel();
            SceneManager.LoadScene(gameData.GetGameLevel());
        }
    }

    public void DoublePrize_Button()
    {
        // WATCH ADD AND x2 THE COINS
        SoundManager.instance.PlaySoundFX("button_a", 1f);
    }

    public void LevelPassed_NextLevel()
    {
        // ADD COINS TO -TOTAL COINS- AND LOAD NEXT SCENE
        GameManager.instance.gameState = GameManager.GameState.startPhase;
        GameManager.instance.playerState = GameManager.PlayerState.ableToPlay;
        SoundManager.instance.PlaySoundFX("button", 1f);
        SoundManager.instance.ChangeVolume_Music(.1f);
        SoundManager.instance.ResetTaptapPitch();
        EconomyManager.instance.IncreaseMainCoin(EconomyManager.instance.GetLevelCoinAmount());
        EconomyManager.instance.ResetCoinAndScore();
    }

    public void LevelPassed_MainMenu()
    {
        // RETURN THE MAIN MENU
        SoundManager.instance.PlaySoundFX("button_a", 1f);
        SoundManager.instance.ResetTaptapPitch();
        Time.timeScale = 1f;
        EconomyManager.instance.IncreaseMainCoin(EconomyManager.instance.GetLevelCoinAmount());
        EconomyManager.instance.ResetCoinAndScore();
        gameData.IncreaseGameLevel();
        SoundManager.instance.ChangeVolume_Music(.4f);
        SceneManager.LoadScene(0);
    }

    #region Main Menu Button Function
    void StartGame_LastLevel()
    {
        GameManager.instance.gameState = GameManager.GameState.startPhase;
        GameManager.instance.playerState = GameManager.PlayerState.ableToPlay;
        SoundManager.instance.PlaySoundFX("button", 1f);
        SceneManager.LoadScene(gameData.GetGameLevel());
        SoundManager.instance.ChangeVolume_Music(.1f);
    }

    public void ToggleMusic()
    {
        if (setData.Toggle_Music)
        {
            SoundManager.instance.ChangeVolume_Music(0f);
            setData.Toggle_Music = false;
        }
        else
        {
            setData.Toggle_Music = true;
            SoundManager.instance.ChangeVolume_Music(.4f);
        }
    }
    #endregion

    #region Pause Menu Button Function
    void PauseMenu()
    {
        SoundManager.instance.PlaySoundFX("button_a", 1f);
        isGamePaused = true;
        UI_Manager.instance.Handle_PauseMenuScreen("open");
    }

    void PauseMenu_Resume()
    {
        SoundManager.instance.PlaySoundFX("button", 1f);
        isGamePaused = false;
        UI_Manager.instance.Handle_PauseMenuScreen("close");
    }

    void PauseMenu_ReturnMenu()
    {
        SoundManager.instance.PlaySoundFX("button_a", 1f);
        SoundManager.instance.ChangeVolume_Music(.4f);
        EconomyManager.instance.ResetCoinAndScore();
        SceneManager.LoadScene(0);
    }

    void PauseMenu_ExitGame()
    {
        SoundManager.instance.PlaySoundFX("button", 1f);
        EconomyManager.instance.ResetCoinAndScore();
        Application.Quit();
    }
    #endregion


    void ToggleStartLevelButton()
    {
        SoundManager.instance.PlaySoundFX("button_a", 1f);
        p_move.ableToMove = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().useGravity = false;

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
