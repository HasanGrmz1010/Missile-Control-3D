using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    public static EventManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    #endregion

    public void HandleNoFuelLeft()
    {
        GameManager.instance.gameState = GameManager.GameState.paused;
        GameManager.instance.playerState = GameManager.PlayerState.eliminated;
        UI_Manager.instance.Invoke("Fuel_GameOverScreen", 1.25f);
    }
}
