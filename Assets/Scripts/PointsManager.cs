using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : Singleton<PointsManager>
{
    public int point;
    public int timePoint;
    public int bonusPoint;
    public int repeatPoint;
    public int totalPoint;

    public void AddPoints(int gelenPuan)
    {
        point += gelenPuan;
        PointsUIManager.Instance.UpdateGameText(point);
    }

    public void CalculatePoints(int time, int repeatCount, int bonusUseCount, bool isWin)
    {
        totalPoint = point - (repeatCount * 250) - (bonusUseCount * 500);
        if(time > 0 )
        {
            totalPoint += 1000;
        }
        
        if(isWin)
        {
            Debug.Log(totalPoint);

            if (totalPoint > HighScoreManager.Instance.highscores[LevelManager.Instance.playedLevel.levelID - 1])
            {
                PlayerPrefs.SetInt("LevelHighScore" + (LevelManager.Instance.playedLevel.levelID - 1), totalPoint);
                HighScoreManager.Instance.highscores[LevelManager.Instance.playedLevel.levelID - 1] = PlayerPrefs.GetInt("LevelHighScore" + (LevelManager.Instance.playedLevel.levelID - 1));
                PointsUIManager.Instance.UpdatePointsWin(point, time, repeatCount, totalPoint, bonusUseCount);

            }
            else
            {
                PointsUIManager.Instance.UpdatePointsWin(point, time, repeatCount, totalPoint, bonusUseCount);

            }

            //PointsUIManager.Instance.UpdatePointsWin(point, time, repeatCount, totalPoint, bonusUseCount);
        }
        else
        {
            PointsUIManager.Instance.UpdatePointsLose(point, time, repeatCount, totalPoint, bonusUseCount);
        }
    }
}
