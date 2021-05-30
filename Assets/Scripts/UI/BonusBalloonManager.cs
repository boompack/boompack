using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBalloonManager : Singleton<BonusBalloonManager>
{

    public void Start()
    {
        ResetBalloons();
    }
    public void ResetBalloons()
    {
        PlayerPrefs.SetInt("BonusBalloon1Count", 5);
        PlayerPrefs.SetInt("BonusBalloon2Count", 5);
        PlayerPrefs.SetInt("BonusBalloon3Count", 5);
        PlayerPrefs.SetInt("BonusBalloon4Count", 0);
        PlayerPrefs.SetInt("BonusBalloon5Count", 0);
        PlayerPrefs.SetInt("BonusBalloon6Count", 0);
        PlayerPrefs.SetInt("BonusBalloon7Count", 0);
        PlayerPrefs.SetInt("BonusBalloon8Count", 0);
        PlayerPrefs.SetInt("BonusBalloon9Count", 0);
        PlayerPrefs.SetInt("BonusBalloon10Count", 0);
        PlayerPrefs.SetInt("BonusBalloon11Count", 0);
        PlayerPrefs.SetInt("BonusBalloon12Count", 0);
        PlayerPrefs.SetInt("BonusBalloon13Count", 0);
        PlayerPrefs.SetInt("BonusBalloon14Count", 0);
        PlayerPrefs.SetInt("BonusBalloon15Count", 0);
        PlayerPrefs.SetInt("BonusBalloon16Count", 0);
        PlayerPrefs.SetInt("BonusBalloon17Count", 0);
        PlayerPrefs.SetInt("BonusBalloon18Count", 0);
        PlayerPrefs.SetInt("BonusBalloon19Count", 0);
        PlayerPrefs.SetInt("BonusBalloon20Count", 0);
        PlayerPrefs.SetInt("BonusBalloon21Count", 0);
        PlayerPrefs.SetInt("BonusBalloon22Count", 0);
        PlayerPrefs.SetInt("BonusBalloon23Count", 0);
        PlayerPrefs.SetInt("BonusBalloon24Count", 0);
        PlayerPrefs.SetInt("BonusBalloon25Count", 0);
        PlayerPrefs.Save();

        GameManager.Instance.bonusBalloon1Count = 5;
        GameManager.Instance.bonusBalloon2Count = 5;
        GameManager.Instance.bonusBalloon3Count = 5;
        GameManager.Instance.bonusBalloon4Count = 0;
        GameManager.Instance.bonusBalloon5Count = 0;
        GameManager.Instance.bonusBalloon6Count = 0;
        GameManager.Instance.bonusBalloon7Count = 0;
        GameManager.Instance.bonusBalloon8Count = 0;
        GameManager.Instance.bonusBalloon9Count = 0;
        GameManager.Instance.bonusBalloon10Count = 0;
        GameManager.Instance.bonusBalloon11Count = 0;
        GameManager.Instance.bonusBalloon12Count = 0;
        GameManager.Instance.bonusBalloon13Count = 0;
        GameManager.Instance.bonusBalloon14Count = 0;
        GameManager.Instance.bonusBalloon15Count = 0;
        GameManager.Instance.bonusBalloon16Count = 0;
        GameManager.Instance.bonusBalloon17Count = 0;
        GameManager.Instance.bonusBalloon18Count = 0;
        GameManager.Instance.bonusBalloon19Count = 0;
        GameManager.Instance.bonusBalloon20Count = 0;
        GameManager.Instance.bonusBalloon21Count = 0;
        GameManager.Instance.bonusBalloon22Count = 0;
        GameManager.Instance.bonusBalloon23Count = 0;
        GameManager.Instance.bonusBalloon24Count = 0;
        GameManager.Instance.bonusBalloon25Count = 0;

        BonusBalloonUIManager.Instance.RefleshCounts();
    }

}
