using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        }
    }
    #endregion

    [SerializeField] GameData_SO gameData;

    [Header(" ======== Level Coin Text ========")]
    [SerializeField] TextMeshProUGUI coinValueText;

    [SerializeField] private int mainCoin;
    [SerializeField] private int levelCoin;
    [SerializeField] private int levelScore;

    private void Start()
    {
        levelCoin = 0;
        levelScore = 0;
    }

    #region Coin Functions
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

}
