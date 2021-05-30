using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LEBlock : Block, IPointerDownHandler, IPointerEnterHandler
{

    public WallPlace wallL;
    public WallPlace wallB;

    public TextMeshPro ropeText;
    public TextMeshPro zoneText;

    public int ropeID;
    // Start is called before the first frame update

    public override void AddYourSelfToGameManager()
    {
        //GameManager.Instance.blocksList.Add(this);
        //GameManager.Instance.blocksArray[placeX, placeY] = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (LevelEditorUIManager.Instance.selectedPrefab == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (onBalloon == null)
            {
                var createdItem = Instantiate(LevelEditorUIManager.Instance.selectedPrefab, Yardimcilar.Derinlik(transform.position, 1), Quaternion.identity);
                createdItem.GetComponent<Balloon>().onBlock = this;
                onBalloon = createdItem.GetComponent<Balloon>();
                HistoryManager.Instance.AddOrder(new BalloonChange(HistoryManager.Instance.ItemTypeConverter(onBalloon), ItemType.Null, placeX, placeY));
            }
            else
            {
                var hs = new BalloonChange(ItemType.Null, HistoryManager.Instance.ItemTypeConverter(onBalloon), placeX, placeY);
                Destroy(onBalloon.gameObject);
                var createdItem = Instantiate(LevelEditorUIManager.Instance.selectedPrefab, Yardimcilar.Derinlik(transform.position, 1), Quaternion.identity);
                createdItem.GetComponent<Balloon>().onBlock = this;
                onBalloon = createdItem.GetComponent<Balloon>();
                hs.newBalloon = HistoryManager.Instance.ItemTypeConverter(onBalloon);
                HistoryManager.Instance.AddOrder(hs);
            }
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (onBalloon != null)
            {
                HistoryManager.Instance.AddOrder(new BalloonChange(ItemType.Null, HistoryManager.Instance.ItemTypeConverter(onBalloon), placeX, placeY));
                Destroy(onBalloon.gameObject);
                onBalloon = null;
            }
        }
        else
        {
            if (onBalloon == null)
            {
                var createdItem = Instantiate(LevelEditorUIManager.Instance.selectedPrefab, Yardimcilar.Derinlik(transform.position, 1), Quaternion.identity);
                createdItem.GetComponent<Balloon>().onBlock = this;
                onBalloon = createdItem.GetComponent<Balloon>();
                HistoryManager.Instance.AddOrder(new BalloonChange(HistoryManager.Instance.ItemTypeConverter(onBalloon), ItemType.Null, placeX, placeY));
            }

            else if (onBalloon.gameObject.name != (LevelEditorUIManager.Instance.selectedPrefab.name + "(Clone)"))
            {
                var hs = new BalloonChange(ItemType.Null, HistoryManager.Instance.ItemTypeConverter(onBalloon), placeX, placeY);
                Destroy(onBalloon.gameObject);
                var createdItem = Instantiate(LevelEditorUIManager.Instance.selectedPrefab, Yardimcilar.Derinlik(transform.position, 1), Quaternion.identity);
                createdItem.GetComponent<Balloon>().onBlock = this;
                onBalloon = createdItem.GetComponent<Balloon>();
                hs.newBalloon = HistoryManager.Instance.ItemTypeConverter(onBalloon);
                HistoryManager.Instance.AddOrder(hs);
            }

            else
            {
                HistoryManager.Instance.AddOrder(new BalloonChange(ItemType.Null, HistoryManager.Instance.ItemTypeConverter(onBalloon), placeX, placeY));
                Destroy(onBalloon.gameObject);
                onBalloon = null;
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LevelEditorManager.Instance.isPressed && (LevelEditorUIManager.Instance.selectedPrefab != null))
        {
            OnPointerDown(eventData);
        }
    }

    public void ChangeRope(int newRope)
    {
        if (newRope != ropeID)
        {
            HistoryManager.Instance.AddOrder(new RopeChange(ropeID, newRope, placeX, placeY));
        }

        ChangeRopeText(newRope);
        ropeID = newRope;
    }
    public void ChangeZone(int newBorder)
    {
        if (newBorder != borderID)
        {
            HistoryManager.Instance.AddOrder(new ZoneChange(borderID, newBorder, placeX, placeY));
        }
        ChangeZoneText(newBorder);
        borderID = newBorder;
    }

    public void OnSelection()
    {
        switch (SelectionManager.Instance.selectionMode)
        {
            case SelectionMode.Rope:
                ChangeRope(SelectionManager.Instance.GetSelectionKey());
                break;
            case SelectionMode.Wall:
                break;
            case SelectionMode.Zone:
                ChangeZone(SelectionManager.Instance.GetSelectionKey());
                ChangeZoneText(borderID);
                break;
            case SelectionMode.Balloon:
                if (onBalloon != null)
                {
                    SelectionManager.Instance.AddBalloons(onBalloon);
                }
                break;
            default:
                break;
        }
    }

    public void ChangeZoneText(int newText)
    {
        if (newText == 0)
        {
            zoneText.text = "";
        }
        else
        {
            zoneText.text = newText.ToString();
        }
    }

    public void ChangeRopeText(int newText)
    {
        if (newText == 0)
        {
            ropeText.text = "";
        }
        else
        {
            ropeText.text = newText.ToString();
        }
    }

    public void SetupBorderID(int borderID)
    {
        this.borderID = borderID;
        ChangeZoneText(borderID);
    }

    public void SetupRopeID(int ropeID)
    {
        this.ropeID = ropeID;
        ChangeRopeText(ropeID);
    }

    public void AddBalloon(ItemType gelenType)
    {
        GameObject yerlestirilecekBalloon = null;
        switch (gelenType)
        {
            case ItemType.StandartRedBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.StandartRedBalloonPrefab;
                break;
            case ItemType.StandartYellowBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.StandartYellowBalloonPrefab;
                break;
            case ItemType.StandartBlueBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.StandartBlueBalloonPrefab;
                break;
            case ItemType.StandartGreenBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.StandartGreenBalloonPrefab;
                break;
            case ItemType.DontTouchBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.DontTouchBalloonPrefab;
                break;

            case ItemType.MustTouchBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.MustTouchBalloonPrefab;
                break;
            case ItemType.ReflectiveBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.ReflectiveBalloonPrefab;
                break;
            case ItemType.DoubleBalloon:
                yerlestirilecekBalloon = LELevelLoader.Instance.DoubleBalloonPrefab;
                break;
            case ItemType.Null:
                Destroy(onBalloon.gameObject);
                onBalloon = null;
                return;
            default:
                Debug.LogError("Bilinmeyen bir balon türü geldi!!!");
                break;
        }
        var createdItem = Instantiate(yerlestirilecekBalloon, Yardimcilar.Derinlik(transform.position, 1), Quaternion.identity);
        createdItem.GetComponent<Balloon>().onBlock = this;
        onBalloon = createdItem.GetComponent<Balloon>();
    }

}
