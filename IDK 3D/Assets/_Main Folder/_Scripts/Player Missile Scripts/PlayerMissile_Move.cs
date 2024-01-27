using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMissile_Move : MonoBehaviour
{
    [SerializeField] ParticleSystem MissileUp_FX;
    [SerializeField] ParticleSystem IdleSmoke_FX;
    public Camera MainCamera;

    public bool ableToMove;
    bool hasTouched;
    bool inFinishPhase;

    Rigidbody rb;
    Quaternion moveRotation;
    Vector3 MoveVec;

    [Header("------------- Rotation Angle Values -------------")]
    [SerializeField] float upAngle;
    [SerializeField] float downAngle;
    [SerializeField] float straightAngle;
    [Header("------------- Speed Values -------------")]
    [SerializeField] float missileSpeed;
    [SerializeField] float EndPhaseMissileSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fuelSpendValue;

    [Header("//////// Level Values ////////")]
    [SerializeField] float LevelEnd_Y_Value;

    // -------------- EVENTS --------------
    public static event Action onNoFuelLeft;

    private void Start()
    {
        MainCamera = Camera.main;
        // Event Subscribtions
        onNoFuelLeft += EventManager.instance.HandleNoFuelLeft;
        onNoFuelLeft += MainCamera.transform.GetComponent<CameraMovement>().onPlayerEliminated;

        // Sound
        SoundManager.instance.OpenRocketSoundVolume();
        SoundManager.instance.MuteEffectSource();

        ableToMove = false; hasTouched = false; inFinishPhase = false;
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        HandleMovement();

        if (!inFinishPhase)
        {
            if (Input.touchCount > 0 && GameManager.instance.playerState != GameManager.PlayerState.eliminated && ableToMove)
            {
                Touch touch = Input.GetTouch(0);

                if (GameManager.instance.gameState != GameManager.GameState.endPhase)
                {
                    FuelManager.instance.SpendFuel(fuelSpendValue);
                }

                if (GameManager.instance.gameState == GameManager.GameState.startPhase)
                {
                    GameManager.instance.gameState = GameManager.GameState.playing;
                }

                // -------------- Rocket Functions & Handling Fuel --------------

                if (touch.phase == TouchPhase.Began && FuelManager.instance.hasFuel())
                {
                    StartRocketFunctions();
                    SoundManager.instance.PlayRocketSoundFX_Boost();
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    StopRocketFunctions();
                    SoundManager.instance.PlayRocketSoundFX_Fall();
                }

                else if (!FuelManager.instance.hasFuel() && ableToMove)
                {
                    StopRocketFunctions();
                    onNoFuelLeft?.Invoke();
                    ableToMove = false;
                }
            }
        }
    }

    void HandleMovement()
    {
        if (hasTouched)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, rotationSpeed * Time.deltaTime);
            transform.position += MoveVec * missileSpeed * Time.deltaTime;
        }
    }

    public void SetFinishPhaseValues()
    {
        transform.DOMoveY(LevelEnd_Y_Value, .25f);
        straightAngle = -90f;
        missileSpeed = EndPhaseMissileSpeed;

        moveRotation = Quaternion.Euler(0, 0, straightAngle);
        MoveVec = new Vector3(1f, 0f, 0f);

        inFinishPhase = true;

        MissileUp_FX.Play();
        IdleSmoke_FX.Stop();
    }

    public void SetTargetPhaseValues()
    {
        missileSpeed = EndPhaseMissileSpeed * 2;
    }

    void StartRocketFunctions()
    {
        if (GameManager.instance.gameState != GameManager.GameState.endPhase)
        {
            MissileUp_FX.Play();
            IdleSmoke_FX.Stop();

            hasTouched = true;
            rb.isKinematic = false;
            moveRotation = Quaternion.Euler(0, 0, upAngle);
            MoveVec = new Vector3(1f, 1f, 0);
        }
    }

    void StopRocketFunctions()
    {
        if (GameManager.instance.gameState != GameManager.GameState.endPhase)
        {
            MissileUp_FX.Stop();
            IdleSmoke_FX.Play();

            MoveVec = new Vector3(1f, -.75f, 0);
            moveRotation = Quaternion.Euler(0, 0, downAngle);
        }
    }

    public void EliminatedStopMovement()
    {
        MissileUp_FX.Stop();
        rb.isKinematic = true;
        missileSpeed = 0f;
        rotationSpeed = 0f;
    }

    public void AssingUpParticleFX(ParticleSystem particle)
    {
        if (particle != null)
        {
            MissileUp_FX = particle;
            MissileUp_FX.Stop();
        }
        else return;
    }

    private void OnDisable()
    {
        onNoFuelLeft -= EventManager.instance.HandleNoFuelLeft;
    }
}
