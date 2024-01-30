using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] RewardedAd rewardedAdButton;

    void Awake()
    {
        DontDestroyOnLoad(this);
        InitializeAds();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += onSceneChanged;
        //rewardedAdButton.LoadAd();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        rewardedAdButton.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    void onSceneChanged(Scene current, Scene next)
    {
        if (next.buildIndex > 0)
        {
            RewardedAd rewardButton = GameObject.FindGameObjectWithTag("reward_AD").GetComponent<RewardedAd>();
            rewardButton.LoadAd();
        }
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= onSceneChanged;
    }
}