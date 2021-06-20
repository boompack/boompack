using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GamePackManager : Singleton<GamePackManager>
{
    public Dictionary<int,GameLevelUIButton> gameButtonDict = new Dictionary<int,GameLevelUIButton>();
    public Slider[] packSliders;
    public SliderText[] sliderTexts;

    public GameObject Content;

    public int[] levelPositions;

    void Start()
    {
        UpdateSliders();
        FindPosition();
    }

    public void UpdateAll()
    {
        foreach (var item in gameButtonDict)
        {
            item.Value.LoadStats();
        }
    }
    

    public void UpdateLevel(int levelID)
    {
        gameButtonDict[levelID].LoadStats();
    }

    public void UpdateSliders()
    {
        //foreach (var item in SaveManager.Instance.levelStats.levelStatsDict)
        //{
        //    packSliders[Yardimcilar.GetLevelState(item.Value.levelID) - 1].value += item.Value.maxPoint;
        //}

        foreach (var item in SaveManager.Instance.levelStats.levelStatsDict)
        {
            packSliders[Yardimcilar.GetLevelState(item.Value.levelID) - 1].value =0;
        }

        foreach (var item in SaveManager.Instance.levelStats.levelStatsDict)
        {
            packSliders[Yardimcilar.GetLevelState(item.Value.levelID) - 1].value += HighScoreManager.Instance.highscores[(item.Value.levelID) - 1];
        }

        foreach (var item in sliderTexts)
        {
            item.UpdateTexts();
        }
    }

    public void FindPosition()
    {
        Debug.Log("Bak Bu sate: "+ SaveManager.Instance.levelStats.lastOpenedState);
        Content.transform.localPosition = new Vector3(0,levelPositions[SaveManager.Instance.levelStats.lastOpenedState - 1], 0);
    }

    public void AnimateLastLevel()
    {
        if (SaveManager.Instance.levelStats.levelStatsDict[300].isPlayable != true)
        {
            gameButtonDict[(SaveManager.Instance.levelStats.lastPlayedLevel + 1)].border.DOColor(Color.cyan, 0.5f)
                .SetLoops(20, LoopType.Yoyo);
        }
        else
        {
            gameButtonDict[(SaveManager.Instance.levelStats.lastPlayedLevel)].border.DOColor(Color.cyan, 0.5f)
                .SetLoops(20, LoopType.Yoyo);
        }
    }

    public void StopLastLevelAnimation()
    {
        if (SaveManager.Instance.levelStats.levelStatsDict[300].isPlayable != true)
        {
            gameButtonDict[(SaveManager.Instance.levelStats.lastPlayedLevel + 1)].border.DOKill();

        }
        else
        {
            gameButtonDict[(SaveManager.Instance.levelStats.lastPlayedLevel)].border.DOKill();

        }
    }
}
