using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenManager : Singleton<WinScreenManager>
{

    public Image star1;
    public Image star2;
    public Image star3;

    public Image emoji;

    public Text congratsText;

    public Sprite fullStar;
    public Sprite emptyStar;

    public Sprite emoji1;
    public Sprite emoji2;
    public Sprite emoji3;

    public GameObject filledStar1;
    public GameObject filledStar2;
    public GameObject filledStar3;

    public Text NextLevelText;

    //private void OnEnable()
    //{
    //    NextLevelText.text= GameManager.Instance.levelLoader.loadedLevel.levelID != 300 ? "Next Level" : "Next";
    //}

    private void OnDisable()
    {
        filledStar1.SetActive(false);
        filledStar2.SetActive(false);
        filledStar3.SetActive(false);

    }

    public void SetGraphics(int bonusCount, int repeatCount, int timeCount)
    {
        Debug.Log("timecount" + timeCount);
        if(bonusCount != 0)
        {
            //star1.sprite = fullStar;
            //star2.sprite = emptyStar;
            //star3.sprite = emptyStar;

            filledStar1.SetActive(true);

            emoji.sprite = emoji1;

            congratsText.text = "Good!";
        }

        

        else if(repeatCount == 0 && timeCount > 0)
        {
            //star1.sprite = fullStar;
            //star2.sprite = fullStar;
            //star3.sprite = fullStar;

            filledStar3.SetActive(true);

            emoji.sprite = emoji3;

            congratsText.text = "Excellent!";

        }

        //else if (repeatCount != 0 || timeCount! > 0)
        else
        {
            //star1.sprite = fullStar;
            //star2.sprite = fullStar;
            //star3.sprite = emptyStar;

            filledStar2.SetActive(true);


            emoji.sprite = emoji2;

            congratsText.text = "Perfect!";
        }
    }
}
