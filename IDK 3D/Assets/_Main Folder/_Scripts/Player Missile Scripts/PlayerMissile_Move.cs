using DG.Tweening;
using System;
using UnityEngine;

public class PlayerMissile_Move : MonoBehaviour
{
    [SerializeField] ParticleSystem MissileUp_FX;
    [SerializeField] ParticleSystem IdleSmoke_FX;
    public Camera MainCamera;

    public bool ableToMove;
    bool hasTouched;
    bool inFinishPhase;
    bool isBoosting;

    EventManager fuelEventManager;
    CameraMovement cameraMovement;

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
        fuelEventManager = EventManager.instance;
        cameraMovement = MainCamera.GetComponent<CameraMovement>();
        onNoFuelLeft += fuelEventManager.HandleNoFuelLeft;
        onNoFuelLeft += cameraMovement.onPlayerEliminated;

        // Sound
        SoundManager.instance.OpenRocketSoundVolume();
        SoundManager.instance.MuteEffectSource();

        ableToMove = false; hasTouched = false; inFinishPhase = false; isBoosting = false;
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        HandleMovement();

        if (inFinishPhase || !ableToMove ||
            GameManager.instance.playerState != GameManager.PlayerState.ableToPlay ||
            GameManager.instance.gameState == GameManager.GameState.paused)
        {
            return;
        }

        // Release can be missed when a touch is interrupted by the OS.
        if (Input.touchCount == 0)
        {
            if (isBoosting)
            {
                StopRocketFunctions();
                SoundManager.instance.PlayRocketSoundFX_Fall();
            }
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (GameManager.instance.gameState == GameManager.GameState.startPhase)
        {
            GameManager.instance.gameState = GameManager.GameState.playing;
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            if (isBoosting)
            {
                StopRocketFunctions();
                SoundManager.instance.PlayRocketSoundFX_Fall();
            }
            return;
        }

        if (touch.phase == TouchPhase.Began && FuelManager.instance.hasFuel())
        {
            StartRocketFunctions();
            SoundManager.instance.PlayRocketSoundFX_Boost();
        }

        if (isBoosting)
        {
            // Prefabs store the original per-frame value; 60 Hz preserves their tuning.
            FuelManager.instance.SpendFuel(fuelSpendValue * 60f * Time.deltaTime);
            if (!FuelManager.instance.hasFuel())
            {
                StopRocketFunctions();
                ableToMove = false;
                onNoFuelLeft?.Invoke();
            }
        }
    }

    void HandleMovement()
    {
        if (hasTouched && GameManager.instance.gameState != GameManager.GameState.paused &&
            GameManager.instance.playerState == GameManager.PlayerState.ableToPlay)
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
        isBoosting = false;

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
            isBoosting = true;
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
            isBoosting = false;

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
        if (fuelEventManager != null)
            onNoFuelLeft -= fuelEventManager.HandleNoFuelLeft;
        if (cameraMovement != null)
            onNoFuelLeft -= cameraMovement.onPlayerEliminated;
    }
}
