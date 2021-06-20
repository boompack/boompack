using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine;
using System.Diagnostics;
using DG.Tweening;
using System.Threading.Tasks;
using GameAnalyticsSDK;

public class GameManager : Singleton<GameManager>
{

    public float waitTime = 0.1f;
    public LevelLoader levelLoader;

    public List<Block> blocksList;

    public int levelSizeX;
    public int levelSizeY;
    public int redMoves;
    public int yellowMoves;
    public int blueMoves;
    public int greenMoves;

    public float levelTime;
    public float timePass;
    public float timeLeft;
    public bool isPaused = false;


    public float lastMoveTime;

    public int poppedBalloonCountThisRound = 0;
    public bool isBonusBalloonCreatedThisRound = false;

    public bool isBonusesOpen = true;
    public bool isBonusesUseble = true;

    public int bonusBalloon1Count;
    public int bonusBalloon2Count;
    public int bonusBalloon3Count;
    public int bonusBalloon4Count;
    public int bonusBalloon5Count;
    public int bonusBalloon6Count;
    public int bonusBalloon7Count;
    public int bonusBalloon8Count;
    public int bonusBalloon9Count;
    public int bonusBalloon10Count;
    public int bonusBalloon11Count;
    public int bonusBalloon12Count;
    public int bonusBalloon13Count;
    public int bonusBalloon14Count;
    public int bonusBalloon15Count;
    public int bonusBalloon16Count;
    public int bonusBalloon17Count;
    public int bonusBalloon18Count;
    public int bonusBalloon19Count;
    public int bonusBalloon20Count;
    public int bonusBalloon21Count;
    public int bonusBalloon22Count;
    public int bonusBalloon23Count;
    public int bonusBalloon24Count;
    public int bonusBalloon25Count;

    public int bonusUseCount;

    public int continuedBalloonPopping = 0;

    public bool colorLock = false;

    public Block[,] blocksArray;

    public GameObject block;

    public List<Balloon> balloonsList = new List<Balloon>();

    public List<MustTouchBalloon> mustTouchBalloonsList = new List<MustTouchBalloon>();

    public List<Rope> ropesList;

    public GameObject bonusBalloon1;
    public GameObject bonusBalloon2;
    public GameObject bonusBalloon3;
    public GameObject bonusBalloon4;
    public GameObject bonusBalloon5;
    public GameObject bonusBalloon6;
    public GameObject bonusBalloon7;
    public GameObject bonusBalloon8;
    public GameObject bonusBalloon9;
    public GameObject bonusBalloon10;
    public GameObject bonusBalloon11;
    public GameObject bonusBalloon12;
    public GameObject bonusBalloon13;
    public GameObject bonusBalloon14;
    public GameObject bonusBalloon15;
    public GameObject bonusBalloon16;
    public GameObject bonusBalloon17;
    public GameObject bonusBalloon18;
    public GameObject bonusBalloon19;
    public GameObject bonusBalloon20;
    public GameObject bonusBalloon21;
    public GameObject bonusBalloon22;
    public GameObject bonusBalloon23;
    public GameObject bonusBalloon24;
    public GameObject bonusBalloon25;

    public bool balloonUseable = true;
    // Start is called before the first frame update
    void Start()
    {
        GetLevelDetails();
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
#if (!LEVEL_EDITOR)
        PointsUIManager.Instance.Clear();
#endif
        levelTime += 0.9f;
        Invoke("PlaySoundSayac", 0.9f);
        Invoke("PlaySoundSayac", levelTime - 2.9f);     
    }

    void Update()
    {
        TimePass();
    }

    public void TimePass()
    {
        if (!isPaused)
        {
            if (timeLeft > 0f)
            {
                timePass += Time.deltaTime;
                timeLeft = levelTime - timePass;
                LevelDetailsUIManager.Instance.timeText.text =  Mathf.Floor(timeLeft).ToString("F0");


            }
            else
            {
                timeLeft = 0f;
                LevelDetailsUIManager.Instance.timeText.text =  Mathf.Floor(timeLeft).ToString("F0");
            }


        }
        /*
        timePass += Time.deltaTime;
        lastMoveTime += Time.deltaTime;
        
        UITimeUpdate();
        */
    }



    public void PlaySoundSayac()
    {
        SoundManager.Instance.PlaySayac();
    }

    public void PlaySoundSayac3sn()
    {
        SoundManager.Instance.PlaySayac3sn();
    }

    public void PauseContinueTime()
    {
        isPaused = !isPaused;

    }

    public void UITimeUpdate()
    {
        LevelDetailsUIManager.Instance.timeText.text = timePass.ToString("F0");
        //LevelDetailsUIManager.Instance.moveTimeText.text = lastMoveTime.ToString("F0");
    }

