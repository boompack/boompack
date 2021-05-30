using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public GameObject GameLevelsContent;
    public GameObject GameLevelButtonPrefab;

    public LeanToggle healthTextToggle;
    public LeanToggle soundToggle;
    public LeanToggle vibrationToggle;

    public Text levelText;

    public Text rewardedPanelMessage;
    public Text rewardedButtonMessage;
    public GameObject rewardAdZone;

    public Image hand;
    public void ShowHealthTexts()
    {
        foreach (var item in GameManager.Instance.balloonsList)
        {
            item.ShowHealthText();
        }
    }

    public void HideHealthText()
    {
        foreach (var item in GameManager.Instance.balloonsList)
        {
            item.HideHealthText();
        }
    }

    public void ReloadLevelButton()
    {
        GameManager.Instance.ReloadLevel();
    }

    public void NextLevelButton()
    {
        //GameManager.Instance.LoadNextLevel();
        //UnityEngine.Debug.Log(LevelManager.Instance.playedLevel.levelID);

        foreach (RewardedZones rewardedZones in AdsManager.Instance.rewardedZones)
        {
            if (LevelManager.Instance.playedLevel.levelID == rewardedZones.startLevel)
            {
                AdsManager.Instance.numberOfSectionsToOpen = rewardedZones.numberOfSectionsToOpen;
                AdsManager.Instance.startLevel = rewardedZones.startLevel;
                rewardedPanelMessage.text = "Watch an ad to enable the next " + AdsManager.Instance.numberOfSectionsToOpen + " levels.";
                rewardedButtonMessage.text = "Main Menu";
                rewardAdZone.SetActive(true);
                DoozyManager.Instance.SendGameEvent("OpenLockScreen");
                return;
            }

        }

        GameManager.Instance.LoadNextLevel();
    }

    public void UpdateLevelText(int levelID)
    {
        levelText.text = levelID.ToString();
    }
}
