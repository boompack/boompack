using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
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

    public GameObject wallBPrefab;
    public GameObject wallLPrefab;

    public GameObject wallBEmptyPrefab;
    public GameObject wallLEmptyPrefab;


    public Block[,] blocksArray;


    void Start()
    {
        SceneChange();
        LoadLevel();
        SetupCamera();
        CheckHealthTexts();
    }

    public void LoadLevel()
    {
        Stopwatch levelLoadingTime = new Stopwatch();
        levelLoadingTime.Start();
        loadedLevel = LevelManager.Instance.playedLevel;

        if (loadedLevel == null)
        {
            UnityEngine.Debug.Log("aaaaaa");
        }

        LevelDetailsUIManager.Instance.loadLevel.text = loadedLevel.levelID.ToString();
        LevelDetailsUIManager.Instance.SetLevelDetails(loadedLevel.redMoves, loadedLevel.yellowMoves, loadedLevel.blueMoves, loadedLevel.greenMoves);


#if (!LEVEL_EDITOR)
        UIManager.Instance.UpdateLevelText(loadedLevel.levelID);

        //Color Manager
        StateColorManager.Instance.SetStateColors(loadedLevel.levelID, this);
#endif



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
                    createdGameObject = Instantiate(StandartRedBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;
                case ItemType.StandartBlueBalloon:
                    createdGameObject = Instantiate(StandartBlueBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;
                case ItemType.StandartYellowBalloon:
                    createdGameObject = Instantiate(StandartYellowBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;
                case ItemType.StandartGreenBalloon:
                    createdGameObject = Instantiate(StandartGreenBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;
                case ItemType.DontTouchBalloon:
                    createdGameObject = Instantiate(DontTouchBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;
                case ItemType.MustTouchBalloon:
                    createdGameObject = Instantiate(MustTouchBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;
                case ItemType.ReflectiveBalloon:
                    createdGameObject = Instantiate(ReflectiveBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;
                case ItemType.DoubleBalloon:
                    createdGameObject = Instantiate(DoubleBalloonPrefab, new Vector3(balloon.itemX, balloon.itemY, -1), Quaternion.identity);
                    break;

                default:
                    UnityEngine.Debug.Log("Henüz Import Edilmeyen Bir Balon Türü Geldi");
                    break;
            }

            Balloon tempBalloon = createdGameObject.GetComponent<Balloon>();
            GameManager.Instance.balloonsList.Add(tempBalloon);

            blocksArray[balloon.itemX, balloon.itemY].GetComponent<Block>().onBalloon = tempBalloon;
            //tempBalloon.transform.position = blocksArray[balloon.itemX, balloon.itemY].transform.position;
            tempBalloon.onBlock = blocksArray[balloon.itemX, balloon.itemY];
        }

        // Create Walls

        if (loadedLevel.wallsBList != null)
        {
            foreach (var item in loadedLevel.wallsBList)
            {
                Vector3 positionCalculated = blocksArray[item.wallX, item.wallY].gameObject.transform.position + new Vector3(0, -0.5f, 0);
                Instantiate(wallBPrefab, positionCalculated, Quaternion.identity);
            }
        }

        if (loadedLevel.wallsLList != null)
        {
            foreach (var item in loadedLevel.wallsLList)
            {
                Vector3 positionCalculated = blocksArray[item.wallX, item.wallY].gameObject.transform.position + new Vector3(-0.5f, 0, 0);
                Instantiate(wallLPrefab, positionCalculated, Quaternion.identity);
            }
        }


        // Create Rope

        if (loadedLevel.ropesList != null)
        {
            foreach (var item in loadedLevel.ropesList)
            {
                item.ropedBlocks.Clear();
                foreach (var conns in item.ropeConnections)
                {
                    blocksArray[conns.connX, conns.connY].onRope = item;
                    blocksArray[conns.connX, conns.connY].onBalloon.isTouchable = false;
                    if (blocksArray[conns.connX, conns.connY].onBalloon is StandartBalloon)
                    {
                        (blocksArray[conns.connX, conns.connY].onBalloon as StandartBalloon).balloonColor = BalloonColor.Rope;
                    }
                    //blocksArray[conns.connX, conns.connY].GetComponent<SpriteRenderer>().color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
                    item.ropedBlocks.Add(blocksArray[conns.connX, conns.connY]);
                    blocksArray[conns.connX, conns.connY].onBalloon.GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.RopeBalloon100;
                }

            }
        }

        // Create Zones

        if (loadedLevel.zonesList != null)
        {
            foreach (var item in loadedLevel.zonesList)
            {
                foreach (var conns in item.zoneConnections)
                {
                    blocksArray[conns.connX, conns.connY].borderID = loadedLevel.zonesList.IndexOf(item) + 1;
                    blocksArray[conns.connX, conns.connY].GetComponent<SpriteRenderer>().enabled = true;
                    blocksArray[conns.connX, conns.connY].GetComponent<SpriteRenderer>().color = StateColorManager.Instance.GetStateWallBackgroundColor();
                }

            }
        }

        // Creating Walls

        for (int i = 0; i < loadedLevel.levelSizeX - 1; i++)
        {
            for (int j = 0; j < loadedLevel.levelSizeY; j++)
            {
                if (blocksArray[i, j].borderID != blocksArray[i + 1, j].borderID)
                {
                    UnityEngine.Debug.Log("Aynı Değil");
                    Instantiate(wallLEmptyPrefab, (blocksArray[i + 1, j].gameObject.transform.position + new Vector3(-0.5f, 0, 0)), Quaternion.identity);
                }

            }
        }

        for (int i = 0; i < loadedLevel.levelSizeX; i++)
        {
            for (int j = 0; j < loadedLevel.levelSizeY - 1; j++)
            {
                if (blocksArray[i, j].borderID != blocksArray[i, j + 1].borderID)
                {
                    UnityEngine.Debug.Log("Aynı Değil X");
                    Instantiate(wallBEmptyPrefab, (blocksArray[i, j + 1].gameObject.transform.position + new Vector3(0, -0.5f, 0)), Quaternion.identity);
                }

            }
        }

        for (int i = 0; i < loadedLevel.levelSizeX; i++)
        {
            if (blocksArray[i, loadedLevel.levelSizeY - 1].borderID != 0)
            {
                Instantiate(wallBEmptyPrefab, (blocksArray[i, loadedLevel.levelSizeY - 1].gameObject.transform.position + new Vector3(0, +0.5f, 0)), Quaternion.identity);
            }

        }

        for (int i = 0; i < loadedLevel.levelSizeX; i++)
        {
            if (blocksArray[i, 0].borderID != 0)
            {
                Instantiate(wallBEmptyPrefab, (blocksArray[i, 0].gameObject.transform.position + new Vector3(0, -0.5f, 0)), Quaternion.identity);
            }

        }

        for (int i = 0; i < loadedLevel.levelSizeY; i++)
        {
            if (blocksArray[0, i].borderID != 0)
            {
                Instantiate(wallLEmptyPrefab, (blocksArray[0, i].gameObject.transform.position + new Vector3(-0.5f, 0, 0)), Quaternion.identity);
            }

        }

        for (int i = 0; i < loadedLevel.levelSizeY; i++)
        {
            if (blocksArray[loadedLevel.levelSizeX - 1, i].borderID != 0)
            {
                Instantiate(wallLEmptyPrefab, (blocksArray[loadedLevel.levelSizeX - 1, i].gameObject.transform.position + new Vector3(+0.5f, 0, 0)), Quaternion.identity);
            }

        }


        levelLoadingTime.Stop();
        UnityEngine.Debug.Log("Level Yaratılma Süresi: " + levelLoadingTime.ElapsedMilliseconds);
    }

    public void SetupCamera()
    {
        CameraResizer.Instance.ResizeCamera(loadedLevel.levelSizeX, loadedLevel.levelSizeY);
        CameraResizer.Instance.BorderCalculate(loadedLevel.levelSizeX, loadedLevel.levelSizeY);
    }

    public void CheckHealthTexts()
    {
        #if (!LEVEL_EDITOR)
        if(PlayerPrefs.GetInt("isShowLivesOn", 1) == 1)
        {
            UIManager.Instance.ShowHealthTexts();
            UnityEngine.Debug.Log("Health Açılması İstendi");
        }
        #endif

    }

    public void SceneChange()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }


}
