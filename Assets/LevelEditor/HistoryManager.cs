using MessagePack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : Singleton<HistoryManager>
{

    public int playedLevel = 1;

    public byte[] yedekByte;
    public Level yedek;

    public bool hasLevelChanged = false;

    public List<Order> changesList = new List<Order>();

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            RevertChange();
        }
    }

    public void AddOrder(Order comingOrder)
    {
        hasLevelChanged = true;
        changesList.Add(comingOrder);
    }

    public void RevertChange()
    {
        if (changesList.Count >= 1)
        {
            changesList[changesList.Count - 1].Revert();
            changesList.RemoveAt(changesList.Count - 1);
        }
        else
        {
            Debug.Log("Geriye Hareket Kalmad�");
        }
    }

    public void YedekOlustur()
    {
        yedekByte = MessagePackSerializer.Serialize(LELevelLoader.Instance.loadedLevel);
        yedek = MessagePackSerializer.Deserialize<Level>(yedekByte);
    }

    public void YedekGeriYukle()
    {
        Debug.Log("De�i�iklikler Geri Y�klenecek");
        LELevelLoader.Instance.loadedLevel = MessagePackSerializer.Deserialize<Level>(yedekByte);
        LevelEditorManager.Instance.levels.levelsList[LELevelLoader.Instance.loadedLevel.levelID - 1] = MessagePackSerializer.Deserialize<Level>(yedekByte);
        LELevelLoader.Instance.LoadLevel(LELevelLoader.Instance.loadedLevel.levelID);
        hasLevelChanged = false;
        changesList.Clear();
    }

    public bool HasLevelChanged()
    {
        if(hasLevelChanged)
        {
            return true;
        }

        
        if(LELevelLoader.Instance.loadedLevel.redMoves.ToString() != LevelEditorManager.Instance.redMovesInput.text)
        {
            return true;
        }

        if(LELevelLoader.Instance.loadedLevel.blueMoves.ToString() != LevelEditorManager.Instance.blueMovesInput.text)
        {
            return true;
        }

        if(LELevelLoader.Instance.loadedLevel.greenMoves.ToString() != LevelEditorManager.Instance.greenMovesInput.text)
        {
            return true;
        }

        if(LELevelLoader.Instance.loadedLevel.yellowMoves.ToString() != LevelEditorManager.Instance.yellowMovesInput.text)
        {
            return true;
        }

        if(LELevelLoader.Instance.loadedLevel.timeLimit.ToString() != LevelEditorManager.Instance.timeInput.text)
        {
            return true;
        }

        if(LELevelLoader.Instance.loadedLevel.maxPoint.ToString() != LevelEditorManager.Instance.maxPointInput.text)
        {
            return true;
        }
        

        return false;
    }

    public ItemType ItemTypeConverter(GameObject balloon)
    {
        switch (balloon.GetComponent<Balloon>())
        {
            case StandartBalloon srb:
                switch (srb.balloonColor)
                {
                    case BalloonColor.Red:
                        return ItemType.StandartRedBalloon;
                    case BalloonColor.Yellow:
                        return ItemType.StandartYellowBalloon;
                    case BalloonColor.Blue:
                        return ItemType.StandartBlueBalloon;
                    case BalloonColor.Green:
                        return ItemType.StandartGreenBalloon;
                    default:
                        break;
                }
                break;

            case DontTouchBalloon dtb:
                return ItemType.DontTouchBalloon;

            case MustTouchBalloon mtb:
                return ItemType.MustTouchBalloon;
            case ReflectiveBalloon rb:
                return ItemType.ReflectiveBalloon;
            case DoubleBalloon rb:
                return ItemType.DoubleBalloon;
            default:
                Debug.LogError("Bilinmeyen bir balon t�r� geldi!!!");
                return ItemType.Null;
        }
        return ItemType.Null;
    }

    public ItemType ItemTypeConverter(Balloon balloon)
    {
        switch (balloon)
        {
            case StandartBalloon srb:
                switch (srb.balloonColor)
                {
                    case BalloonColor.Red:
                        return ItemType.StandartRedBalloon;
                    case BalloonColor.Yellow:
                        return ItemType.StandartYellowBalloon;
                    case BalloonColor.Blue:
                        return ItemType.StandartBlueBalloon;
                    case BalloonColor.Green:
                        return ItemType.StandartGreenBalloon;
                    default:
                        break;
                }
                break;

            case DontTouchBalloon dtb:
                return ItemType.DontTouchBalloon;

            case MustTouchBalloon mtb:
                return ItemType.MustTouchBalloon;
            case ReflectiveBalloon rb:
                return ItemType.ReflectiveBalloon;
            case DoubleBalloon rb:
                return ItemType.DoubleBalloon;
            default:
                Debug.LogError("Bilinmeyen bir balon t�r� geldi!!!");
                return ItemType.Null;
        }
        return ItemType.Null;
    }

    public void ClearHistory()
    {
        hasLevelChanged = false;
        changesList.Clear();
    }
}
