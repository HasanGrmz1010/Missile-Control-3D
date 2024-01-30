using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile_CollideHandler : MonoBehaviour
{
    [SerializeField] TaptapHandler taptap;

    [SerializeField] PlayerMissile_Move p_move;
    [SerializeField] float JerrycanFuelValue;
    Rigidbody rb;
    private void Start()
    {
        taptap = GameObject.FindGameObjectWithTag("taptap").GetComponent<TaptapHandler>();
        p_move = transform.GetComponent<PlayerMissile_Move>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 6:// coin
                SoundManager.instance.PlaySoundFX("coin", 1f);
                other.GetComponent<Coin>().CollectedStateMove();
                EconomyManager.instance.IncreaseLevelScore(10);
                GameManager.instance.CreateAndPlayFX("coin", other.transform.position, Quaternion.identity);
                break;

            case 7:// fuel
                SoundManager.instance.PlaySoundFX("fuel", 1f);
                SoundManager.instance.PlaySoundFX("fuel_blast", .1f);
                UI_Manager.instance.FuelGained_Tween();
                FuelManager.instance.AddFuel(JerrycanFuelValue);
                EconomyManager.instance.IncreaseLevelScore(5);
                GameManager.instance.CreateAndPlayFX("fuel", other.transform.position, Quaternion.identity);
                other.gameObject.SetActive(false);
                break;

            case 10:// finish line
                if (GameManager.instance.playerState != GameManager.PlayerState.eliminated)
                {
                    taptap.ChangeFinalStatus();
                    SoundManager.instance.FinalStageSoundFX();
                    SoundManager.instance.ChangeVolume_Music(.05f);
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    GameManager.instance.gameState = GameManager.GameState.endPhase;

                    // Handle Camera follow offset and POV
                    p_move.MainCamera.transform.GetComponent<CameraMovement>().SetOffset(new Vector3(14, -20, 24));
                    DOTween.To(() => p_move.MainCamera.fieldOfView, set => p_move.MainCamera.fieldOfView = set, 20f, 1f).SetEase(Ease.OutQuad);

                    p_move.SetFinishPhaseValues();
                    UI_Manager.instance.TapTapPhase_HandleUI();
                }
                break;

            case 11:// target line
                if (GameManager.instance.playerState != GameManager.PlayerState.eliminated)
                {
                    taptap.ChangeFinalStatus();
                    SoundManager.instance.FinalTargetSoundFX();
                    DOTween.To(() => p_move.MainCamera.fieldOfView, set => p_move.MainCamera.fieldOfView = set, 40f, 1f).SetEase(Ease.OutQuad);
                    p_move.SetTargetPhaseValues();
                }
                break;
            
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.layer)
        {
            case 8:// borders
                SoundManager.instance.ExplodeSoundFX();
                HandlePlayerExploded();
                break;

            case 9:// hammer
                SoundManager.instance.ExplodeSoundFX();
                HandlePlayerExploded();
                break;

            case 12:// last target
                Time.timeScale = .5f;
                DOTween.To(() => p_move.MainCamera.fieldOfView, set => p_move.MainCamera.fieldOfView = set, 50f, 1f).SetEase(Ease.OutQuad);

                transform.gameObject.SetActive(false);
                GameManager.instance.SetMultiplierValue(taptap.GetTaptapMultiplier());
                GameManager.instance.playerState = GameManager.PlayerState.win;
                GameManager.instance.CreateAndPlayFX("explode", transform.position, Quaternion.identity);
                SoundManager.instance.ExplodeSoundFX();


                UI_Manager.instance.Handle_LevelPassedScreen();
                break;

            case 13:// blade
                SoundManager.instance.ExplodeSoundFX();
                HandlePlayerExploded();
                break;
            default:

                break;
        }
    }

    public void HandlePlayerExploded()
    {
        Time.timeScale = .5f;
        DOTween.To(() => p_move.MainCamera.fieldOfView, set => p_move.MainCamera.fieldOfView = set, 50f, 1f).SetEase(Ease.OutQuad);
        p_move.MainCamera.GetComponent<CameraMovement>().onPlayerEliminated();
        p_move.EliminatedStopMovement();

        transform.gameObject.SetActive(false);
        GameManager.instance.CreateAndPlayFX("explode", transform.position, Quaternion.identity);

        if (GameManager.instance.playerState != GameManager.PlayerState.eliminated)
        {
            UI_Manager.instance.Handle_Explode_GameOverScreen();
            GameManager.instance.playerState = GameManager.PlayerState.eliminated;
        }
    }
}
