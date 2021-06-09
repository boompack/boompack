using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointsUIManager : Singleton<PointsUIManager>
{
    public Text puanTextW;
    public Text puanTextL;

    public Text timePointTextW;
    public Text timePointTextL;
    public Text bonusPointTextW;
    public Text bonusPointTextL;
    public Text repeatPointTextW;
    public Text repeatPointTextL;
    public Text totalPointTextW;
    public Text totalPointTextL;

    public Text puanTextGameScreen;
    
    

    public void UpdatePointsWin(int point, int time, int repeatCount, int totalPoint, int bonusUseCount)
    {
        #if (!LEVEL_EDITOR)
        puanTextW.text = point.ToString();

        Debug.Log("Puanlar Güncellenecek");
        puanTextW.text = point.ToString();
        puanTextW.DOColor(new Color (0.6196079f, 0.282353f, 0.8431373f, 1f ), 4).From(Color.clear);


        bonusPointTextW.text = (-500 * bonusUseCount).ToString();
        bonusPointTextW.DOColor(new Color (0.6196079f, 0.282353f, 0.8431373f, 1f ), 4).From(Color.clear);
        timePointTextW.text =time>0? "1000":"0";
        timePointTextW.DOColor(new Color (0.6196079f, 0.282353f, 0.8431373f, 1f ), 5).From(Color.clear);
        repeatPointTextW.text = repeatCount==0 ? (250*repeatCount).ToString() : "-" + (250 * repeatCount).ToString();
        repeatPointTextW.DOColor(new Color (0.6196079f, 0.282353f, 0.8431373f, 1f ), 6).From(Color.clear);
        totalPointTextW.text = totalPoint.ToString();
        totalPointTextW.DOColor(new Color (0.6196079f, 0.282353f, 0.8431373f, 1f ), 7).From(Color.clear);
    
        int gecici = totalPoint;
        totalPoint = 0;
        DOTween.To(()=>totalPoint, x=>totalPoint = x, gecici, 4).OnUpdate(()=> totalPointTextW.text = totalPoint.ToString()).SetEase(Ease.InOutQuad);     
        #endif
    }

    public void UpdatePointsLose(int point, int time, int repeatCount, int totalPoint, int bonusUseCount)
    {
        #if (!LEVEL_EDITOR)
        puanTextL.text = point.ToString();
        bonusPointTextL.text = bonusUseCount.ToString();
        timePointTextL.text = time.ToString();
        repeatPointTextL.text = repeatCount.ToString();
        totalPointTextL.text = totalPoint.ToString();
        #endif
    }


    public void UpdateGameText(int point)
    {
        puanTextGameScreen.text = point.ToString();
    }

    public void Clear()
    {
        puanTextGameScreen.text = "0";
    }

    public void AnimateText(int gelensayi, Text gelenText)
    {




    }
}
