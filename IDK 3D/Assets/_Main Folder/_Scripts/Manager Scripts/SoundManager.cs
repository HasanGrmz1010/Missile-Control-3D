using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    [SerializeField] AudioSource RocketSource;
    [SerializeField] AudioSource EffectSource;
    [SerializeField] AudioSource MusicSource;

    [SerializeField] AudioClip mainMenuMusic;

    [Header("----------- ROCKET SOUNDS -----------")]
    [SerializeField] AudioClip rocketBooster;
    [SerializeField] AudioClip rocketFreeFall;
    [Header("----------- COIN SOUNDS -----------")]
    [SerializeField] AudioClip coinSound;
    [Header("----------- FUEL SOUNDS -----------")]
    [SerializeField] AudioClip fuelSound;
    [SerializeField] AudioClip fuelBlastSound;

    private void Start()
    {

    }

    void PlaySoundFX(string clip, float volume)
    {
        if (clip != null)
        {
            switch (clip)
            {
                case "coin":
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(coinSound);
                    break;

                case "fuel":
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(fuelSound);
                    break;

                case "fuel_blast":
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(fuelBlastSound);
                    break;

                default:
                    break;
            }
        }
        else return;
    }

    public void PlayCoinSoundFX()
    {
        PlaySoundFX("coin", 1f);
    }

    public void PlayFuelSoundFX()
    {
        PlaySoundFX("fuel", 1f);
        PlaySoundFX("fuel_blast", .1f);
    }

}
