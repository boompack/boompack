using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LELevelLoader : Singleton<LELevelLoader>
{

    public Level loadedLevel;

    public GameObject blockPrefab;
    public GameObject StandartRedBalloonPrefab;
    public GameObject StandartBlueBalloonPrefab;
    public GameObject StandartYellowBalloonPrefab;
    public GameObject StandartGreenBalloonPrefab;
    public GameObject DontTouchBalloonPrefab;
    public GameObject MustTouchBalloonPrefab;
    public GameObject ReflectiveBalloonPrefab;
    public GameObject DoubleBalloonPrefab;

    public GameObject wallPrefabR;
    public GameObject wallPrefabU;

    public Block[,] blocksArray;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(1);
    }

    public void LoadLevel(int levelID)
    {
        bool playerdangeldi;
        if (LevelManager.Instance.playedLevel != null)
        {
            levelID = HistoryManager.Instance.playedLevel;
            LevelManager.Instance.playedLevel = null;
            playerdangeldi = true;
            Debug.Log("asd222f");
        }
        else
        {
            playerdangeldi = false;
        }

        LevelEditorManager.Instance.levelInput.text = levelID.ToString();

        //SaveCurrentLevel();
        //Delete old Items
        if (blocksArray != null)
        {
            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    if (blocksArray[i, j] != null)
                    {
                        if (blocksArray[i, j].onBalloon != null)
                        {
                            Destroy(blocksArray[i, j].onBalloon.gameObject);
                        }
                        Destroy(blocksArray[i, j].gameObject);
                    }
                }
            }
        }

        loadedLevel = LevelEditorManager.Instance.levels.levelsList[levelID - 1];

        if (!playerdangeldi)
        {
            Debug.Log("asd");
            HistoryManager.Instance.YedekOlustur();
        }
        // Load Level Stats

        LevelEditorManager.Instance.redMovesInput.text = loadedLevel.redMoves.ToString();
        LevelEditorManager.Instance.blueMovesInput.text = loadedLevel.blueMoves.ToString();
        LevelEditorManager.Instance.yellowMovesInput.text = loadedLevel.yellowMoves.ToString();
        LevelEditorManager.Instance.greenMovesInput.text = loadedLevel.greenMoves.ToString();
        LevelEditorManager.Instance.timeInput.text = loadedLevel.timeLimit.ToString();
        LevelEditorManager.Instance.maxPointInput.text = loadedLevel.maxPoint.ToString();



        // Creating Blocks

        blocksArray = new Block[loadedLevel.levelSizeX, loadedLevel.levelSizeY];
        

        for (int i = 0; i < loadedLevel.levelSizeX; i++)
        {
            for (int j = 0; j < loadedLevel.levelSizeY; j++)
            {
                blocksArray[i, j] = Instantiate(blockPrefab, new Vector3(i, j, 0), Quaternion.identity).GetComponent<Block>();
                blocksArray[i, j].placeX = i;
                blocksArray[i, j].placeY = j;
            }
        }

        // Creating Balloons

        GameObject createdGameObject = null;
        foreach (var balloon in loadedLevel.itemsList)
        {
            switch (balloon.itemType)
            {
                case ItemType.StandartRedBalloon:
                    createdGameObject = Instantiate(StandartRedBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;
                case ItemType.StandartBlueBalloon:
                    createdGameObject = Instantiate(StandartBlueBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;
                case ItemType.StandartGreenBalloon:
                    createdGameObject = Instantiate(StandartGreenBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;
                case ItemType.StandartYellowBalloon:
                    createdGameObject = Instantiate(StandartYellowBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;
                case ItemType.DontTouchBalloon:
                    createdGameObject = Instantiate(DontTouchBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;
                case ItemType.MustTouchBalloon:
                    createdGameObject = Instantiate(MustTouchBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;
                case ItemType.ReflectiveBalloon:
                    createdGameObject = Instantiate(ReflectiveBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;
                case ItemType.DoubleBalloon:
                    createdGameObject = Instantiate(DoubleBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, 1), Quaternion.identity);
                    break;

                default:
                    Debug.Log("Henüz Import Edilmeyen Bir Balon Türü Geldi");
                    break;
            }

            blocksArray[balloon.itemX, balloon.itemY].GetComponent<Block>().onBalloon = createdGameObject.GetComponent<Balloon>();
            createdGameObject.GetComponent<Balloon>().onBlock = blocksArray[balloon.itemX, balloon.itemY];
        }

        // Load Walls

        if (loadedLevel.wallsLList != null)
        {
            foreach (var item in loadedLevel.wallsLList)
            {
                ((LEBlock)blocksArray[item.wallX, item.wallY]).wallL.AddWall();
            }
        }

        if (loadedLevel.wallsBList != null)
        {
            foreach (var item in loadedLevel.wallsBList)
            {
                ((LEBlock)blocksArray[item.wallX, item.wallY]).wallB.AddWall();
            }
        }

        // Load Ropes

        if (loadedLevel.ropesList != null)
        {
            foreach (var ropeItem in loadedLevel.ropesList)
            {
                foreach (var item in ropeItem.ropeConnections)
                {
                    ((LEBlock)blocksArray[item.connX, item.connY]).SetupRopeID(loadedLevel.ropesList.IndexOf(ropeItem) + 1);
                }
            }
        }

        //Load Zones

        if (loadedLevel.zonesList != null)
        {
            foreach (var zoneItem in loadedLevel.zonesList)
            {
                foreach (var item in zoneItem.zoneConnections)
                {
                    ((LEBlock)blocksArray[item.connX, item.connY]).SetupBorderID(loadedLevel.zonesList.IndexOf(zoneItem) + 1);
                }
            }
        }

        SetupCamera();
    }

    public void SatirDegistir(bool arttir)
    {
        if (arttir)
        {
            loadedLevel.levelSizeY++;
            var newArray = new Block[blocksArray.GetLength(0), blocksArray.GetLength(1) + 1];

            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            Debug.Log(blocksArray.GetLength(0));
            Debug.Log(blocksArray.GetLength(1));
            blocksArray = newArray;
            Debug.Log(blocksArray.GetLength(0));
            Debug.Log(blocksArray.GetLength(1));

            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                blocksArray[i, blocksArray.GetLength(1) - 1] = Instantiate(blockPrefab, Yardimcilar.Derinlik(i, blocksArray.GetLength(1) - 1, 0), Quaternion.identity).GetComponent<Block>();
                blocksArray[i, blocksArray.GetLength(1) - 1].placeX = i;
                blocksArray[i, blocksArray.GetLength(1) - 1].placeY = blocksArray.GetLength(1) - 1;
            }
        }
        else
        {
            loadedLevel.levelSizeY--;
            var newArray = new Block[blocksArray.GetLength(0), blocksArray.GetLength(1) - 1];

            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                if (blocksArray[i, blocksArray.GetLength(1) - 1] != null)
                {
                    if (blocksArray[i, blocksArray.GetLength(1) - 1].onBalloon != null)
                    {
                        Destroy(blocksArray[i, blocksArray.GetLength(1) - 1].onBalloon.gameObject);
                    }

                    Destroy(blocksArray[i, blocksArray.GetLength(1) - 1].gameObject);
                    blocksArray[i, blocksArray.GetLength(1) - 1] = null;
                }
            }

            for (int i = 0; i < newArray.GetLength(0); i++)
            {
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            blocksArray = newArray;
        }

        loadedLevel.levelSizeX = blocksArray.GetLength(0);
        loadedLevel.levelSizeY = blocksArray.GetLength(1);
        SetupCamera();
    }

    public void SatirDegistirAsagiya(bool arttir)
    {
        if (arttir)
        {
            loadedLevel.levelSizeY++;
            var newArray = new Block[blocksArray.GetLength(0), blocksArray.GetLength(1) + 1];

            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            Debug.Log("Old X: " + blocksArray.GetLength(0));
            Debug.Log("Old Y: " + blocksArray.GetLength(1));
            blocksArray = newArray;
            Debug.Log("New X: " + blocksArray.GetLength(0));
            Debug.Log("New Y: " + blocksArray.GetLength(1));

            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                blocksArray[i, blocksArray.GetLength(1) - 1] = Instantiate(blockPrefab, Yardimcilar.Derinlik(i, blocksArray.GetLength(1) - 1, 0), Quaternion.identity).GetComponent<Block>();
                blocksArray[i, blocksArray.GetLength(1) - 1].placeX = i;
                blocksArray[i, blocksArray.GetLength(1) - 1].placeY = blocksArray.GetLength(1) - 1;
            }

            //Kaydir

            for (int i = blocksArray.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = blocksArray.GetLength(1) - 1; j > 0; j--)
                {
                    if (blocksArray[i, j - 1].onBalloon != null)
                    {
                        blocksArray[i, j].placeX = i;
                        blocksArray[i, j].placeY = j;
                        blocksArray[i, j].onBalloon = blocksArray[i, j - 1].onBalloon;
                        blocksArray[i, j - 1].onBalloon = null;
                        blocksArray[i, j].onBalloon.onBlock = blocksArray[i, j];
                        blocksArray[i, j].onBalloon.transform.position = blocksArray[i, j].transform.position;

                        ((LEBlock)blocksArray[i, j]).ChangeZone(blocksArray[i, j - 1].borderID);
                        ((LEBlock)blocksArray[i, j]).ChangeRope(((LEBlock)blocksArray[i, j - 1]).ropeID);
                        ((LEBlock)blocksArray[i, j - 1]).ChangeZone(0);
                        ((LEBlock)blocksArray[i, j - 1]).ChangeRope(0);
                        ((LEBlock)blocksArray[i, j]).wallB.wallX = i;
                        ((LEBlock)blocksArray[i, j]).wallB.wallY = j;
                        ((LEBlock)blocksArray[i, j]).wallL.wallX = i;
                        ((LEBlock)blocksArray[i, j]).wallL.wallY = j;

                        if (((LEBlock)blocksArray[i, j - 1]).wallB.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.AddWall();
                            ((LEBlock)blocksArray[i, j - 1]).wallB.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.DeleteWall();
                        }

                        if (((LEBlock)blocksArray[i, j - 1]).wallL.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.AddWall();
                            ((LEBlock)blocksArray[i, j - 1]).wallL.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.DeleteWall();
                        }
                    }
                }
            }
        }
        else
        {
            loadedLevel.levelSizeY--;
            var newArray = new Block[blocksArray.GetLength(0), blocksArray.GetLength(1) - 1];


            //En alttaki Balonları sil
            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                if (blocksArray[i, 0].onBalloon != null)
                {
                    Destroy(blocksArray[i, 0].onBalloon.gameObject);
                    blocksArray[i, 0].onBalloon = null;
                }
            }


            //Alta Kaydır
            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1) - 1; j++)
                {
                    if (blocksArray[i, j + 1].onBalloon != null)
                    {
                        blocksArray[i, j].onBalloon = blocksArray[i, j + 1].onBalloon;
                        blocksArray[i, j + 1].onBalloon = null;

                        blocksArray[i, j].onBalloon.onBlock = blocksArray[i, j];
                        blocksArray[i, j].onBalloon.transform.position = blocksArray[i, j].transform.position;

                        ((LEBlock)blocksArray[i, j]).ChangeZone(blocksArray[i, j + 1].borderID);
                        ((LEBlock)blocksArray[i, j]).ChangeRope(((LEBlock)blocksArray[i, j + 1]).ropeID);
                        ((LEBlock)blocksArray[i, j + 1]).ChangeZone(0);
                        ((LEBlock)blocksArray[i, j + 1]).ChangeRope(0);

                        if (((LEBlock)blocksArray[i, j + 1]).wallB.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.AddWall();
                            ((LEBlock)blocksArray[i, j + 1]).wallB.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.DeleteWall();
                        }

                        if (((LEBlock)blocksArray[i, j + 1]).wallL.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.AddWall();
                            ((LEBlock)blocksArray[i, j + 1]).wallL.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.DeleteWall();
                        }
                    }
                }
            }


            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                if (blocksArray[i, blocksArray.GetLength(1) - 1] != null)
                {
                    if (blocksArray[i, blocksArray.GetLength(1) - 1].onBalloon != null)
                    {
                        Destroy(blocksArray[i, blocksArray.GetLength(1) - 1].onBalloon.gameObject);
                    }

                    Destroy(blocksArray[i, blocksArray.GetLength(1) - 1].gameObject);
                    blocksArray[i, blocksArray.GetLength(1) - 1] = null;
                }
            }

            for (int i = 0; i < newArray.GetLength(0); i++)
            {
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            blocksArray = newArray;
        }

        loadedLevel.levelSizeX = blocksArray.GetLength(0);
        loadedLevel.levelSizeY = blocksArray.GetLength(1);
        SetupCamera();
    }

    public void SutunDegistir(bool arttir)
    {
        if (arttir)
        {
            loadedLevel.levelSizeX++;
            var newArray = new Block[blocksArray.GetLength(0) + 1, blocksArray.GetLength(1)];

            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            Debug.Log(blocksArray.GetLength(0));
            Debug.Log(blocksArray.GetLength(1));
            blocksArray = newArray;
            Debug.Log(blocksArray.GetLength(0));
            Debug.Log(blocksArray.GetLength(1));

            for (int i = 0; i < blocksArray.GetLength(1); i++)
            {
                blocksArray[blocksArray.GetLength(0) - 1, i] = Instantiate(blockPrefab, Yardimcilar.Derinlik(blocksArray.GetLength(0) - 1, i, 0), Quaternion.identity).GetComponent<Block>();
                blocksArray[blocksArray.GetLength(0) - 1, i].placeX = blocksArray.GetLength(0) - 1;
                blocksArray[blocksArray.GetLength(0) - 1, i].placeY = i;
            }
        }
        else
        {
            loadedLevel.levelSizeX--;
            var newArray = new Block[blocksArray.GetLength(0) - 1, blocksArray.GetLength(1)];

            for (int i = 0; i < blocksArray.GetLength(1); i++)
            {
                if (blocksArray[blocksArray.GetLength(0) - 1, i] != null)
                {
                    if (blocksArray[blocksArray.GetLength(0) - 1, i].onBalloon != null)
                    {
                        Destroy(blocksArray[blocksArray.GetLength(0) - 1, i].onBalloon.gameObject);
                    }

                    Destroy(blocksArray[blocksArray.GetLength(0) - 1, i].gameObject);
                    blocksArray[blocksArray.GetLength(0) - 1, i] = null;
                }
            }

            for (int i = 0; i < newArray.GetLength(0); i++)
            {
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            blocksArray = newArray;
        }

        loadedLevel.levelSizeX = blocksArray.GetLength(0);
        loadedLevel.levelSizeY = blocksArray.GetLength(1);



        SetupCamera();
    }

    public void SutunDegistirSola(bool arttir)
    {
        if (arttir)
        {
            loadedLevel.levelSizeX++;
            var newArray = new Block[blocksArray.GetLength(0) + 1, blocksArray.GetLength(1)];

            for (int i = 0; i < blocksArray.GetLength(0); i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            Debug.Log(blocksArray.GetLength(0));
            Debug.Log(blocksArray.GetLength(1));
            blocksArray = newArray;
            Debug.Log(blocksArray.GetLength(0));
            Debug.Log(blocksArray.GetLength(1));

            for (int i = 0; i < blocksArray.GetLength(1); i++)
            {
                blocksArray[blocksArray.GetLength(0) - 1, i] = Instantiate(blockPrefab, Yardimcilar.Derinlik(blocksArray.GetLength(0) - 1, i, 0), Quaternion.identity).GetComponent<Block>();
                blocksArray[blocksArray.GetLength(0) - 1, i].placeX = blocksArray.GetLength(0) - 1;
                blocksArray[blocksArray.GetLength(0) - 1, i].placeY = i;
            }

            //Kaydır

            for (int i = blocksArray.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = blocksArray.GetLength(1) - 1; j >= 0; j--)
                {
                    if (blocksArray[i - 1, j].onBalloon != null)
                    {
                        blocksArray[i, j].placeX = i;
                        blocksArray[i, j].placeY = j;
                        blocksArray[i, j].onBalloon = blocksArray[i - 1, j].onBalloon;
                        blocksArray[i - 1, j].onBalloon = null;
                        blocksArray[i, j].onBalloon.onBlock = blocksArray[i, j];
                        blocksArray[i, j].onBalloon.transform.position = blocksArray[i, j].transform.position;

                        ((LEBlock)blocksArray[i, j]).ChangeZone(blocksArray[i - 1, j].borderID);
                        ((LEBlock)blocksArray[i, j]).ChangeRope(((LEBlock)blocksArray[i - 1, j]).ropeID);
                        ((LEBlock)blocksArray[i - 1, j]).ChangeZone(0);
                        ((LEBlock)blocksArray[i - 1, j]).ChangeRope(0);
                        ((LEBlock)blocksArray[i, j]).wallB.wallX = i;
                        ((LEBlock)blocksArray[i, j]).wallB.wallY = j;
                        ((LEBlock)blocksArray[i, j]).wallL.wallX = i;
                        ((LEBlock)blocksArray[i, j]).wallL.wallY = j;

                        if (((LEBlock)blocksArray[i - 1, j]).wallB.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.AddWall();
                            ((LEBlock)blocksArray[i - 1, j]).wallB.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.DeleteWall();
                        }

                        if (((LEBlock)blocksArray[i - 1, j]).wallL.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.AddWall();
                            ((LEBlock)blocksArray[i - 1, j]).wallL.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.DeleteWall();
                        }
                    }
                }
            }
        }
        else
        {
            loadedLevel.levelSizeX--;
            var newArray = new Block[blocksArray.GetLength(0) - 1, blocksArray.GetLength(1)];


            //En alttaki Balonları sil
            for (int i = 0; i < blocksArray.GetLength(1); i++)
            {
                if (blocksArray[0, i].onBalloon != null)
                {
                    Destroy(blocksArray[0, i].onBalloon.gameObject);
                    blocksArray[0, i].onBalloon = null;
                }
            }


            //Alta Kaydır
            for (int i = 0; i < blocksArray.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < blocksArray.GetLength(1); j++)
                {
                    if (blocksArray[i + 1, j].onBalloon != null)
                    {
                        blocksArray[i, j].onBalloon = blocksArray[i + 1, j].onBalloon;
                        blocksArray[i + 1, j].onBalloon = null;

                        blocksArray[i, j].onBalloon.onBlock = blocksArray[i, j];
                        blocksArray[i, j].onBalloon.transform.position = blocksArray[i, j].transform.position;

                        ((LEBlock)blocksArray[i, j]).ChangeZone(blocksArray[i + 1, j].borderID);
                        ((LEBlock)blocksArray[i, j]).ChangeRope(((LEBlock)blocksArray[i + 1, j]).ropeID);
                        ((LEBlock)blocksArray[i + 1, j]).ChangeZone(0);
                        ((LEBlock)blocksArray[i + 1, j]).ChangeRope(0);

                        if (((LEBlock)blocksArray[i + 1, j]).wallB.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.AddWall();
                            ((LEBlock)blocksArray[i + 1, j]).wallB.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallB.DeleteWall();
                        }

                        if (((LEBlock)blocksArray[i + 1, j]).wallL.hasWall)
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.AddWall();
                            ((LEBlock)blocksArray[i + 1, j]).wallL.DeleteWall();
                        }
                        else
                        {
                            ((LEBlock)blocksArray[i, j]).wallL.DeleteWall();
                        }
                    }
                }
            }





            for (int i = 0; i < blocksArray.GetLength(1); i++)
            {
                if (blocksArray[blocksArray.GetLength(0) - 1, i] != null)
                {
                    if (blocksArray[blocksArray.GetLength(0) - 1, i].onBalloon != null)
                    {
                        Destroy(blocksArray[blocksArray.GetLength(0) - 1, i].onBalloon.gameObject);
                    }

                    Destroy(blocksArray[blocksArray.GetLength(0) - 1, i].gameObject);
                    blocksArray[blocksArray.GetLength(0) - 1, i] = null;
                }
            }

            for (int i = 0; i < newArray.GetLength(0); i++)
            {
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = blocksArray[i, j];
                }
            }
            blocksArray = newArray;
        }

        loadedLevel.levelSizeX = blocksArray.GetLength(0);
        loadedLevel.levelSizeY = blocksArray.GetLength(1);



        SetupCamera();
    }

    public void SetupCamera()
    {
        CameraResizer.Instance.ResizeCamera(loadedLevel.levelSizeX, loadedLevel.levelSizeY);
    }

    public void SaveCurrentLevel()
    {
        if (blocksArray != null)
        {
            loadedLevel.itemsList = new List<Item>();
            loadedLevel.wallsLList = new List<Wall>();
            loadedLevel.wallsBList = new List<Wall>();

            if (loadedLevel.ropesList == null)
            {
                loadedLevel.ropesList = new List<Rope>();
            }
            else
            {
                loadedLevel.ropesList.Clear();
            }

            if (loadedLevel.zonesList == null)
            {
                loadedLevel.zonesList = new List<Zone>();
            }
            else
            {
                loadedLevel.zonesList.Clear();
            }

            for (int i = 0; i < loadedLevel.levelSizeX; i++)
            {
                for (int j = 0; j < loadedLevel.levelSizeY; j++)
                {

                    if (blocksArray[i, j] != null)
                    {
                        if (blocksArray[i, j].onBalloon != null)
                        {
                            switch (blocksArray[i, j].onBalloon.GetComponent<Balloon>())
                            {
                                case StandartBalloon srb:
                                    switch (srb.balloonColor)
                                    {
                                        case BalloonColor.Red:
                                            loadedLevel.itemsList.Add(new Item { itemType = ItemType.StandartRedBalloon, itemX = i, itemY = j });
                                            break;
                                        case BalloonColor.Yellow:
                                            loadedLevel.itemsList.Add(new Item { itemType = ItemType.StandartYellowBalloon, itemX = i, itemY = j });
                                            break;
                                        case BalloonColor.Blue:
                                            loadedLevel.itemsList.Add(new Item { itemType = ItemType.StandartBlueBalloon, itemX = i, itemY = j });
                                            break;
                                        case BalloonColor.Green:
                                            loadedLevel.itemsList.Add(new Item { itemType = ItemType.StandartGreenBalloon, itemX = i, itemY = j });
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                case DontTouchBalloon dtb:
                                    loadedLevel.itemsList.Add(new Item { itemType = ItemType.DontTouchBalloon, itemX = i, itemY = j });
                                    break;

                                case MustTouchBalloon mtb:
                                    loadedLevel.itemsList.Add(new Item { itemType = ItemType.MustTouchBalloon, itemX = i, itemY = j });
                                    break;
                                case ReflectiveBalloon rb:
                                    loadedLevel.itemsList.Add(new Item { itemType = ItemType.ReflectiveBalloon, itemX = i, itemY = j });
                                    break;
                                case DoubleBalloon rb:
                                    loadedLevel.itemsList.Add(new Item { itemType = ItemType.DoubleBalloon, itemX = i, itemY = j });
                                    break;

                                default:
                                    Debug.LogError("Henüz Import Edilmeyen Bir Balon Türü Geldi");
                                    break;
                            }
                        }

                        // Save Walls



                        if (((LEBlock)blocksArray[i, j]).wallB.hasWall)
                        {
                            loadedLevel.wallsBList.Add(new Wall { wallX = i, wallY = j });
                        }

                        if (((LEBlock)blocksArray[i, j]).wallL.hasWall)
                        {
                            loadedLevel.wallsLList.Add(new Wall { wallX = i, wallY = j });
                        }

                        // Save Rope

                        if (((LEBlock)blocksArray[i, j]).ropeID != 0)
                        {
                            if (loadedLevel.ropesList.Count < ((LEBlock)blocksArray[i, j]).ropeID)
                            {
                                int fark = ((LEBlock)blocksArray[i, j]).ropeID - loadedLevel.ropesList.Count;
                                for (int m = 0; m < fark; m++)
                                {
                                    loadedLevel.ropesList.Add(new Rope());
                                }
                            }
                            loadedLevel.ropesList[((LEBlock)blocksArray[i, j]).ropeID - 1].ropeConnections.Add(new Connection { connX = i, connY = j });
                        }

                        // Save Zones

                        if (blocksArray[i, j].borderID != 0)
                        {
                            if (loadedLevel.zonesList.Count < blocksArray[i, j].borderID)
                            {
                                int fark = blocksArray[i, j].borderID - loadedLevel.zonesList.Count;
                                for (int m = 0; m < fark; m++)
                                {
                                    loadedLevel.zonesList.Add(new Zone());
                                }
                            }
                            loadedLevel.zonesList[blocksArray[i, j].borderID - 1].zoneConnections.Add(new Connection { connX = i, connY = j });
                        }

                        //
                    }
                }
            }
        }
        Debug.Log("Kaydedilen İtem Sayısı: " + loadedLevel.itemsList.Count);

        loadedLevel.redMoves = Int32.Parse(LevelEditorManager.Instance.redMovesInput.text);
        loadedLevel.blueMoves = Int32.Parse(LevelEditorManager.Instance.blueMovesInput.text);
        loadedLevel.yellowMoves = Int32.Parse(LevelEditorManager.Instance.yellowMovesInput.text);
        loadedLevel.greenMoves = Int32.Parse(LevelEditorManager.Instance.greenMovesInput.text);
        loadedLevel.timeLimit = Int32.Parse(LevelEditorManager.Instance.timeInput.text);
        loadedLevel.maxPoint = Int32.Parse(LevelEditorManager.Instance.maxPointInput.text);


        if (loadedLevel.wallsLList.Count == 0)
        {
            loadedLevel.wallsLList = null;
        }

        if (loadedLevel.wallsBList.Count == 0)
        {
            loadedLevel.wallsBList = null;
        }

        //Clear Empty Ropes
        for (int i = loadedLevel.ropesList.Count - 1; i >= 0; i--)
        {
            if (loadedLevel.ropesList[i].ropeConnections.Count == 0)
            {
                loadedLevel.ropesList.RemoveAt(i);
            }
        }

        //Clear Empty Zones
        for (int i = loadedLevel.zonesList.Count - 1; i >= 0; i--)
        {
            if (loadedLevel.zonesList[i].zoneConnections.Count == 0)
            {
                loadedLevel.zonesList.RemoveAt(i);
            }
        }
    }

    public void DeleteThisLevel()
    {
        LevelEditorManager.Instance.levels.levelsList.Remove(loadedLevel);
        LevelEditorManager.Instance.levelCountText.text = LevelEditorManager.Instance.levels.levelsList.Count.ToString();
        LevelEditorManager.Instance.JSONKaydet();
        LevelEditorManager.Instance.LoadLevel(loadedLevel.levelID);
    }

    public void AddNewLevelHere()
    {
        LevelEditorManager.Instance.levels.levelsList.Insert(loadedLevel.levelID - 1, new Level { levelID = loadedLevel.levelID, levelSizeX = 3, levelSizeY = 5 });
        LevelEditorManager.Instance.levelCountText.text = LevelEditorManager.Instance.levels.levelsList.Count.ToString();
        LevelEditorManager.Instance.JSONKaydet();
        LevelEditorManager.Instance.LoadLevel(loadedLevel.levelID - 1);
    }

    public void ChangeLevelID(string gelenIDText)
    {
        int gelenID = Int32.Parse(gelenIDText);

        LevelEditorManager.Instance.levels.levelsList.Remove(loadedLevel);
        LevelEditorManager.Instance.levels.levelsList.Insert(gelenID - 1, loadedLevel);
        LevelEditorManager.Instance.JSONKaydet();
        LevelEditorManager.Instance.LoadLevel(gelenID);
    }

    public void RotateLevel()
    {
        int xLenght = blocksArray.GetLength(0) - 1;
        int yLenght = blocksArray.GetLength(1) - 1;

        var buffX = 0;
        var buffY = 0;


        foreach (var item in loadedLevel.itemsList)
        {
            buffX = item.itemY;
            buffY = xLenght - item.itemX;


            item.itemX = buffX;
            item.itemY = buffY;
        }

        foreach (var zl in loadedLevel.zonesList)
        {
            foreach (var zc in zl.zoneConnections)
            {
                buffX = zc.connY;
                buffY = xLenght - zc.connX;

                zc.connX = buffX;
                zc.connY = buffY;
            }
        }

        foreach (var item in loadedLevel.ropesList)
        {
            foreach (var a in item.ropeConnections)
            {
                buffX = a.connY;
                buffY = xLenght - a.connX;

                a.connX = buffX;
                a.connY = buffY;
            }
        }

        if (loadedLevel.wallsBList != null)
        {
            foreach (var item in loadedLevel.wallsBList)
            {
                buffX = item.wallY;
                buffY = xLenght - item.wallX;

                item.wallX = buffX;
                item.wallY = buffY;
            }
        }

        if (loadedLevel.wallsLList != null)
        {
            foreach (var item in loadedLevel.wallsLList)
            {
                buffX = item.wallY;
                buffY = xLenght - item.wallX;

                item.wallX = buffX;
                item.wallY = buffY;
            }
        }


        var buffList = loadedLevel.wallsBList;
        loadedLevel.wallsBList = loadedLevel.wallsLList;
        loadedLevel.wallsLList = buffList;

        var buffInt = loadedLevel.levelSizeX;
        loadedLevel.levelSizeX = loadedLevel.levelSizeY;
        loadedLevel.levelSizeY = buffInt;

        LoadLevel(loadedLevel.levelID);
        LevelEditorManager.Instance.JSONKaydet();         
    }
}
