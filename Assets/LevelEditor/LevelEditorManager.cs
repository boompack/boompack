using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using MessagePack;
using System.Diagnostics;
using System;
using UnityEngine.SceneManagement;

public class LevelEditorManager : Singleton<LevelEditorManager>
{

    public Levels levels;


    public int levelCount;
    public Text levelCountText;


    public GameObject levelButton;
    public InputField levelInput;

    public InputField redMovesInput;
    public InputField blueMovesInput;
    public InputField yellowMovesInput;
    public InputField greenMovesInput;
    public InputField timeInput;
    public InputField maxPointInput;
    public InputField stateInput;

    public byte[] convertedbyte;

    public bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        //LoadTestLevels();
        LoadLevelData();
        LoadFirstLevel();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {

            LELevelLoader.Instance.SaveCurrentLevel();
            JSONKaydet();
            UnityEngine.Debug.Log("Kaydedildi");
        }
    }
    public void LoadTestLevels()
    {

        levels = new Levels();


        for (int i = 0; i < 200; i++)
        {
            var level1 = new Level { levelID = i + 1, levelSizeX = 3, levelSizeY = 5 };
            levels.levelsList.Add(level1);

            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    level1.itemsList.Add(new Item { itemType = ItemType.StandartRedBalloon, itemX = j, itemY = k });
                }
            }
        }
    }

    public void LoadLevels()
    {
        Stopwatch saat = new Stopwatch();
        saat.Start();

        string path = Path.Combine(Application.streamingAssetsPath, "levelsByte.txt");
        StreamReader reader = new StreamReader(path);
        levels = MessagePackSerializer.Deserialize<Levels>(File.ReadAllBytes(path));
        reader.Close();
        reader.Dispose();

        //var Item = MessagePackSerializer.Deserialize<Level>(MessagePackSerializer.ConvertFromJson(reader.ReadToEnd()));
        //var Item2 = MessagePackSerializer.Deserialize<Level>(convertedbyte);


        saat.Stop();
        UnityEngine.Debug.Log(saat.ElapsedMilliseconds);

        //CreateButtons();
    }

    public void CreateButtons()
    {
        if (levels != null)
        {
            if (levels.levelsList != null)
            {
                levelCount = levels.levelsList.Count;

                for (int i = 0; i < levels.levelsList.Count; i++)
                {
                    Instantiate(levelButton);
                }
            }
        }
    }

    public void JSONKaydet()
    {
        LELevelLoader.Instance.SaveCurrentLevel();
        for (int i = 0; i < levels.levelsList.Count; i++)
        {
            levels.levelsList[i].levelID = i + 1;
        }

        //State Puanlarını Kaydet

        levels.statePoints = new Dictionary<int, int>();
        for (int i = 0; i < levels.levelsList.Count; i++)
        {
            int levelStateTemp = ((levels.levelsList[i].levelID - 1) / 20) + 1;
            UnityEngine.Debug.Log("LevelStateTemp: " + levelStateTemp);
            if(!levels.statePoints.ContainsKey(levelStateTemp))
            {
                levels.statePoints.Add(levelStateTemp,0);
            }
            levels.statePoints[levelStateTemp] += levels.levelsList[i].maxPoint;
        }


        string path = Path.Combine(Application.streamingAssetsPath, "levels.txt");

        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(levels)));
        writer.Flush();
        writer.Close();
        writer.Dispose();


        convertedbyte = MessagePackSerializer.Serialize(levels);
        File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "levelsByte.txt"), convertedbyte);
    }

    public void LoadLevelData()
    {
        LoadLevels();
        levelCountText.text = levels.levelsList.Count.ToString();
    }

    public void LoadFirstLevel()
    {
        if (levelInput.text == "")
        {
            levelInput.text = "1";
            levelInput.placeholder.GetComponent<Text>().text = "1";
        }

        //LELevelLoader.Instance.LoadLevel(1);
    }

    public void LoadLevel(int loadLevel)
    {
        LELevelLoader.Instance.LoadLevel(loadLevel);
    }

    public void NextLevel()
    {
        LELevelLoader.Instance.LoadLevel(LELevelLoader.Instance.loadedLevel.levelID + 1);
    }

    public void PreviousLevel()
    {
        LELevelLoader.Instance.LoadLevel(LELevelLoader.Instance.loadedLevel.levelID - 1);
    }

    public void PlayLevel()
    {
        LELevelLoader.Instance.SaveCurrentLevel();
        JSONKaydet();
        LevelManager.Instance.LoadLevelsAgain();
        LevelManager.Instance.LoadTestLevel(Int32.Parse(levelInput.text));
        HistoryManager.Instance.playedLevel = Int32.Parse(levelInput.text);

        Scene scene = SceneManager.GetActiveScene();
        foreach (var item in scene.GetRootGameObjects())
        {
            item.SetActive(false);
        }
        SceneManager.LoadScene(1, LoadSceneMode.Single);

        //LevelManager.Instance.playedLevel = LELevelLoader.Instance.loadedLevel;
        //LevelManager.
    }

    public void OnApplicationQuit()
    {
        /*
        LELevelLoader.Instance.SaveCurrentLevel();
        JSONKaydet();
        */
    }
}
