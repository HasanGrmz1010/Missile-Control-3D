using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    float turnSpeed;
    [SerializeField] Transform BladeTransform;

    private void Start()
    {
        turnSpeed = -750f;
    }

    private void Update()
    {
        IdleTurn();
    }

    void IdleTurn()
    {
        BladeTransform.Rotate(new Vector3(0, 0, 1) * turnSpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