    public void TimeReset()
    {
        lastMoveTime = 0;
    }

    public void BonusUseCountReset()
    {
        bonusUseCount = 0;
    }

    public void DestroyGameScene()
    {
        SceneManager.UnloadScene(1);
    }
    public void FindNeighbourBlocks(Wave wave, Balloon poppedBalloon, ref List<Block> wave1, ref List<Block> wave2, ref List<Block> wave3)
    {

        //! Bu işlem pahalı olduğu için daha da hard-coded yazılabilir.
        // Find Wave3
        for (int i = poppedBalloon.onBlock.placeX - poppedBalloon.wave1Lenght - poppedBalloon.wave2Lenght - poppedBalloon.wave3Lenght; i <= poppedBalloon.onBlock.placeX + poppedBalloon.wave1Lenght + poppedBalloon.wave2Lenght + poppedBalloon.wave3Lenght; i++)
        {
            for (int j = poppedBalloon.onBlock.placeY - poppedBalloon.wave1Lenght - poppedBalloon.wave2Lenght - poppedBalloon.wave3Lenght; j <= poppedBalloon.onBlock.placeY + poppedBalloon.wave1Lenght + poppedBalloon.wave2Lenght + poppedBalloon.wave3Lenght; j++)
            {
                if ((i > -1 && j > -1) && (i < levelSizeX && j < levelSizeY))
                {
                    //UnityEngine.Debug.Log("List" + wave3.Count);
                    wave3.Add(blocksArray[i, j]);
                }
            }
        }

        // Find Wave2
        for (int i = poppedBalloon.onBlock.placeX - poppedBalloon.wave1Lenght - poppedBalloon.wave2Lenght; i <= poppedBalloon.onBlock.placeX + poppedBalloon.wave1Lenght + poppedBalloon.wave2Lenght; i++)
        {
            for (int j = poppedBalloon.onBlock.placeY - poppedBalloon.wave1Lenght - poppedBalloon.wave2Lenght; j <= poppedBalloon.onBlock.placeY + poppedBalloon.wave1Lenght + poppedBalloon.wave2Lenght; j++)
            {
                if ((i > -1 && j > -1) && (i < levelSizeX && j < levelSizeY))
                {
                    wave2.Add(blocksArray[i, j]);
                }
            }
        }

        foreach (var item in wave2)
        {
            if (wave3.Contains(item))
            {
                wave3.Remove(item);
            }
        }

        // Find Wave1
        for (int i = poppedBalloon.onBlock.placeX - poppedBalloon.wave1Lenght; i <= poppedBalloon.onBlock.placeX + poppedBalloon.wave1Lenght; i++)
        {
            for (int j = poppedBalloon.onBlock.placeY - poppedBalloon.wave1Lenght; j <= poppedBalloon.onBlock.placeY + poppedBalloon.wave1Lenght; j++)
            {
                if ((i > -1 && j > -1) && (i < levelSizeX && j < levelSizeY))
                {
                    wave1.Add(blocksArray[i, j]);
                }
            }
        }

        foreach (var item in wave1)
        {
            if (wave2.Contains(item))
            {
                wave2.Remove(item);
            }
        }

        wave1.Remove(poppedBalloon.onBlock);

        UnityEngine.Debug.Log("Wave1 Count: " + wave1.Count);
        UnityEngine.Debug.Log("Wave2 Count: " + wave2.Count);
        UnityEngine.Debug.Log("Wave3 Count: " + wave3.Count);
    }


    public void PopBalloon(Balloon poppedBalloon, bool forceEffect = false, bool isMainPopping = true)
    {
        //WriteToUI();
        var coroutine = BalloonPopping(Wave.Wave1, poppedBalloon, forceEffect, isMainPopping);
        StartCoroutine(coroutine);
        poppedBalloon.animator.Play("Pop Animation");
    }

