using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlace : MonoBehaviour
{

    public bool hasWall = false;
    public int wallX;
    public int wallY;
    public bool isHorizontal = false;

    public void Start()
    {
        LEBlock temp = GetComponentInParent<LEBlock>();

        if ((isHorizontal && temp.placeY == 0) || (!isHorizontal && temp.placeX == 0))
        {
            gameObject.SetActive(false);
        }

        wallX = temp.placeX;
        wallY = temp.placeY;
    }

    private void OnMouseDown()
    {
        /*
        if (!hasWall)
        {
            AddWall();
        }
        else
        {
            DeleteWall();
        }
        */
    }

    public void AddWall()
    {
        hasWall = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
    }

    public void DeleteWall()
    {
        hasWall = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.05f);
    }

    public void SelectedWall()
    {
        if (SelectionManager.Instance.selectionMode != SelectionMode.Wall)
        {
            return;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!hasWall)
            {
                HistoryManager.Instance.AddOrder(new WallChange(this));
            }
            AddWall();
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (hasWall)
            {
                HistoryManager.Instance.AddOrder(new WallChange(this));
            }
            DeleteWall();
        }
        else
        {
            if (hasWall)
            {
                HistoryManager.Instance.AddOrder(new WallChange(this));
                DeleteWall();
            }
            else
            {
                HistoryManager.Instance.AddOrder(new WallChange(this));
                AddWall();
            }
        }
    }
}



