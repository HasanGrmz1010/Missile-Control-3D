using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile_CollideHandler : MonoBehaviour
{
    [SerializeField] PlayerMissile_Move p_move;
    [SerializeField] float JerrycanFuelValue;
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 6:// coin
                EconomyManager.instance.AddLevelCoin();
                other.gameObject.SetActive(false);
                GameManager.instance.CreateAndPlayFX("coin", other.transform.position, Quaternion.identity);
                break;

            case 7:// fuel
                FuelManager.instance.AddFuel(JerrycanFuelValue);
                other.gameObject.SetActive(false);
                GameManager.instance.CreateAndPlayFX("fuel", other.transform.position, Quaternion.identity);
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
                    p_move.EliminatedStopMovement();
                    p_move.MainCamera.GetComponent<CameraMovement>().onPlayerEliminated();

                    transform.gameObject.SetActive(false);
                    GameManager.instance.CreateAndPlayFX("explode", transform.position, Quaternion.identity);
                    GameManager.instance.playerState = GameManager.PlayerState.eliminated;
                    UI_Manager.instance.Explode_GameOverScreen();

                }
                break;

            case 9:// hammer
                if (GameManager.instance.playerState != GameManager.PlayerState.eliminated)
                {
                    p_move.EliminatedStopMovement();
                    p_move.MainCamera.GetComponent<CameraMovement>().onPlayerEliminated();

                    transform.gameObject.SetActive(false);
                    GameManager.instance.CreateAndPlayFX("explode", transform.position, Quaternion.identity);
                    GameManager.instance.playerState = GameManager.PlayerState.eliminated;
                    UI_Manager.instance.Explode_GameOverScreen();
                }
                break;

            default:

                break;
        }
    }
}