    public IEnumerator BalloonPopping(Wave wave, Balloon poppedBalloon, bool forceEffect = false, bool isMainPopping = true)
    {
        Stopwatch calculatingMoveTime = new Stopwatch();
        calculatingMoveTime.Start();

        continuedBalloonPopping++;
        //Find Balloons
        List<Block> wave1 = new List<Block>();
        List<Block> wave2 = new List<Block>();
        List<Block> wave3 = new List<Block>();

        FindNeighbourBlocks(wave, poppedBalloon, ref wave1, ref wave2, ref wave3);

        CheckWalls(wave, poppedBalloon, ref wave1, ref wave2, ref wave3);
        CheckZones(poppedBalloon, ref wave1, ref wave2, ref wave3);


        //var coroutine = AnimateEffects(wave1, wave2, wave3, poppedBalloon.onBlock);
        //StartCoroutine(coroutine);

        //

        ShakeCamera();

        foreach (var item in wave1)
        {
            if (item.onBalloon != null)
            {
                if (!item.onBalloon.isEffectedFromThisBalloon.Contains(poppedBalloon))
                {
                    int randomTime = Random.Range(0, 100);
                    item.onBalloon.GetEffect(Wave.Wave1, poppedBalloon, randomTime);
                    AnimateEffect(item, poppedBalloon.onBlock, randomTime, 1);
                }

            }
        }
        calculatingMoveTime.Stop();
        UnityEngine.Debug.Log("İlk dalga hesaplama süresi: " + calculatingMoveTime.ElapsedMilliseconds);
        yield return new WaitForSeconds(waitTime);

        foreach (var item in wave2)
        {
            if (item.onBalloon != null)
            {
                if (!item.onBalloon.isEffectedFromThisBalloon.Contains(poppedBalloon))
                {
                    int randomTime = Random.Range(0, 150);
                    item.onBalloon.GetEffect(Wave.Wave2, poppedBalloon, randomTime);
                    AnimateEffect(item, poppedBalloon.onBlock, randomTime, 2);
                }

            }
        }

        yield return new WaitForSeconds(waitTime);

        foreach (var item in wave3)
        {
            if (item.onBalloon != null)
            {
                if (!item.onBalloon.isEffectedFromThisBalloon.Contains(poppedBalloon))
                {
                    int randomTime = Random.Range(0, 250);
                    item.onBalloon.GetEffect(Wave.Wave3, poppedBalloon, randomTime);
                    AnimateEffect(item, poppedBalloon.onBlock, randomTime, 3);
                }

            }
        }

        continuedBalloonPopping--;

        //Open Balloons bools
        if (isMainPopping)
        {
            yield return new WaitForSeconds(0.5f);
            OpenBalloonsLock();
            OpenRopesLock();
            //BonusBalloonCheck();
            CheckEndGame();
            TimeReset();

            while (continuedBalloonPopping != 0)
            {
                yield return 0;
            }
        }

        yield return null;
    }

    private void ShakeCamera()
    {
#if (!LEVEL_EDITOR)
        if (GameSettingsManager.Instance.isVibrationOn)
        {
            Camera.main.DOShakePosition(0.5f, 0.1f, 10, 90, true);
        }
#endif
    }

