using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    #endregion

    int TapTap_Multiplier;

    [Header("_____________ GAME DATA _____________")]
    [SerializeField] GameData_SO GameData;

    [Header("_____________ PARTICLE FX _____________")]
    [SerializeField] ParticleSystem coinCollected_FX;
    [SerializeField] ParticleSystem fuelCollected_FX;
    [SerializeField] ParticleSystem missileExplode_FX;

    #region STATE ENUMS
    public enum GameState
    {
        startPhase, playing, paused, endPhase
    }
    public GameState gameState = new GameState();

    public enum PlayerState
    {
        ableToPlay, eliminated, win
    }
    public PlayerState playerState = new PlayerState();
    #endregion


    private void Start()
    {
        if (GameData != null)
        {
            int lastLevel = GameData.GetGameLevel();
        }
        gameState = GameState.startPhase;
        playerState = PlayerState.ableToPlay;
    }

    #region FX Functions
    public void CreateAndPlayFX(string _tag, Vector3 pos, Quaternion qua)
    {
        ParticleSystem selectedFX = null;
        float explodeFX_duration = 0f;
        switch (_tag)
        {
            case "coin":
                selectedFX = Instantiate(coinCollected_FX, pos, qua);
                break;

            case "fuel":
                selectedFX = Instantiate(fuelCollected_FX, pos, qua);
                break;

            case "explode":
                selectedFX = Instantiate(missileExplode_FX, pos, qua);
                explodeFX_duration = 8f;
                break;
            default:
                break;
        }

        if (selectedFX != null && explodeFX_duration == 0f)
        {
            selectedFX.gameObject.SetActive(true);
            selectedFX.Play();
            float duration = selectedFX.main.duration;
            StartCoroutine(DeactivateFX(selectedFX, duration));
        }
        else if (selectedFX != null && explodeFX_duration != 0f)
        {
            selectedFX.gameObject.SetActive(true);
            selectedFX.Play();
            StartCoroutine(DeactivateFX(selectedFX, explodeFX_duration));
        }
    }

    IEnumerator DeactivateFX(ParticleSystem fx, float duration)
    {
        yield return new WaitForSeconds(duration);
        fx.gameObject.SetActive(false);
    }
    #endregion

    public void SetMultiplierValue(int _val)
    {
        if (_val > 0) TapTap_Multiplier = _val;
    }

    public int GetMultiplierValue()
    {
        return TapTap_Multiplier;
    }
}
