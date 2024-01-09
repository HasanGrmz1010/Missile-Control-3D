using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    #region Singleton
    public static ButtonManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    #endregion

    public void GameOver_GiveUp()
    {

    }

    public void GameOver_TryAgain()
    {

    }

    public void DoublePrize_Button()
    {

    }

    public void LevelPassed_Continue()
    {

    }

    public void LevelPassed_MainMenu()
    {

    }
}