    public bool CalculateMoves(Balloon poppedBalloon)
    {
#if (!LEVEL_EDITOR)
        switch (poppedBalloon)
        {
            case StandartBalloon sb:
                switch (sb.balloonColor)
                {
                    case BalloonColor.Red:
                        if (redMoves > 0)
                        {
                            redMoves--;
                            LevelDetailsUIManager.Instance.SetRedMovesText(redMoves);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case BalloonColor.Yellow:
                        if (yellowMoves > 0)
                        {
                            yellowMoves--;
                            LevelDetailsUIManager.Instance.SetYellowMovesText(yellowMoves);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case BalloonColor.Blue:
                        if (blueMoves > 0)
                        {
                            blueMoves--;
                            LevelDetailsUIManager.Instance.SetBlueMovesText(blueMoves);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case BalloonColor.Green:
                        if (greenMoves > 0)
                        {
                            greenMoves--;
                            LevelDetailsUIManager.Instance.SetGreenMovesText(greenMoves);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return true;
#else

        switch (poppedBalloon)
        {
            case StandartBalloon sb:
                switch (sb.balloonColor)
                {
                    case BalloonColor.Red:
                        redMoves--;
                        break;
                    case BalloonColor.Yellow:
                        yellowMoves--;
                        break;
                    case BalloonColor.Blue:
                        blueMoves--;
                        break;
                    case BalloonColor.Green:
                        greenMoves--;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return true;
#endif
    }

    public void GetLevelDetails()
    {
        levelSizeX = levelLoader.loadedLevel.levelSizeX;
        levelSizeY = levelLoader.loadedLevel.levelSizeY;
        blocksArray = levelLoader.blocksArray;

        redMoves = levelLoader.loadedLevel.redMoves;
        yellowMoves = levelLoader.loadedLevel.yellowMoves;
        blueMoves = levelLoader.loadedLevel.blueMoves;
        greenMoves = levelLoader.loadedLevel.greenMoves;
        levelTime = levelLoader.loadedLevel.timeLimit;
        timeLeft = levelTime;
        ropesList = levelLoader.loadedLevel.ropesList;

        bonusBalloon1Count = PlayerPrefs.GetInt("BonusBalloon1Count", 0);
        bonusBalloon2Count = PlayerPrefs.GetInt("BonusBalloon2Count", 0);
        bonusBalloon3Count = PlayerPrefs.GetInt("BonusBalloon3Count", 0);
        bonusBalloon4Count = PlayerPrefs.GetInt("BonusBalloon4Count", 0);
        bonusBalloon5Count = PlayerPrefs.GetInt("BonusBalloon5Count", 0);
        bonusBalloon6Count = PlayerPrefs.GetInt("BonusBalloon6Count", 0);
        bonusBalloon7Count = PlayerPrefs.GetInt("BonusBalloon7Count", 0);
        bonusBalloon8Count = PlayerPrefs.GetInt("BonusBalloon8Count", 0);
        bonusBalloon9Count = PlayerPrefs.GetInt("BonusBalloon9Count", 0);
        bonusBalloon10Count = PlayerPrefs.GetInt("BonusBalloon10Count", 0);
        bonusBalloon11Count = PlayerPrefs.GetInt("BonusBalloon11Count", 0);
        bonusBalloon12Count = PlayerPrefs.GetInt("BonusBalloon12Count", 0);
        bonusBalloon13Count = PlayerPrefs.GetInt("BonusBalloon13Count", 0);
        bonusBalloon14Count = PlayerPrefs.GetInt("BonusBalloon14Count", 0);
        bonusBalloon15Count = PlayerPrefs.GetInt("BonusBalloon15Count", 0);
        bonusBalloon16Count = PlayerPrefs.GetInt("BonusBalloon16Count", 0);
        bonusBalloon17Count = PlayerPrefs.GetInt("BonusBalloon17Count", 0);
        bonusBalloon18Count = PlayerPrefs.GetInt("BonusBalloon18Count", 0);
        bonusBalloon19Count = PlayerPrefs.GetInt("BonusBalloon19Count", 0);
        bonusBalloon20Count = PlayerPrefs.GetInt("BonusBalloon20Count", 0);
        bonusBalloon21Count = PlayerPrefs.GetInt("BonusBalloon21Count", 0);
        bonusBalloon22Count = PlayerPrefs.GetInt("BonusBalloon22Count", 0);
        bonusBalloon23Count = PlayerPrefs.GetInt("BonusBalloon23Count", 0);
        bonusBalloon24Count = PlayerPrefs.GetInt("BonusBalloon24Count", 0);
        bonusBalloon25Count = PlayerPrefs.GetInt("BonusBalloon25Count", 0);


        BonusBalloonUIManager.Instance.RefleshCounts();

        WriteToUI();

    }

    public void WriteToUI()
    {
        LevelDetailsUIManager.Instance.SetRedMovesText(redMoves);
        LevelDetailsUIManager.Instance.SetYellowMovesText(yellowMoves);
        LevelDetailsUIManager.Instance.SetBlueMovesText(blueMoves);
        LevelDetailsUIManager.Instance.SetGreenMovesText(greenMoves);
        LevelDetailsUIManager.Instance.timeText.text = levelTime.ToString();
    }

    public void BonusBalloonCheck()
    {
        UnityEngine.Debug.Log(poppedBalloonCountThisRound + " balloon popped this round");

        if (!isBonusesOpen)
        {
            return;
        }

        if (poppedBalloonCountThisRound >= 34)
        {
            bonusBalloon25Count++;
        }
        else
        {
            switch (poppedBalloonCountThisRound)
            {
                case 33: bonusBalloon24Count++; break;
                case 32: bonusBalloon23Count++; break;
                case 31: bonusBalloon22Count++; break;
                case 30: bonusBalloon21Count++; break;
                case 29: bonusBalloon20Count++; break;
                case 28: bonusBalloon19Count++; break;
                case 27: bonusBalloon18Count++; break;
                case 26: bonusBalloon17Count++; break;
                case 25: bonusBalloon16Count++; break;
                case 24: bonusBalloon15Count++; break;
                case 23: bonusBalloon14Count++; break;
                case 22: bonusBalloon13Count++; break;
                case 21: bonusBalloon12Count++; break;
                case 20: bonusBalloon11Count++; break;
                case 19: bonusBalloon10Count++; break;
                case 18: bonusBalloon9Count++; break;
                case 17: bonusBalloon8Count++; break;
                case 16: bonusBalloon7Count++; break;
                case 15: bonusBalloon6Count++; break;
                case 14: bonusBalloon5Count++; break;
                case 13: bonusBalloon4Count++; break;
                case 12: bonusBalloon3Count++; break;
                case 11: bonusBalloon2Count++; break;
                case 10: bonusBalloon1Count++; break;
            }
        }

        BonusBalloonUIManager.Instance.RefleshCounts();
    }
    public void CheckZones(Balloon poppedBalloon, ref List<Block> wave1, ref List<Block> wave2, ref List<Block> wave3)
    {
        for (int i = wave1.Count - 1; i >= 0; i--)
        {
            if (wave1[i].borderID != poppedBalloon.onBlock.borderID)
            {
                wave1.RemoveAt(i);
            }
        }

        for (int i = wave2.Count - 1; i >= 0; i--)
        {
            if (wave2[i].borderID != poppedBalloon.onBlock.borderID)
            {
                wave2.RemoveAt(i);
            }
        }

        for (int i = wave3.Count - 1; i >= 0; i--)
        {
            if (wave3[i].borderID != poppedBalloon.onBlock.borderID)
            {
                wave3.RemoveAt(i);
            }
        }
    }

    public void CheckWalls(Wave wave, Balloon poppedBalloon, ref List<Block> wave1, ref List<Block> wave2, ref List<Block> wave3)
    {
        for (int i = wave1.Count - 1; i >= 0; i--)
        {
            /*
            Debug.DrawLine(poppedBalloon.transform.position, wave1[i].transform.position);
            Debug.Log("a1");
            Debug.Break();
            */

            int layerMask = LayerMask.GetMask("Wall");
            RaycastHit2D hit = Physics2D.Raycast(poppedBalloon.transform.position, (wave1[i].transform.position - poppedBalloon.transform.position), Vector2.Distance(poppedBalloon.transform.position, wave1[i].transform.position), layerMask);
            if (hit.collider != null)
            {
                //Debug.Log("Arada duvar Var...");
                wave1.Remove(wave1[i]);
            }
        }

        for (int i = wave2.Count - 1; i >= 0; i--)
        {
            //Debug.DrawLine(poppedBalloon.transform.position, wave2[i].transform.position);

            int layerMask = LayerMask.GetMask("Wall");
            RaycastHit2D hit = Physics2D.Raycast(poppedBalloon.transform.position, (wave2[i].transform.position - poppedBalloon.transform.position), Vector2.Distance(poppedBalloon.transform.position, wave2[i].transform.position), layerMask);
            if (hit.collider != null)
            {
                //Debug.Log("Arada duvar Var...");
                wave2.Remove(wave2[i]);
            }
        }

        for (int i = wave3.Count - 1; i >= 0; i--)
        {
            //Debug.DrawLine(poppedBalloon.transform.position, wave3[i].transform.position);

            int layerMask = LayerMask.GetMask("Wall");
            RaycastHit2D hit = Physics2D.Raycast(poppedBalloon.transform.position, (wave3[i].transform.position - poppedBalloon.transform.position), Vector2.Distance(poppedBalloon.transform.position, wave3[i].transform.position), layerMask);
            if (hit.collider != null)
            {
                //Debug.Log("Arada duvar Var...");
                wave3.Remove(wave3[i]);
            }
        }
    }

    public void OpenBalloonsLock()
    {
        foreach (var item in balloonsList)
        {
            item.isEffectedFromThisBalloon.Clear();
        }
    }

    public void OpenRopesLock()
    {
        if (ropesList != null)
        {
            foreach (var item in ropesList)
            {
                item.isEffectedFromThisBalloon.Clear();
            }
        }
    }

    public void AnimateStars()
    {
        foreach (var item in balloonsList)
        {
            item.gameObject.transform.DOMove(new Vector3(item.transform.position.x,item.transform.position.y + 25, -1), 1.5f).SetEase(Ease.InCirc).SetDelay(Random.Range(0f,0.35f));
            item.GetComponent<SpriteRenderer>().DOColor(new Color(0,0,0,0),0.75f).SetEase(Ease.InCirc).SetDelay(Random.Range(0f,0.35f));
        }
    }

    public void CheckEndGame()
    {
        bool isThereActiveBalloon = false;
        UnityEngine.Debug.Log("Toplam Balon Sayısı: " + balloonsList.Count);
        foreach (var item in balloonsList)
        {
            if (item.balloonState != BalloonState.Dead && item.balloonState != BalloonState.Popped)
            {
                isThereActiveBalloon = true;
            }
        }

        if (!isThereActiveBalloon)
        {
            PointsManager.Instance.CalculatePoints(Mathf.RoundToInt(timeLeft), RepeatManager.Instance.repeatCount, bonusUseCount, true);
            AnimateStars();
            GameEndScreens(true);
            isPaused = true;           
            foreach (RewardedZones rewardedZones in AdsManager.Instance.rewardedZones)
            {
                if (levelLoader.loadedLevel.levelID == rewardedZones.startLevel)
                {
                    return;
                }

            }

            if (PlayerPrefs.GetInt("PREMIUM") == 1)
            {
                if (levelLoader.loadedLevel.levelID != 300)
                {
                    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isLocked = false;
                    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isPlayable = true;
                    SaveManager.Instance.SaveLevelStats();
                }
            }

            else
            {
                if (levelLoader.loadedLevel.levelID < 240)
                {
                    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isLocked = false;
                    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isPlayable = true;
                    SaveManager.Instance.SaveLevelStats();
                }
                else
                {
                    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isLocked = false;
                    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isPlayable = false;
                    SaveManager.Instance.SaveLevelStats();
                }
            }




            
        }
        else
        {
            if (!CheckMoves())
            {
                PointsManager.Instance.CalculatePoints(Mathf.RoundToInt(timeLeft), RepeatManager.Instance.repeatCount, bonusUseCount, false);
                GameEndScreens(false);
                isPaused = true; 
            }
            else
            {
                bool isThereToucableBalloon = false;
                foreach (var item in balloonsList)
                {
                    if ((item.balloonState != BalloonState.Dead && item.balloonState != BalloonState.Popped) && item.isTouchable == true)
                    {
                        isThereToucableBalloon = true;
                    }
                }

                if (!isThereToucableBalloon)
                {
                    PointsManager.Instance.CalculatePoints(Mathf.RoundToInt(timeLeft), RepeatManager.Instance.repeatCount, bonusUseCount, false);
                    GameEndScreens(false);
                    isPaused = true; 
                }


            }
        }

    }


    /*
        public bool MustTouchControl()
        {
            bool theypopped = true;
            foreach (var item in mustTouchBalloonsList)
            {
                if (item.balloonState != BalloonState.Popped)
                {
                    theypopped = false;
                }
            }
            return theypopped;
        }
        */

    public void GameEndScreens(bool isWin)
    {
        if (isWin)
        {
            UnityEngine.Debug.Log(levelLoader.loadedLevel.levelID);
            //if(PointsManager.Instance.point > HighScoreManager.Instance.highscores[levelLoader.loadedLevel.levelID-1])
            //{
            //    PlayerPrefs.SetInt("LevelHighScore" + (levelLoader.loadedLevel.levelID - 1), PointsManager.Instance.point);
            //    HighScoreManager.Instance.highscores[levelLoader.loadedLevel.levelID - 1] = PlayerPrefs.GetInt("LevelHighScore" + (levelLoader.loadedLevel.levelID - 1));
            //}
            SaveManager.Instance.SaveLevelStat(levelLoader.loadedLevel.levelID, PointsManager.Instance.point, bonusUseCount, RepeatManager.Instance.repeatCount, Mathf.RoundToInt(timeLeft));
            WinScreenManager.Instance.SetGraphics(bonusUseCount, RepeatManager.Instance.repeatCount, Mathf.RoundToInt(timeLeft));
            WinScreenManager.Instance.NextLevelText.text = GameManager.Instance.levelLoader.loadedLevel.levelID != 300 ? "Next Level" : "Next";
            //CheckEndEpisode();
            DoozyManager.Instance.SendGameEvent("WinGame");
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Yardimcilar.GetLevelState(levelLoader.loadedLevel.levelID).ToString(), levelLoader.loadedLevel.levelID.ToString(), PointsManager.Instance.point);

            //if (levelLoader.loadedLevel.levelID != 300)
            //{
            //    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isLocked = false;
            //    SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isPlayable = true;
            //    SaveManager.Instance.SaveLevelStats();
            //}

            //if (PlayerPrefs.GetInt("PREMIUM") == 1)
            //{
            //    if (levelLoader.loadedLevel.levelID == 240)
            //    {
            //        SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isLocked = false;
            //        SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isPlayable = true;
            //        SaveManager.Instance.SaveLevelStats();
            //    }
            //}

            //else
            //{
            //    if (levelLoader.loadedLevel.levelID == 240)
            //    {
            //        SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isLocked = false;
            //        SaveManager.Instance.levelStats.levelStatsDict[levelLoader.loadedLevel.levelID + 1].isPlayable = false;
            //        SaveManager.Instance.SaveLevelStats();
            //    }
            //}

        }
        else
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, Yardimcilar.GetLevelState(levelLoader.loadedLevel.levelID).ToString(), levelLoader.loadedLevel.levelID.ToString(), PointsManager.Instance.point);

            DoozyManager.Instance.SendGameEvent("LoseGame");
            AdsManager.Instance.ShowInterstitialAd(RepeatManager.Instance.repeatCount);
        }
    }

    public void CheckEndEpisode()
    {
        if (levelLoader.loadedLevel.levelID == 300)
        {
            DoozyManager.Instance.SendGameEvent("LastGame");
        }
        else if ((levelLoader.loadedLevel.levelID % 20) == 0)
        {
            DoozyManager.Instance.SendGameEvent("LastEpisode");
        }
        else
        {
            DoozyManager.Instance.SendGameEvent("WinGame");
        }
    }

    public bool CheckMoves()
    {
        if ((redMoves == 0) && (yellowMoves == 0) && (greenMoves == 0) && (blueMoves == 0))
        {
            return false;
        }

        if (!HasAvailableMove())
        {
            return false;
        }

        return true;
    }

    public bool HasColorBalloon(BalloonColor color)
    {
        foreach (var item in balloonsList)
        {
            if (item is StandartBalloon)
            {
                if (((StandartBalloon)item).balloonColor == color)
                {
                    if (item.balloonState != BalloonState.Popped && item.balloonState != BalloonState.Dead)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool HasAvailableMove()
    {
        bool hasAvailableMove = false;

        if (redMoves > 0 && HasColorBalloon(BalloonColor.Red))
        {
            UnityEngine.Debug.Log("Halaa Kırmızı Balon ve Kırmızı Balon Patlatma Hakkı Var");
            hasAvailableMove = true;
        }

        if (blueMoves > 0 && HasColorBalloon(BalloonColor.Blue))
        {
            UnityEngine.Debug.Log("Halaa Mavi Balon ve Mavi Balon Patlatma Hakkı Var");
            hasAvailableMove = true;
        }

        if (greenMoves > 0 && HasColorBalloon(BalloonColor.Green))
        {
            UnityEngine.Debug.Log("Halaa Yeşil Balon ve Yeşil Balon Patlatma Hakkı Var");
            hasAvailableMove = true;
        }

        if (yellowMoves > 0 && HasColorBalloon(BalloonColor.Yellow))
        {
            UnityEngine.Debug.Log("Halaa Sarı Balon ve Sarı Balon Patlatma Hakkı Var");
            hasAvailableMove = true;
        }

        return hasAvailableMove;
    }

    public IEnumerator AnimateEffects(List<Block> wave1, List<Block> wave2, List<Block> wave3, Block poppedBlock)
    {


        foreach (var item in wave1)
        {
            if (item.onBalloon != null)
            {
                Vector3 dir = new Vector3(item.placeX, item.placeY, 1) - poppedBlock.transform.position;
                item.onBalloon.transform.DOPunchPosition(dir * 0.25f, 0.75f, 5, 0.4f, false);
            }
        }


        yield return new WaitForSeconds(waitTime);

        foreach (var item in wave2)
        {
            if (item.onBalloon != null)
            {
                Vector3 dir = new Vector3(item.placeX, item.placeY, 1) - poppedBlock.transform.position;
                item.onBalloon.transform.DOPunchPosition(dir * 0.075f, 0.65f, 5, 0.3f, false);
            }
        }

        yield return new WaitForSeconds(waitTime);

        foreach (var item in wave3)
        {
            if (item.onBalloon != null)
            {
                Vector3 dir = new Vector3(item.placeX, item.placeY, 1) - poppedBlock.transform.position;
                item.onBalloon.transform.DOPunchPosition(dir * 0.015f, 0.5f, 5, 0.2f, false);
            }
        }

        yield return null;

    }

    public async void AnimateEffect(Block gelenBlock, Block poppedBlock, int randomTime, int wave)
    {
        if (randomTime != 0)
        {
            await Task.Delay(randomTime);
        }
        if (gelenBlock.onBalloon != null)
        {
            Vector3 dir = gelenBlock.transform.position - poppedBlock.transform.position;

            switch (wave)
            {
                case 1:
                    gelenBlock.onBalloon.transform.DOKill(true);
                    Tweener anim = gelenBlock.onBalloon.transform.DOPunchPosition(dir * 0.25f, 0.75f, 5, 0.4f, false);
                    //anim.ChangeValues(gelenBlock.transform.position, gelenBlock.transform.position);
                    break;
                case 2:
                    gelenBlock.onBalloon.transform.DOKill(true);
                    Tweener anim2 = gelenBlock.onBalloon.transform.DOPunchPosition(dir * 0.075f, 0.65f, 5, 0.3f, false);
                    //anim2.ChangeValues(gelenBlock.transform.position, gelenBlock.transform.position);
                    break;
                case 3:
                default:
                    gelenBlock.onBalloon.transform.DOKill(true);
                    gelenBlock.onBalloon.transform.DOPunchPosition(dir * 0.015f, 0.5f, 5, 0.2f, false);
                    Tweener anim3 = gelenBlock.onBalloon.transform.DOPunchPosition(dir * 0.015f, 0.5f, 5, 0.2f, false);
                    //anim3.ChangeValues(gelenBlock.transform.position, gelenBlock.transform.position);
                    break;

            }

        }
    }
    private void OnEnable()
    {
        //Start listening for game events
        Message.AddListener<GameEventMessage>(OnMessage);
    }

    private void OnDisable()
    {
        //Stop listening for game events
        Message.RemoveListener<GameEventMessage>(OnMessage);
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("BonusBalloon1Count", bonusBalloon1Count);
        PlayerPrefs.SetInt("BonusBalloon2Count", bonusBalloon2Count);
        PlayerPrefs.SetInt("BonusBalloon3Count", bonusBalloon3Count);
        PlayerPrefs.SetInt("BonusBalloon4Count", bonusBalloon4Count);
        PlayerPrefs.SetInt("BonusBalloon5Count", bonusBalloon5Count);
        PlayerPrefs.SetInt("BonusBalloon6Count", bonusBalloon6Count);
        PlayerPrefs.SetInt("BonusBalloon7Count", bonusBalloon7Count);
        PlayerPrefs.SetInt("BonusBalloon8Count", bonusBalloon8Count);
        PlayerPrefs.SetInt("BonusBalloon9Count", bonusBalloon9Count);
        PlayerPrefs.SetInt("BonusBalloon10Count", bonusBalloon10Count);
        PlayerPrefs.SetInt("BonusBalloon11Count", bonusBalloon11Count);
        PlayerPrefs.SetInt("BonusBalloon12Count", bonusBalloon12Count);
        PlayerPrefs.SetInt("BonusBalloon13Count", bonusBalloon13Count);
        PlayerPrefs.SetInt("BonusBalloon14Count", bonusBalloon14Count);
        PlayerPrefs.SetInt("BonusBalloon15Count", bonusBalloon15Count);
        PlayerPrefs.SetInt("BonusBalloon16Count", bonusBalloon16Count);
        PlayerPrefs.SetInt("BonusBalloon17Count", bonusBalloon17Count);
        PlayerPrefs.SetInt("BonusBalloon18Count", bonusBalloon18Count);
        PlayerPrefs.SetInt("BonusBalloon19Count", bonusBalloon19Count);
        PlayerPrefs.SetInt("BonusBalloon20Count", bonusBalloon20Count);
        PlayerPrefs.SetInt("BonusBalloon21Count", bonusBalloon21Count);
        PlayerPrefs.SetInt("BonusBalloon22Count", bonusBalloon22Count);
        PlayerPrefs.SetInt("BonusBalloon23Count", bonusBalloon23Count);
        PlayerPrefs.SetInt("BonusBalloon24Count", bonusBalloon24Count);
        PlayerPrefs.SetInt("BonusBalloon25Count", bonusBalloon25Count);
        PlayerPrefs.Save();

    }

    private void OnMessage(GameEventMessage message)
    {
        if (message == null) return;
        if (message.Source == null) return;



        if (message.EventName == "GoMainMenu")
        {
            SavePrefs();
            StopAllCoroutines();
            DestroyGameScene();
            RepeatManager.Instance.repeatCount = 0;
        }

        if (message.EventName == "ReloadLevel")
        {
            if (continuedBalloonPopping != 0)
            {
                return;
            }
            SavePrefs();
            StopAllCoroutines();
            DestroyGameScene();
            LevelManager.Instance.LoadLevel(levelLoader.loadedLevel.levelID);
        }
    }

    public void ReloadLevel()
    {
        if (continuedBalloonPopping != 0)
        {
            return;
        }
        SavePrefs();
        StopAllCoroutines();
        DestroyGameScene();
        LevelManager.Instance.LoadLevel(levelLoader.loadedLevel.levelID);
    }

    public void LoadNextLevel()
    {
        if (levelLoader.loadedLevel.levelID != 300)
        {
            if (continuedBalloonPopping != 0)
            {
                return;
            }
            SavePrefs();
            StopAllCoroutines();
            DestroyGameScene();
            LevelManager.Instance.LoadLevel(levelLoader.loadedLevel.levelID + 1);
        }
        else
        {
            if (continuedBalloonPopping != 0)
            {
                return;
            }
            SavePrefs();
            StopAllCoroutines();
            DestroyGameScene();
            LevelManager.Instance.LoadLevel(levelLoader.loadedLevel.levelID);
        }




    }

    public void LoadPreviousLevel()
    {
        if (continuedBalloonPopping != 0)
        {
            return;
        }
        SavePrefs();
        StopAllCoroutines();
        DestroyGameScene();
        LevelManager.Instance.LoadLevel(levelLoader.loadedLevel.levelID - 1);
    }

    public void LoadThisLevel(int thisID)
    {
        if (continuedBalloonPopping != 0)
        {
            return;
        }
        SavePrefs();
        StopAllCoroutines();
        DestroyGameScene();
        LevelManager.Instance.LoadLevel(thisID);
    }
}

public enum Wave : byte
{
    Wave1 = 100,
    Wave2 = 50,
    Wave3 = 25,
}
