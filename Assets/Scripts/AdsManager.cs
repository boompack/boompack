using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : Singleton<AdsManager>
{
    private const string MaxSdkKey = "WZOT5TbxGDXPeAawzny3fnJ9lQ1xFRLZnkf5LQIG2tskep_Pc58uZ1AeXvA57EtTnw5VUv-4MLbgVTHRfiSUJd";
    public LevelStatsObject levelStats = new LevelStatsObject();

    [Header("Inerstitial Options")]
    [SerializeField] int interstitialCounter = 0;

    [Header("Rewarded Options")]
    [HideInInspector]
    public int numberOfSectionsToOpen = 0;
    [HideInInspector]
    public int startLevel = 0;
    public List<RewardedZones> rewardedZones=new List<RewardedZones>();

    #region Interstitial Variables
#if UNITY_IOS
            string interstitialAdUnitId = "60c957a2ebcdac7e";
            int interstitialRetryAttempt;
#elif UNITY_ANDROID
    string interstitialAdUnitId = "5fe90c4b577049be";
            int interstitialRetryAttempt;
#endif
    #endregion

    #region Rewarded Variables
#if UNITY_IOS
            string rewardedAdUnitId = "1797cffd801ab211";
            int rewardedRetryAttempt;
#elif UNITY_ANDROID
    string rewardedAdUnitId = "96f8577e41d141f5";
            int rewardedRetryAttempt;
        #endif
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
            // AppLovin SDK is initialized, configure and start loading ads.
            Debug.Log("MAX SDK Initialized");

            InitializeInterstitialAds();
            InitializeRewardedAds();
        };

        MaxSdk.SetSdkKey(MaxSdkKey);
        MaxSdk.InitializeSdk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRewardedAd(int currentLevel)
    {
        ////var level=0;
        //foreach (RewardedZones rewardedZones in rewardedZones)
        //{
        //    if (currentLevel == rewardedZones.startLevel)
        //    {
                //numberOfSectionsToOpen = rewardedZones.numberOfSectionsToOpen;
                if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
                {
                    MaxSdk.ShowRewardedAd(rewardedAdUnitId);
                }
        //    }
        //}



    }

    public void ShowInterstitialAd(int i)
    {
        if(i==interstitialCounter-1)
        {
            if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
            {
                MaxSdk.ShowInterstitial(interstitialAdUnitId);
            }

        }

    }

    #region AppLovin Interstitial

        public void InitializeInterstitialAds()
        {
            // Attach callback
            MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

            // Load the first interstitial
            LoadInterstitial();
        }

        private void LoadInterstitial()
        {
            MaxSdk.LoadInterstitial(interstitialAdUnitId);
        }

        private void OnInterstitialLoadedEvent(string adUnitId)
        {
            // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

            // Reset retry attempt
            interstitialRetryAttempt = 0;
        }

        private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
        {
            // Interstitial ad failed to load 
            // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

            interstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

            Invoke("LoadInterstitial", (float)retryDelay);
        }

        private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
        {
            // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
            LoadInterstitial();
        }

        private void OnInterstitialDismissedEvent(string adUnitId)
        {
            // Interstitial ad is hidden. Pre-load the next ad.
            LoadInterstitial();
        }

    #endregion

    #region AppLovin Rewarded
        public void InitializeRewardedAds()
        {
            // Attach callback
            MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
            MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

            // Load the first rewarded ad
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            MaxSdk.LoadRewardedAd(rewardedAdUnitId);
        }

        private void OnRewardedAdLoadedEvent(string adUnitId)
        {
            // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

            // Reset retry attempt
            rewardedRetryAttempt = 0;
        }

        private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
        {
            // Rewarded ad failed to load 
            // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

            rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

            Invoke("LoadRewardedAd", (float)retryDelay);
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
        {
            // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
            LoadRewardedAd();
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId) { }

        private void OnRewardedAdClickedEvent(string adUnitId) { }

        private void OnRewardedAdDismissedEvent(string adUnitId)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            LoadRewardedAd();
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
        {
        Debug.Log("ödül verildi");
            SaveManager.Instance.OpenRewardedLevels();
        UIManager.Instance.rewardedPanelMessage.text = "Your next " + AdsManager.Instance.numberOfSectionsToOpen + " levels have been unlocked.";
        UIManager.Instance.rewardedButtonMessage.text = "Levels Screen";
        UIManager.Instance.rewardAdZone.SetActive(false);
        //DoozyManager.Instance.SendGameEvent("OpenedReward");

    }
    #endregion
}


[System.Serializable]
public class RewardedZones
{
    public int startLevel;
    public int numberOfSectionsToOpen;
}