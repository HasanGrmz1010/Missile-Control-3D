using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EconomyManager : MonoBehaviour
{
    #region Singleton
    public static EconomyManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else
        {
            instance = this;
            mainCoin = gameData.GetTotalCoinValue();
            mainGem = gameData.GetGemsValue();
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    [SerializeField] GameData_SO gameData;

    [Header(" ======== Level Coin Text ========")]
    [SerializeField] TextMeshProUGUI coinValueText;
    [SerializeField] TextMeshProUGUI MM_coinValueText;
    [SerializeField] TextMeshProUGUI MM_gemValueText;

    [SerializeField] private int mainCoin;
    [SerializeField] private int levelCoin;
    [SerializeField] private int levelScore;
    [SerializeField] private int mainGem;

    private void Start()
    {
        SceneManager.activeSceneChanged += AssignCoinText;

        MM_coinValueText.text = gameData.GetTotalCoinValue().ToString();
        MM_gemValueText.text = gameData.GetGemsValue().ToString();

        levelCoin = 0;
        levelScore = 0;
    }

    #region Coin Functions
    public void IncreaseLevelCoin(int _val)
    {
        if (_val > 0)
        {
            levelCoin += _val;
            coinValueText.text = levelCoin.ToString();
        }
    }

    public void AddLevelCoin()
    {
        levelCoin++;
        coinValueText.text = levelCoin.ToString();
    }

    public void RemoveLevelCoin()
    {
        levelCoin--;
        coinValueText.text = levelCoin.ToString();
    }

    public void IncreaseMainCoin(int _val)
    {
        if (_val > 0)
        {
            gameData.IncreaseTotalCoin(levelCoin);
        }
    }

    public void DecreaseMainCoin(int _val)
    {
        if (_val > 0 && (mainCoin - _val) >= 0)
        {
            gameData.DecreaseTotalCoin(_val);
        }
    }

    public int GetLevelCoinAmount() { return levelCoin; }
    #endregion

    #region Score Functions
    public void IncreaseLevelScore(int _val)
    {
        if (_val > 0)
            levelScore += _val;
        else return;
    }

    public int GetCurrentLevelScore()
    {
        if (levelScore > 0) return levelScore;
        else return 0;
    }

    #endregion

    #region Gem Functions

    #endregion
    public void ResetCoinAndScore()
    {
        levelCoin = 0;
        levelScore = 0;
    }

    private void AssignCoinText(Scene current, Scene next)
    {
        if (next.buildIndex == 0)
        {
            MM_coinValueText = GameObject.FindGameObjectWithTag("coinText").GetComponent<TextMeshProUGUI>();
            MM_coinValueText.text = gameData.GetTotalCoinValue().ToString();
            MM_gemValueText = GameObject.FindGameObjectWithTag("gemText").GetComponent<TextMeshProUGUI>();
            MM_gemValueText.text = gameData.GetGemsValue().ToString();
        }
        else
        {
            coinValueText = GameObject.FindGameObjectWithTag("coinText").GetComponent<TextMeshProUGUI>();
        }
    }
}
