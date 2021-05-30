using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateColorManager : Singleton<StateColorManager>
{
    public LevelLoader levelLoader;


    public SpriteRenderer background;
    public SpriteRenderer backgroundTile;
    public Image border;
    public Image borderBack;
    public Image levelLeft;
    public Image bonusRight;

    [Header("State 1 Colors")]
    public Color state1BackgroundColor;
    public Color state1BackgroundTileColor;
    public Color state1BorderColor;
    public Color state1BorderBackgroundColor;
    public Color state1WallColor;
    public Color state1WallBackgroundColor;

    [Header("State 2 Colors")]
    public Color state2BackgroundColor;
    public Color state2BackgroundTileColor;
    public Color state2BorderColor;
    public Color state2BorderBackgroundColor;
    public Color state2WallColor;
    public Color state2WallBackgroundColor;

    [Header("State 3 Colors")]
    public Color state3BackgroundColor;
    public Color state3BackgroundTileColor;
    public Color state3BorderColor;
    public Color state3BorderBackgroundColor;
    public Color state3WallColor;
    public Color state3WallBackgroundColor;

    [Header("State 4 Colors")]
    public Color state4BackgroundColor;
    public Color state4BackgroundTileColor;
    public Color state4BorderColor;
    public Color state4BorderBackgroundColor;
    public Color state4WallColor;
    public Color state4WallBackgroundColor;

    [Header("State 5 Colors")]
    public Color state5BackgroundColor;
    public Color state5BackgroundTileColor;
    public Color state5BorderColor;
    public Color state5BorderBackgroundColor;
    public Color state5WallColor;
    public Color state5WallBackgroundColor;

    [Header("State 6 Colors")]
    public Color state6BackgroundColor;
    public Color state6BackgroundTileColor;
    public Color state6BorderColor;
    public Color state6BorderBackgroundColor;
    public Color state6WallColor;
    public Color state6WallBackgroundColor;

    [Header("State 7 Colors")]
    public Color state7BackgroundColor;
    public Color state7BackgroundTileColor;
    public Color state7BorderColor;
    public Color state7BorderBackgroundColor;
    public Color state7WallColor;
    public Color state7WallBackgroundColor;

    [Header("State 8 Colors")]
    public Color state8BackgroundColor;
    public Color state8BackgroundTileColor;
    public Color state8BorderColor;
    public Color state8BorderBackgroundColor;
    public Color state8WallColor;
    public Color state8WallBackgroundColor;

    [Header("State 9 Colors")]
    public Color state9BackgroundColor;
    public Color state9BackgroundTileColor;
    public Color state9BorderColor;
    public Color state9BorderBackgroundColor;
    public Color state9WallColor;
    public Color state9WallBackgroundColor;

    [Header("State 10 Colors")]
    public Color state10BackgroundColor;
    public Color state10BackgroundTileColor;
    public Color state10BorderColor;
    public Color state10BorderBackgroundColor;
    public Color state10WallColor;
    public Color state10WallBackgroundColor;

    [Header("State 11 Colors")]
    public Color state11BackgroundColor;
    public Color state11BackgroundTileColor;
    public Color state11BorderColor;
    public Color state11BorderBackgroundColor;
    public Color state11WallColor;
    public Color state11WallBackgroundColor;

    [Header("State 12 Colors")]
    public Color state12BackgroundColor;
    public Color state12BackgroundTileColor;
    public Color state12BorderColor;
    public Color state12BorderBackgroundColor;
    public Color state12WallColor;
    public Color state12WallBackgroundColor;

    [Header("State 13 Colors")]
    public Color state13BackgroundColor;
    public Color state13BackgroundTileColor;
    public Color state13BorderColor;
    public Color state13BorderBackgroundColor;
    public Color state13WallColor;
    public Color state13WallBackgroundColor;

    [Header("State 14 Colors")]
    public Color state14BackgroundColor;
    public Color state14BackgroundTileColor;
    public Color state14BorderColor;
    public Color state14BorderBackgroundColor;
    public Color state14WallColor;
    public Color state14WallBackgroundColor;

    [Header("State 15 Colors")]
    public Color state15BackgroundColor;
    public Color state15BackgroundTileColor;
    public Color state15BorderColor;
    public Color state15BorderBackgroundColor;
    public Color state15WallColor;
    public Color state15WallBackgroundColor;

    private Color currentLevelBackgroundColor;
    private Color currentLevelBackgroundTileColor;
    private Color currentLevelWallBackgroundColor;

    public void SetStateColors(int stateID, LevelLoader lvl)
    {
        levelLoader = lvl;
        stateID --;

        if( (stateID / 20) % 5 == 0)
        {
            background.color = state1BackgroundColor;
            backgroundTile.color = state1BackgroundTileColor;

            border.color = state1BorderColor;
            borderBack.color = state1BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;

            currentLevelWallBackgroundColor = state1WallBackgroundColor;

            levelLeft.color = state1BorderColor;
            bonusRight.color = state1BorderColor;
        }

        else if ((stateID / 20) % 5== 1)
        {
            background.color = state15BackgroundColor;
            backgroundTile.color = state15BackgroundTileColor;

            border.color = state15BorderColor;
            borderBack.color = state15BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;

            currentLevelWallBackgroundColor = state15WallBackgroundColor;

            levelLeft.color = state15BorderColor;
            bonusRight.color = state15BorderColor;
        }

        else if ((stateID / 20) % 5 == 2)
        {
            background.color = state2BackgroundColor;
            backgroundTile.color = state2BackgroundTileColor;

            border.color = state2BorderColor;
            borderBack.color = state2BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;

            currentLevelWallBackgroundColor = state2WallBackgroundColor;

            levelLeft.color = state2BorderColor;
            bonusRight.color = state2BorderColor;
        }

        else if ((stateID / 20) % 5 == 3)
        {
            background.color = state3BackgroundColor;
            backgroundTile.color = state3BackgroundTileColor;

            border.color = state3BorderColor;
            borderBack.color = state3BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;

            currentLevelWallBackgroundColor = state3WallBackgroundColor;

            levelLeft.color = state3BorderColor;
            bonusRight.color = state3BorderColor;
        }

        else if ((stateID / 20) % 5 == 4)
        {
            background.color = state4BackgroundColor;
            backgroundTile.color = state4BackgroundTileColor;

            border.color = state4BorderColor;
            borderBack.color = state4BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;

            currentLevelWallBackgroundColor = state4WallBackgroundColor;

            levelLeft.color = state4BorderColor;
            bonusRight.color = state4BorderColor;
        }

        /* OLD 15 SYSTEM
        levelLoader = lvl;

        if (stateID <= 20)
        {
            background.color = state1BackgroundColor;
            backgroundTile.color = state1BackgroundTileColor;

            border.color = state1BorderColor;
            borderBack.color = state1BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state1WallColor;

            currentLevelWallBackgroundColor = state1WallBackgroundColor;

            levelLeft.color = state1BorderColor;
            bonusRight.color = state1BorderColor;
        }

        else if (stateID <= 40)
        {
            background.color = state2BackgroundColor;
            backgroundTile.color = state2BackgroundTileColor;

            border.color = state2BorderColor;
            borderBack.color = state2BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state2WallColor;

            currentLevelWallBackgroundColor = state2WallBackgroundColor;

            levelLeft.color = state2BorderColor;
            bonusRight.color = state2BorderColor;
        }

        else if (stateID <= 60)
        {
            background.color = state3BackgroundColor;
            backgroundTile.color = state3BackgroundTileColor;

            border.color = state3BorderColor;
            borderBack.color = state3BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state3WallColor;

            currentLevelWallBackgroundColor = state3WallBackgroundColor;

            levelLeft.color = state3BorderColor;
            bonusRight.color = state3BorderColor;
        }

        else if (stateID <= 80)
        {
            background.color = state4BackgroundColor;
            backgroundTile.color = state4BackgroundTileColor;

            border.color = state4BorderColor;
            borderBack.color = state4BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state4WallColor;

            currentLevelWallBackgroundColor = state4WallBackgroundColor;

            levelLeft.color = state4BorderColor;
            bonusRight.color = state4BorderColor;
        }

        else if (stateID <= 100)
        {
            background.color = state5BackgroundColor;
            backgroundTile.color = state5BackgroundTileColor;

            border.color = state5BorderColor;
            borderBack.color = state5BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state5WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state5WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state5WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state5WallColor;

            currentLevelWallBackgroundColor = state5WallBackgroundColor;

            levelLeft.color = state5BorderColor;
            bonusRight.color = state5BorderColor;
        }

        else if (stateID <= 120)
        {
            background.color = state6BackgroundColor;
            backgroundTile.color = state6BackgroundTileColor;

            border.color = state6BorderColor;
            borderBack.color = state6BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state6WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state6WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state6WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state6WallColor;

            currentLevelWallBackgroundColor = state6WallBackgroundColor;

            levelLeft.color = state6BorderColor;
            bonusRight.color = state6BorderColor;
        }
        else if (stateID <= 140)
        {
            background.color = state7BackgroundColor;
            backgroundTile.color = state7BackgroundTileColor;

            border.color = state7BorderColor;
            borderBack.color = state7BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state7WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state7WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state7WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state7WallColor;

            currentLevelWallBackgroundColor = state7WallBackgroundColor;

            levelLeft.color = state7BorderColor;
            bonusRight.color = state7BorderColor;
        }

        else if (stateID <= 160)
        {
            background.color = state8BackgroundColor;
            backgroundTile.color = state8BackgroundTileColor;

            border.color = state8BorderColor;
            borderBack.color = state8BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state8WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state8WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state8WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state8WallColor;

            currentLevelWallBackgroundColor = state8WallBackgroundColor;

            levelLeft.color = state8BorderColor;
            bonusRight.color = state8BorderColor;
        }

        else if (stateID <= 180)
        {
            background.color = state9BackgroundColor;
            backgroundTile.color = state9BackgroundTileColor;

            border.color = state9BorderColor;
            borderBack.color = state9BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state9WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state9WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state9WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state9WallColor;

            currentLevelWallBackgroundColor = state9WallBackgroundColor;

            levelLeft.color = state9BorderColor;
            bonusRight.color = state9BorderColor;
        }

        else if (stateID <= 200)
        {
            background.color = state10BackgroundColor;
            backgroundTile.color = state10BackgroundTileColor;

            border.color = state10BorderColor;
            borderBack.color = state10BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state10WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state10WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state10WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state10WallColor;

            currentLevelWallBackgroundColor = state10WallBackgroundColor;

            levelLeft.color = state10BorderColor;
            bonusRight.color = state10BorderColor;
        }

        else if (stateID <= 220)
        {
            background.color = state11BackgroundColor;
            backgroundTile.color = state11BackgroundTileColor;

            border.color = state11BorderColor;
            borderBack.color = state11BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state11WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state11WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state11WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state11WallColor;

            currentLevelWallBackgroundColor = state11WallBackgroundColor;

            levelLeft.color = state11BorderColor;
            bonusRight.color = state11BorderColor;
        }

        else if (stateID <= 240)
        {
            background.color = state12BackgroundColor;
            backgroundTile.color = state12BackgroundTileColor;

            border.color = state12BorderColor;
            borderBack.color = state12BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state12WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state12WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state12WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state12WallColor;

            currentLevelWallBackgroundColor = state12WallBackgroundColor;

            levelLeft.color = state12BorderColor;
            bonusRight.color = state12BorderColor;
        }

        else if (stateID <= 260)
        {
            background.color = state13BackgroundColor;
            backgroundTile.color = state13BackgroundTileColor;

            border.color = state13BorderColor;
            borderBack.color = state13BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state13WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state13WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state13WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state13WallColor;

            currentLevelWallBackgroundColor = state13WallBackgroundColor;

            levelLeft.color = state13BorderColor;
            bonusRight.color = state13BorderColor;
        }

        else if (stateID <= 280)
        {
            background.color = state14BackgroundColor;
            backgroundTile.color = state14BackgroundTileColor;

            border.color = state14BorderColor;
            borderBack.color = state14BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state14WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state14WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state14WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state14WallColor;

            currentLevelWallBackgroundColor = state14WallBackgroundColor;

            levelLeft.color = state14BorderColor;
            bonusRight.color = state14BorderColor;
        }

        else
        {
            background.color = state15BackgroundColor;
            backgroundTile.color = state15BackgroundTileColor;

            border.color = state15BorderColor;
            borderBack.color = state15BorderBackgroundColor;

            levelLoader.wallBPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;
            levelLoader.wallBEmptyPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;
            levelLoader.wallLPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;
            levelLoader.wallLEmptyPrefab.GetComponent<SpriteRenderer>().color = state15WallColor;

            currentLevelWallBackgroundColor = state15WallBackgroundColor;

            levelLeft.color = state15BorderColor;
            bonusRight.color = state15BorderColor;
        }
        */
    }

    public Color GetStateWallBackgroundColor()
    {
        return currentLevelWallBackgroundColor;
    }

    public Color GetStateBackgroundColor()
    {
        return currentLevelBackgroundColor;
    }

    public Color GetStateBackgroundTileColor()
    {
        return currentLevelBackgroundTileColor;
    }


    public Color GetStateBackgroundColor(int levelID)
    {
        levelID --;

        if( (levelID / 20) % 5 == 0)
        {
            return state1BackgroundColor;
            
        }

        else if ((levelID / 20) % 5== 1)
        {
            return state15BackgroundColor;
        }

        else if ((levelID / 20) % 5 == 2)
        {
            return state2BackgroundColor;
        }

        else if ((levelID / 20) % 5 == 3)
        {
            return state3BackgroundColor;
        }

        else if ((levelID / 20) % 5 == 4)
        {
            return state4BackgroundColor;
        }

        return state1BackgroundColor;
    }

    public Color GetStateBorderColor(int levelID)
    {
        levelID --;

        if( (levelID / 20) % 5 == 0)
        {
            return state1BorderColor;
            
        }

        else if ((levelID / 20) % 5== 1)
        {
            return state15BorderColor;
        }

        else if ((levelID / 20) % 5 == 2)
        {
            return state2BorderColor;
        }

        else if ((levelID / 20) % 5 == 3)
        {
            return state3BorderColor;
        }

        else if ((levelID / 20) % 5 == 4)
        {
            return state4BorderColor;
        }

        return state1BorderColor;
    }





}
