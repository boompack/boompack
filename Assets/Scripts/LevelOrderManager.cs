using Lean.Touch;
using MessagePack;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOrderManager : MonoBehaviour
{
    public Levels levels;
    public int levelCount;

    public GameObject levelPrefab;

    public byte[] convertedbyte;

    public List<GameObject> levelObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        DeleteOldInstances();
        LoadLevelData();
        CreatePrefabs();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteOldInstances()
    {
        Destroy(HistoryManager.Instance.gameObject);
        Destroy(CameraResizer.Instance.gameObject);
        Destroy(LevelManager.Instance.gameObject);
    }

    public void CreatePrefabs()
    {
        foreach (var item in levels.levelsList)
        {
            GameObject yaratilan = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
            levelObjects.Add(yaratilan);

            int koordinatX = -((item.levelID - 1) / 25);
            yaratilan.GetComponent<LeanSelectableBlock>().X = (item.levelID -1) % 25;
            yaratilan.GetComponent<LeanSelectableBlock>().Y = koordinatX;
            yaratilan.GetComponent<LevelUI>().levelID = item.levelID;
            yaratilan.GetComponent<LevelUI>().levelUIText.text = item.levelID.ToString();
            yaratilan.GetComponent<LevelUI>().level = item;
        }
        
        
    }

    public void LoadLevelData()
    {
        LoadLevels();
    }

    public void LoadLevels()
    {
        Stopwatch saat = new Stopwatch();
        saat.Start();

        string path = Path.Combine(Application.streamingAssetsPath, "levels.txt");
        StreamReader reader = new StreamReader(path);
        levels = MessagePackSerializer.Deserialize<Levels>(MessagePackSerializer.ConvertFromJson(reader.ReadToEnd()));
        reader.Close();
        reader.Dispose();

        //var Item = MessagePackSerializer.Deserialize<Level>(MessagePackSerializer.ConvertFromJson(reader.ReadToEnd()));
        //var Item2 = MessagePackSerializer.Deserialize<Level>(convertedbyte);


        saat.Stop();
        UnityEngine.Debug.Log(saat.ElapsedMilliseconds);

        //CreateButtons();
    }

    public void JSONKaydet()
    {
        Dictionary<int, Level> dictLevels = new Dictionary<int, Level>();
        foreach (var item in levelObjects)
        {
            dictLevels.Add((item.GetComponent<LeanSelectableBlock>().Y * -25) + (item.GetComponent<LeanSelectableBlock>().X + 1), item.GetComponent<LevelUI>().level);
        }


        List<Level> newLevelList = new List<Level>();
        for (int i = 1; i <= dictLevels.Count; i++)
        {
            dictLevels[i].levelID = i;
            newLevelList.Add(dictLevels[i]);
        }
        string path = Path.Combine(Application.streamingAssetsPath, "levels.txt");

        StreamWriter writer = new StreamWriter(path, false);
        levels.levelsList = newLevelList;
        writer.Write(MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(levels)));
        writer.Flush();
        writer.Close();
        writer.Dispose();


        convertedbyte = MessagePackSerializer.Serialize(levels);
        File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "levelsByte.txt"), convertedbyte);
        SceneManager.LoadScene(0);
    }

    public void ExitWithoutSaving()
    {
        SceneManager.LoadScene(0);
    }


}
