using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed;
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
}
