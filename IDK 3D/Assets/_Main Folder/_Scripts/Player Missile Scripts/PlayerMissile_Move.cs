using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMissile_Move : MonoBehaviour
{
    [SerializeField] ParticleSystem MissileUp_FX;
    [SerializeField] ParticleSystem IdleSmoke_FX;
    public GameObject MainCamera;

    bool ableToMove, touched;

    Rigidbody rb;
    Quaternion moveRotation;
    Vector3 MoveVec;

    [SerializeField] float upAngle, downAngle;
    [SerializeField] float missileSpeed, rotationSpeed;
    [SerializeField] float fuelSpendValue;

    // -------------- EVENTS --------------
    public static event Action onNoFuelLeft;

    private void Start()
    {
        onNoFuelLeft += EventManager.instance.HandleNoFuelLeft;
        onNoFuelLeft += MainCamera.GetComponent<CameraMovement>().onPlayerEliminated;
        ableToMove = true; touched = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && GameManager.instance.playerState != GameManager.PlayerState.eliminated)
        {
            Touch touch = Input.GetTouch(0);
            FuelManager.instance.SpendFuel(fuelSpendValue);

            if (touch.phase == TouchPhase.Began && FuelManager.instance.hasFuel())
            {
                StartRocketFunctions();
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                StopRocketFunctions();
            }

            else if (!FuelManager.instance.hasFuel() && ableToMove)
            {
                StopRocketFunctions();
                onNoFuelLeft?.Invoke();
                ableToMove = false;
            }
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (touched)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, rotationSpeed * Time.deltaTime);
            transform.position += MoveVec * missileSpeed * Time.deltaTime;
        }
    }

    void StartRocketFunctions()
    {
        MissileUp_FX.Play();
        IdleSmoke_FX.Stop();
        touched = true;
        rb.isKinematic = false;
        moveRotation = Quaternion.Euler(0, 0, upAngle);
        MoveVec = new Vector3(1f, 1f, 0);
    }

    void StopRocketFunctions()
    {
        MissileUp_FX.Stop();
        IdleSmoke_FX.Play();
        MoveVec = new Vector3(1f, -.75f, 0);
        moveRotation = Quaternion.Euler(0, 0, downAngle);
    }

    public void EliminatedStopMovement()
    {
        MissileUp_FX.Stop();
        rb.isKinematic = true;
        missileSpeed = 0f;
        rotationSpeed = 0f;
    }

    private void OnDisable()
    {
        onNoFuelLeft -= EventManager.instance.HandleNoFuelLeft;
    }
}
