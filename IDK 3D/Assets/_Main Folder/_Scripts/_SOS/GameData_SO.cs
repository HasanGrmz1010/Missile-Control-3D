using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGameData", menuName = "Game Data SO")]
public class GameData_SO : ScriptableObject
{
    [SerializeField] private int GAME_LEVEL;
    [SerializeField] private int HIGH_SCORE;

    [SerializeField] private int TOTAL_COIN;

    public int GetGameLevel() { return GAME_LEVEL; }
    public void IncreaseGameLevel() { GAME_LEVEL++; }
    public int GetHighScore() { return HIGH_SCORE; }
    public void ChangeHighScore(int _val)
    {
        if (_val > 0 && _val > HIGH_SCORE)
            HIGH_SCORE = _val;
        else return;
    }
}
