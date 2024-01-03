using System.Collections;
using System.Collections.Generic;
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
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    [SerializeField] private int mainCoin;
    [SerializeField] int levelCoin;

    private void Start()
    {
        levelCoin = 0;
    }

    public void AddLevelCoin()
    {
        levelCoin++;
    }

    public void RemoveLevelCoin()
    {
        levelCoin--;
    }

    public void IncreaseMainCoin(int _val)
    {
        if (_val > 0)
        {
            mainCoin += _val;
        }
    }

    public void DecreaseMainCoin(int _val)
    {
        if (_val > 0 && (mainCoin - _val) >= 0)
        {
            mainCoin -= _val;
        }
    }
}
