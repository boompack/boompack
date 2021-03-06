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
    public LevelStatsObject levelStats = new LevelStatsObject();

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

        if (GameManager.Instance.levelLoader.loadedLevel.levelID != 300)
        {
            foreach (RewardedZones rewardedZones in AdsManager.Instance.rewardedZones)
            {
                if (LevelManager.Instance.playedLevel.levelID == rewardedZones.startLevel && SaveManager.Instance.levelStats.levelStatsDict[GameManager.Instance.levelLoader.loadedLevel.levelID + 2].isLocked == true)
                {
                    //Debug.Log(SaveManager.Instance.levelStats.levelStatsDict[GameManager.Instance.levelLoader.loadedLevel.levelID + 1]);
                    //Debug.Log(SaveManager.Instance.levelStats.levelStatsDict[GameManager.Instance.levelLoader.loadedLevel.levelID + 1].isLocked);
                    AdsManager.Instance.numberOfSectionsToOpen = rewardedZones.numberOfSectionsToOpen;
                    AdsManager.Instance.startLevel = rewardedZones.startLevel;
                    rewardedPanelMessage.text = "Watch an ad to enable the next " + AdsManager.Instance.numberOfSectionsToOpen + " levels.";
                    rewardedButtonMessage.text = "Main Menu";
                    rewardAdZone.SetActive(true);
                    DoozyManager.Instance.SendGameEvent("OpenLockScreen");
                    return;
                }

            }

        }
        else
        {
            DoozyManager.Instance.SendGameEvent("LastGame");

        }


        if (PlayerPrefs.GetInt("PREMIUM") == 1)
        {
            GameManager.Instance.LoadNextLevel();
        }
        else
        {
            if (GameManager.Instance.levelLoader.loadedLevel.levelID == 240)
            {
                GameManager.Instance.SavePrefs();
                GameManager.Instance.StopAllCoroutines();
                GameManager.Instance.DestroyGameScene();
                DoozyManager.Instance.SendGameEvent("UpgrageMessage");

            }
            else
            {
                GameManager.Instance.LoadNextLevel();

            }
        }
        
    }

    public void DirectNextLevelButton()
    {
        GameManager.Instance.LoadNextLevel();

    }

public void UpdateLevelText(int levelID)
    {
        levelText.text = levelID.ToString();
    }

    public void SendFeedback()
    {
        Application.OpenURL ("mailto:" + "boompack.game@gmail.com" + "?subject=" + "Boom Pack Feedback");
    }
}
