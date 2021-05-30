using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevelUIManager : Singleton<TestLevelUIManager>
{
    public void BackToEditor()
    {
        SceneManager.LoadScene("LevelEditor");
    }

    public void NextLevel()
    {
        int currentLevelID = LevelManager.Instance.playedLevel.levelID;
        LevelManager.Instance.playedLevel = LevelManager.Instance.levelsDictionary[currentLevelID + 1];
        SceneManager.LoadScene("LEGameScene");
    }

    public void PreviousLevel()
    {
        int currentLevelID = LevelManager.Instance.playedLevel.levelID;
        LevelManager.Instance.playedLevel = LevelManager.Instance.levelsDictionary[currentLevelID - 1];
        SceneManager.LoadScene("LEGameScene");
    }

    public void GoToLevel(string input)
    {

        int gidilecekLevel = Int32.Parse(input);
        LevelManager.Instance.playedLevel = LevelManager.Instance.levelsDictionary[gidilecekLevel];
        SceneManager.LoadScene("LEGameScene");
    }

    public void ReloadTestLevel()
    {
        //DestroyGameScene();
        SceneManager.LoadScene("LEGameScene");
    }

    public void DestroyGameScene()
    {
        SceneManager.UnloadScene("LEGameScene");
    }
}
