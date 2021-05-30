using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditorUIManager : Singleton<LevelEditorUIManager>
{
    public GameObject selectedPrefab;
    public Toggle bonusBalloonToggle;

    public InputField loadedLevelText;

    public InputField ropeInput;
    public InputField zoneInput;

    public GameObject SavePopUp;
    public GameObject LevelDeletePopUp;

    void Start()
    {
        BonusReset();
    }

    public void LoadLevel()
    {
        LevelEditorManager.Instance.LoadLevel(System.Int32.Parse(loadedLevelText.text));
        HistoryManager.Instance.ClearHistory();
    }

    public void LevelDeleteButton()
    {
        LevelDeletePopUp.SetActive(true);
    }

    public void LevelDeleteNoButton()
    {
        LevelDeletePopUp.SetActive(false);
    }

    public void LoadLevelButton()
    {
        if (!HistoryManager.Instance.HasLevelChanged())
        {
            LoadLevel();
        }
        else
        {
            SavePopUp.SetActive(true);
            SavePopUp.GetComponent<SavePopupUI>().popUpType = PopUpType.Level;
        }
    }

    public void NextLevel()
    {
        if (loadedLevelText.text == (LevelManager.Instance.levels.levelsList.Count).ToString())
        {
            LELevelLoader.Instance.LoadLevel(1);
        }
        else
        {
            LELevelLoader.Instance.LoadLevel(LELevelLoader.Instance.loadedLevel.levelID + 1);
        }
        HistoryManager.Instance.ClearHistory();
    }

    public void PreviousLevel()
    {
        Debug.Log("Previous Bas�ld�");
        if (loadedLevelText.text == "1")
        {         
            LELevelLoader.Instance.LoadLevel(LevelManager.Instance.levels.levelsList.Count);
        }
        else
        {            
            LELevelLoader.Instance.LoadLevel(LELevelLoader.Instance.loadedLevel.levelID - 1);
        }
        HistoryManager.Instance.ClearHistory();
    }

    public void PreviousLevelButton()
    {
        if (!HistoryManager.Instance.HasLevelChanged())
        {
            PreviousLevel();
        }
        else
        {
            SavePopUp.SetActive(true);
            SavePopUp.GetComponent<SavePopupUI>().popUpType = PopUpType.Previous;
        }
    }

    public void NextLevelButton()
    {
        if (!HistoryManager.Instance.HasLevelChanged())
        {
            NextLevel();
        }
        else
        {
            SavePopUp.SetActive(true);
            SavePopUp.GetComponent<SavePopupUI>().popUpType = PopUpType.Next;
        }
    }

    public void RevertAllChanges()
    {
        LELevelLoader.Instance.LoadLevel(LELevelLoader.Instance.loadedLevel.levelID);
    }

    public void RevertLevelChanges()
    {
        HistoryManager.Instance.YedekGeriYukle();
    }

    public void BonusReset()
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
    }

    public void LevelOrderEditorLoad()
    {
        SceneManager.LoadScene(2);
    }
}
