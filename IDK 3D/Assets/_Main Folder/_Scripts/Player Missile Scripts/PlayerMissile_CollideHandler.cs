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
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 6:// coin
                other.GetComponent<Coin>().CollectedStateMove();
                //UI_Manager.instance.CoinGained_Tween();
                EconomyManager.instance.AddLevelCoin();
                GameManager.instance.CreateAndPlayFX("coin", other.transform.position, Quaternion.identity);
                break;

            case 7:// fuel
                UI_Manager.instance.FuelGained_Tween();
                FuelManager.instance.AddFuel(JerrycanFuelValue);
                GameManager.instance.CreateAndPlayFX("fuel", other.transform.position, Quaternion.identity);
                other.gameObject.SetActive(false);
                break;

            case 10:// finish line
                if (GameManager.instance.playerState != GameManager.PlayerState.eliminated)
                {
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
                if (GameManager.instance.playerState != GameManager.PlayerState.eliminated)
                {
                    Time.timeScale = .5f;
                    DOTween.To(() => p_move.MainCamera.fieldOfView, set => p_move.MainCamera.fieldOfView = set, 50f, 1f).SetEase(Ease.OutQuad);
                    p_move.MainCamera.GetComponent<CameraMovement>().onPlayerEliminated();
                    p_move.EliminatedStopMovement();

                    transform.gameObject.SetActive(false);
                    GameManager.instance.CreateAndPlayFX("explode", transform.position, Quaternion.identity);
                    GameManager.instance.playerState = GameManager.PlayerState.eliminated;
                    UI_Manager.instance.Handle_Explode_GameOverScreen();

                }
                break;

            case 9:// hammer
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
                break;

            case 12:// last target
                Time.timeScale = .5f;
                DOTween.To(() => p_move.MainCamera.fieldOfView, set => p_move.MainCamera.fieldOfView = set, 50f, 1f).SetEase(Ease.OutQuad);

                transform.gameObject.SetActive(false);
                GameManager.instance.SetMultiplierValue(taptap.GetTaptapMultiplier());
                GameManager.instance.playerState = GameManager.PlayerState.win;
                GameManager.instance.CreateAndPlayFX("explode", transform.position, Quaternion.identity);


                UI_Manager.instance.Handle_LevelPassedScreen();
                break;
            default:

                break;
        }
    }
}
