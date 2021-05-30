using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelUIButton : MonoBehaviour
{
    public int levelID;
    public Text levelText;

    public Image background;
    public Image border;

    public Image levelImage;

    public GameObject lockObject;
    public GameObject unlockObject;
    public GameObject lockImage;
    public GameObject unlockImage;

    public Image star1;
    public Image star2;
    public Image star3;

    public Sprite levelScreenshot;
    public Sprite levelScreenshotBlur;

    LevelStat myLevel=null;

    void Awake()
    {
        GamePackManager.Instance.gameButtonDict.Add(levelID, this);
    }

    private void OnEnable()
    {
        if(myLevel!=null)
            LoadStats();


    }

    void Start()
    {
        LoadStats();
    }

    public void GoLevel()
    {
        LevelManager.Instance.LoadLevel(levelID);
    }

    public void LoadStats()
    {
        myLevel = SaveManager.Instance.levelStats.levelStatsDict[levelID];

        if (myLevel.isLocked)
        {
            lockObject.SetActive(true);
            levelImage.sprite = levelScreenshotBlur;
            GetComponent<UIButton>().enabled = false;
            //if (SaveManager.Instance.levelStats.levelStatsDict[levelID].isPlayable)
            //{
            //    lockImage.SetActive(false);
            //    unlockImage.SetActive(true);
            //    GetComponent<UIButton>().enabled = true;
            //}
        }
        else
        {
            if (!SaveManager.Instance.levelStats.levelStatsDict[levelID].isPlayable)
            {
                lockObject.SetActive(false);
                unlockObject.SetActive(true);
                GetComponent<UIButton>().enabled = false;
                levelImage.sprite = levelScreenshotBlur;

            }
            else
            {
                GetComponent<UIButton>().enabled = true;
                //lockObject.SetActive(false);
                levelImage.sprite = levelScreenshot;
                unlockObject.SetActive(false);
                lockObject.SetActive(false);

            }
        }

        switch (SaveManager.Instance.levelStats.levelStatsDict[levelID].levelStar)
        {
            case 0:
                break;
            case 1:
                star1.sprite = BalloonGraphicReferance.Instance.fullLevelStar;

                break;
            case 2:
                star1.sprite = BalloonGraphicReferance.Instance.fullLevelStar;
                star2.sprite = star1.sprite;
                break;
            case 3:
                star1.sprite = BalloonGraphicReferance.Instance.fullLevelStar;
                star2.sprite = star1.sprite;
                star3.sprite = star1.sprite;
                break;
        }
    }
}




