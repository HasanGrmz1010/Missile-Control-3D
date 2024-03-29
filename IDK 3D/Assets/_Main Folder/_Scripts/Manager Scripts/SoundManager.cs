using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] SettingsData_SO setData;
    [SerializeField] Button music_toggle;
    [SerializeField] Toggle fx_toggle;

    [SerializeField] AudioSource RocketSource;
    [SerializeField] AudioSource EffectSource;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource LevelStatusSource;
    [SerializeField] AudioSource TaptapSource;

    [SerializeField] AudioClip mainMenuMusic;

    [Header("----------- ROCKET SOUNDS -----------")]
    [SerializeField] AudioClip rocketBooster;
    [SerializeField] AudioClip rocketFreeFall;
    [SerializeField] AudioClip rocketExplode;
    [Header("----------- EFFECT SOUNDS -----------")]
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip fuelSound;
    [SerializeField] AudioClip fuelBlastSound;
    [SerializeField] AudioClip buttonPressed;
    [SerializeField] AudioClip buttonAlternative;
    [SerializeField] AudioClip popper;
    [SerializeField] AudioClip coinCollected;
    [SerializeField] AudioClip taptap_pop;
    [SerializeField] AudioClip cha_ching;
    [Header("----------- LEVEL STATUS SOUNDS -----------")]
    [SerializeField] AudioClip levelPassed;
    [SerializeField] AudioClip levelFailed;

    private void Start()
    {
        SceneManager.activeSceneChanged += CheckMusicSound;
        if (!setData.Toggle_Music) MusicSource.volume = 0f;
        MusicSource.Play();
    }

    public void PlaySoundFX(string clip, float volume)
    {
        if (clip != null)
        {
            switch (clip)
            {
                case "coin":
                    EffectSource.pitch = 1f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(coinSound);
                    break;

                case "fuel":
                    EffectSource.pitch = 1f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(fuelSound);
                    break;

                case "fuel_blast":
                    EffectSource.pitch = 1f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(fuelBlastSound);
                    break;

                case "button":
                    EffectSource.pitch = .9f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(buttonPressed);
                    break;

                case "button_a":
                    EffectSource.pitch = 1f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(buttonAlternative);
                    break;

                case "collect":
                    EffectSource.pitch = 1f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(coinCollected);
                    break;

                case "taptap":
                    TaptapSource.volume = volume;
                    TaptapSource.pitch += 0.025f;
                    TaptapSource.PlayOneShot(taptap_pop);
                    break;

                case "popper":
                    EffectSource.pitch = 1f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(popper);
                    break;

                case "passed":
                    LevelStatusSource.volume = volume;
                    LevelStatusSource.PlayOneShot(levelPassed);
                    break;

                case "failed":
                    LevelStatusSource.volume = volume;
                    LevelStatusSource.PlayOneShot(levelFailed);
                    break;

                case "ching":
                    EffectSource.pitch = 1f;
                    EffectSource.volume = volume;
                    EffectSource.PlayOneShot(cha_ching);
                    break;
                default:
                    break;
            }
        }
        else return;
    }

    #region Rocket Sound Functions
    public void ExplodeSoundFX()
    {
        RocketSource.loop = false;
        RocketSource.Stop();
        RocketSource.volume = .2f;
        RocketSource.clip = rocketExplode;
        RocketSource.Play();
    }

    public void FinalStageSoundFX()
    {
        RocketSource.clip = rocketBooster;
        RocketSource.volume = .9f;
        RocketSource.DOPitch(1.5f, 5f);
    }

    public void FinalTargetSoundFX()
    {
        RocketSource.DOPitch(2.5f, 1f).SetEase(Ease.Linear);
    }

    public void OpenRocketSoundVolume()
    {
        RocketSource.volume = 0f;
        RocketSource.Play();
    }

    public void MuteRocketSound()
    {
        RocketSource.volume = 0f;
    }

    public void PlayRocketSoundFX_Boost()
    {
        RocketSource.clip = rocketBooster;
        RocketSource.DOPitch(1f, .2f);
        RocketSource.DOFade(.75f, .2f);
        RocketSource.Play();
    }

    public void PlayRocketSoundFX_Fall()
    {
        RocketSource.clip = rocketBooster;
        RocketSource.DOPitch(.7f, .2f);
        RocketSource.DOFade(.5f, .2f);
        RocketSource.Play();
    }
    #endregion

    #region Effect Sound Functions
    public void MuteEffectSource()
    {
        EffectSource.volume = 0f;
    }

    public void ResetTaptapPitch()
    {
        TaptapSource.pitch = 1f;
    }
    #endregion

    #region Music Sound Functions
    public void ChangeVolume_Music(float _val)
    {
        if (!setData.Toggle_Music)
        {
            return;
        }
        else
        {
            if (_val >= 0f)
            {
                MusicSource.DOFade(_val, .25f);
            }
        }
    }

    void CheckMusicSound(Scene current, Scene next)
    {
        if (current.buildIndex == 0)
        {
            if (!setData.Toggle_Music)
            {
                ChangeVolume_Music(0f);
            }
        }
    }
    #endregion

    #region Level Status Functions
    public void LevelPassedSoundFX()
    {
        PlaySoundFX("passed", 1f);
    }

    public void LevelFailedSoundFX()
    {
        PlaySoundFX("failed", 1f);
    }
    #endregion

}
