using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] Transform CoinIndicatorLerpPos;
    private void Start()
    {

    }

    private void Update()
    {
        IdleTurn();
    }

    void IdleTurn()
    {
        transform.Rotate(new Vector3(0, 1, 0) * turnSpeed * Time.deltaTime);
    }

    public void CollectedStateMove()
    {
        transform.DOScale(.4f, .5f);
        transform.DOMove(CoinIndicatorLerpPos.position, .5f).OnComplete(() =>
        {
            UI_Manager.instance.CoinGained_Tween();
            gameObject.SetActive(false);
        });
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
