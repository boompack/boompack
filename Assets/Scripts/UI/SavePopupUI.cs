using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePopupUI : MonoBehaviour
{
    public int goingLevelID;

    public PopUpType popUpType;

    public void SayYes()
    {
        gameObject.SetActive(false);

        switch (popUpType)
        {
            case PopUpType.Previous:
                LELevelLoader.Instance.SaveCurrentLevel();
                LevelEditorUIManager.Instance.PreviousLevel();
                break;
            case PopUpType.Next:
                LELevelLoader.Instance.SaveCurrentLevel();
                LevelEditorUIManager.Instance.NextLevel();
                break;
            case PopUpType.Level:
                LELevelLoader.Instance.SaveCurrentLevel();
                LevelEditorUIManager.Instance.LoadLevel();
                break;
        }
    }

    public void SayNo()
    {
        //LevelEditorUIManager.Instance.RevertLevelChanges();
        gameObject.SetActive(false);

        switch (popUpType)
        {
            case PopUpType.Previous:
                LevelEditorUIManager.Instance.PreviousLevel();
                break;
            case PopUpType.Next:
                LevelEditorUIManager.Instance.NextLevel();
                break;
            case PopUpType.Level:
                LevelEditorUIManager.Instance.LoadLevel();
                break;
        }
    }

    public void SayDirectYes()
    {
        gameObject.SetActive(false);

        switch (popUpType)
        {
            case PopUpType.Previous:
                LELevelLoader.Instance.SaveCurrentLevel();
                LevelEditorManager.Instance.JSONKaydet();
                LevelEditorUIManager.Instance.PreviousLevel();
                break;
            case PopUpType.Next:
                LELevelLoader.Instance.SaveCurrentLevel();
                LevelEditorManager.Instance.JSONKaydet();
                LevelEditorUIManager.Instance.NextLevel();
                break;
            case PopUpType.Level:
                LELevelLoader.Instance.SaveCurrentLevel();
                LevelEditorManager.Instance.JSONKaydet();
                LevelEditorUIManager.Instance.LoadLevel();
                break;
        }
    }

    public void Vazgec()
    {
        gameObject.SetActive(false);
        switch (popUpType)
        {
            case PopUpType.Level:
                LevelEditorUIManager.Instance.loadedLevelText.text = LELevelLoader.Instance.loadedLevel.levelID.ToString();
                break;
        }
    }



}

public enum PopUpType
{
    Previous,
    Next,
    Level
}
