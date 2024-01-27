using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGameData", menuName = "Game Data SO")]
public class GameData_SO : ScriptableObject
{
    [SerializeField] private int GAME_LEVEL;
    [SerializeField] private int HIGH_SCORE;

    [SerializeField] private int TOTAL_COIN;
    [SerializeField] private int GEMS;

    [SerializeField] GameObject missileObject;
    [SerializeField] ParticleSystem upMissileFX;

    public int GetGameLevel() { return GAME_LEVEL; }
    public void IncreaseGameLevel() { GAME_LEVEL++; }

    public int GetHighScore() { return HIGH_SCORE; }
    public void ChangeHighScore(int _val)
    {
        if (_val > 0 && _val > HIGH_SCORE)
            HIGH_SCORE = _val;
        else return;
    }

    public void IncreaseTotalCoin(int _val) { if (_val > 0) TOTAL_COIN += _val; }
    public void DecreaseTotalCoin(int _val) { if (_val > 0 && (TOTAL_COIN - _val) >= 0) TOTAL_COIN -= _val; }
    public int GetTotalCoinValue() { return TOTAL_COIN; }

    public void IncreaseGems(int _val) { if (_val > 0) GEMS += _val; }
    public void DecreaseGems(int _val) { if (_val > 0 && (GEMS - _val) >= 0) GEMS -= _val; }
    public int GetGemsValue() { return GEMS; }

    public GameObject GetCurrentMissileObject()
    {
        if (missileObject != null)
            return missileObject;
        else return null;
    }

    public void SetCurrentMissileObject(GameObject _val)
    {
        if (_val != null) missileObject = _val;
    }

    public ParticleSystem GetUpMissileFX()
    {
        return upMissileFX;
        //if (upMissileFX != null) return upMissileFX;
        //else return null;
    }

    public void SetUpMissileFX(ParticleSystem fx)
    {
        if (fx != null) upMissileFX = fx;
    }

}
