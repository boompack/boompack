using GameAnalyticsSDK;
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
            string rewardedOpenLevelsAdUnitId = "1797cffd801ab211";
            string rewardedBonusBallsAdUnitId = "f45ec4c3bfbdac91";
            int rewardedRetryAttempt;
#elif UNITY_ANDROID
    string rewardedOpenLevelsAdUnitId = "96f8577e41d141f5";
    string rewardedBonusBallsAdUnitId = "b6daed938a421669";
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

    public void ShowOpenLevelsRewardedAd(int currentLevel)
    {
        ////var level=0;
        //foreach (RewardedZones rewardedZones in rewardedZones)
        //{
        //    if (currentLevel == rewardedZones.startLevel)
        //    {
                //numberOfSectionsToOpen = rewardedZones.numberOfSectionsToOpen;
                if (MaxSdk.IsRewardedAdReady(rewardedOpenLevelsAdUnitId))
                {
            //GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "MAX", "OpenLevels");

            MaxSdk.ShowRewardedAd(rewardedOpenLevelsAdUnitId);

            SaveManager.Instance.OpenRewardedLevels();
            SaveManager.Instance.SaveLevelStats();
            PlayerPrefs.Save();
            GameManager.Instance.SavePrefs();
            SaveManager.Instance.LoadLevelStats();

            UIManager.Instance.rewardedPanelMessage.text = "Your next " + AdsManager.Instance.numberOfSectionsToOpen + " levels have been unlocked.";
            UIManager.Instance.rewardedButtonMessage.text = "Levels Screen";
            UIManager.Instance.rewardAdZone.SetActive(false);
            //DoozyManager.Instance.SendGameEvent("OpenedReward");

            if (GameManager.Instance.levelLoader.loadedLevel.levelID == 300)
            {
                DoozyManager.Instance.SendGameEvent("LastGame");
            }
            else if ((GameManager.Instance.levelLoader.loadedLevel.levelID % 20) == 0)
            {
                DoozyManager.Instance.SendGameEvent("LastEpisode");
            }

            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "MAX", "OpenLevels");
        }
        //    }
        //}


    }

    public void ShowBonusBallsRewardedAd()
    {

        if (MaxSdk.IsRewardedAdReady(rewardedBonusBallsAdUnitId))
        {

                //GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "MAX", "BonusBall");
            MaxSdk.ShowRewardedAd(rewardedBonusBallsAdUnitId);
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "MAX", "BonusBall");
            Debug.Log(PlayerPrefs.GetInt("BonusBalloon1Count"));
            PlayerPrefs.SetInt("BonusBalloon1Count", PlayerPrefs.GetInt("BonusBalloon1Count") + 1);
            GameManager.Instance.bonusBalloon1Count = PlayerPrefs.GetInt("BonusBalloon1Count");

            Debug.Log(PlayerPrefs.GetInt("BonusBalloon1Count"));
            PlayerPrefs.Save();
            BonusBalloonUIManager.Instance.RefleshCounts();
        }

    }

    public void ShowInterstitialAd(int i)
    {
        if(i>0 && (i+1)%interstitialCounter==0 && PlayerPrefs.GetInt("PREMIUM") != 1)
        {
            if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
            {
                GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "MAX", "Interstitial");

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

            GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "MAX", "Interstitial");

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
        LoadBonusBallsRewardedAd();
        LoadOpenLevelsRewardedAd();
        }

        private void LoadOpenLevelsRewardedAd()
        {
            MaxSdk.LoadRewardedAd(rewardedOpenLevelsAdUnitId);
        }

    private void LoadBonusBallsRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardedBonusBallsAdUnitId);
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
        //if (adUnitId == rewardedOpenLevelsAdUnitId)
        //{
        //    GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, "MAX", "OpenLevels");
        //}
        LoadOpenLevelsRewardedAd();
        LoadBonusBallsRewardedAd();
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId) 
        {
            //if (adUnitId == rewardedOpenLevelsAdUnitId)
            //{
            //    GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "MAX", "OpenLevels");
            //}
        }

        private void OnRewardedAdClickedEvent(string adUnitId) { }

        private void OnRewardedAdDismissedEvent(string adUnitId)
        {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadOpenLevelsRewardedAd();
        LoadBonusBallsRewardedAd();

    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
        {

            if(adUnitId==rewardedOpenLevelsAdUnitId)
            {

            //    SaveManager.Instance.OpenRewardedLevels();
            //    SaveManager.Instance.SaveLevelStats();
            //    PlayerPrefs.Save();
            //    GameManager.Instance.SavePrefs();
            //    SaveManager.Instance.LoadLevelStats();

            //UIManager.Instance.rewardedPanelMessage.text = "Your next " + AdsManager.Instance.numberOfSectionsToOpen + " levels have been unlocked.";
            //    UIManager.Instance.rewardedButtonMessage.text = "Levels Screen";
            //    UIManager.Instance.rewardAdZone.SetActive(false);
            //    //DoozyManager.Instance.SendGameEvent("OpenedReward");

            //    if (GameManager.Instance.levelLoader.loadedLevel.levelID == 300)
            //    {
            //        DoozyManager.Instance.SendGameEvent("LastGame");
            //    }
            //    else if ((GameManager.Instance.levelLoader.loadedLevel.levelID % 20) == 0)
            //    {
            //        DoozyManager.Instance.SendGameEvent("LastEpisode");
            //    }

            //    GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "MAX", "OpenLevels");

        }
        else if (adUnitId==rewardedBonusBallsAdUnitId)
            {
            //    GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "MAX", "BonusBall");
            //    Debug.Log(PlayerPrefs.GetInt("BonusBalloon1Count"));
            //    PlayerPrefs.SetInt("BonusBalloon1Count", PlayerPrefs.GetInt("BonusBalloon1Count") + 1);
            //    GameManager.Instance.bonusBalloon1Count = PlayerPrefs.GetInt("BonusBalloon1Count");

            //Debug.Log(PlayerPrefs.GetInt("BonusBalloon1Count"));
            //    PlayerPrefs.Save();
            //BonusBalloonUIManager.Instance.RefleshCounts();

        }
    }
    #endregion
}


[System.Serializable]
public class RewardedZones
{
    public int startLevel;
    public int numberOfSectionsToOpen;
}