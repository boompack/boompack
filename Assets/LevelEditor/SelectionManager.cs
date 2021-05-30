using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : Singleton<SelectionManager>
{
    public List<GameObject> selectedObjects = new List<GameObject>();
    public List<WallPlace> selectedWalls = new List<WallPlace>();
    public List<LEBlock> selectedBlocks = new List<LEBlock>();
    public List<Balloon> selectedBalloons = new List<Balloon>();


    public InputField zoneText;
    public InputField ropeText;

    public SelectionMode oldSelectionMode;
    public SelectionMode selectionMode;

    public GameObject selector;

    public void AddWalls(WallPlace selectedWall)
    {
        /*
        if (selectionMode == SelectionMode.Zone)
        {
            return;
        }

        selectedWalls.Add(selectedWall);
        */

        selectedWall.hasWall = !selectedWall.hasWall;
    }

    public void AddZones(LEBlock selectedBlock)
    {
        if(selectionMode == SelectionMode.Wall)
        {
            return;
        }

        selectedBlocks.Add(selectedBlock);
    }

    public void AddBalloons(Balloon selectedBalloon)
    {
        selectedBalloons.Add(selectedBalloon);
    }

    public void SelectionModeChange(int selectionID)
    {
        if(selectionID == 0)
        {
            selector.SetActive(false);
        }

        if(selectionID == 4)
        {
            selector.SetActive(false);
        }

        oldSelectionMode = selectionMode;
        selectionMode = (SelectionMode)selectionID;
        SetSelector();
    }

    public void ToOldSelection()
    {
        selectionMode = oldSelectionMode;
        SetSelector();
    }

    public void SetSelector()
    {
        switch (selectionMode)
        {
            case SelectionMode.None:
            selector.SetActive(false);
            break;
            case SelectionMode.Rope:
            selector.SetActive(true);
            break;
            case SelectionMode.Wall:
            selector.SetActive(true);
            break;
            case SelectionMode.Zone:
            selector.SetActive(true);
            break;
            case SelectionMode.Camera:
            selector.SetActive(false);
            break;
            case SelectionMode.Balloon:
            selector.SetActive(true);
            break;
        }
    }

    public int GetSelectionKey()
    {
        if(Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1) )
        {
            return 1;
        }

        else if(Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2) )
        {
            return 2;
        }

        else if(Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3) )
        {
            return 3;
        }

        else if(Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4) )
        {
            return 4;
        }

        else if(Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5) )
        {
            return 5;
        }

        else if(Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6) )
        {
            return 6;
        }

        else if(Input.GetKey(KeyCode.Alpha7) || Input.GetKey(KeyCode.Keypad7) )
        {
            return 7;
        }

        else if(Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Keypad8) )
        {
            return 8;
        }

        else if(Input.GetKey(KeyCode.Alpha9) || Input.GetKey(KeyCode.Keypad9) )
        {
            return 9;
        }
        else if(Input.GetKey(KeyCode.Alpha0) || Input.GetKey(KeyCode.Keypad0) )
        {
            return 10;
        }

        else if(Input.GetKey(KeyCode.LeftAlt))
        {
            return 0;
        }

        else
        {
            if(selectionMode == SelectionMode.Rope && !System.String.IsNullOrWhiteSpace(LevelEditorUIManager.Instance.ropeInput.text))
            {
                return System.Int32.Parse(LevelEditorUIManager.Instance.ropeInput.text);
            }
            else if(selectionMode == SelectionMode.Zone && !System.String.IsNullOrWhiteSpace(LevelEditorUIManager.Instance.zoneInput.text))
            {
                return System.Int32.Parse(LevelEditorUIManager.Instance.zoneInput.text);
            }
        }

        return 0;
    }
}

public enum SelectionMode : byte
{
    None = 0,
    Rope = 1,
    Wall = 2,
    Zone = 3,
    Camera = 4,
    Balloon = 5
}
