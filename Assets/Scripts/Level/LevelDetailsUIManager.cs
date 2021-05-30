using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class LevelDetailsUIManager : Singleton<LevelDetailsUIManager>
{
    int redMovesStart;
    int yellowMovesStart;
    int blueMovesStart;
    int greenMovesStart;


    public Text levelText;
    public Text redMovesText;
    public Text yellowMovesText;
    public Text blueMovesText;
    public Text greenMovesText;
    public Text timeText;
    public Text moveTimeText;
    public Text pointText;
    public Toggle colorLockToggle;

    public Image redMovesImage;
    public Image greenMovesImage;
    public Image blueMovesImage;
    public Image yellowMovesImage;

    public Color redMovesColor;
    public Color yellowMovesColor;
    public Color blueMovesColor;
    public Color greenMovesColor;

    public GameObject redMovesGameObject;
    public GameObject greenMovesGameObject;
    public GameObject blueMovesGameObject;
    public GameObject yellowMovesGameObject;

    public Sprite emptyMovesSprite;
    public Sprite redMovesSprite;
    public Sprite yellowMovesSprite;
    public Sprite blueMovesSprite;
    public Sprite greenMovesSprite;

    public InputField loadLevel;

    public void ChangeColorLockToggle()
    {
        GameManager.Instance.colorLock = colorLockToggle.isOn;
    }




    public void SetLevelDetails(int red, int yellow, int blue, int green)
    {
        redMovesStart = red;
        yellowMovesStart = yellow;
        blueMovesStart = blue;
        greenMovesStart = green;
    }


    public void LoadNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void LoadPreviousLevel()
    {
        GameManager.Instance.LoadPreviousLevel();
    }

    public void LoadThisLevel()
    {
        if (loadLevel.text == null)
        {
            return;
        }
        GameManager.Instance.LoadThisLevel(System.Int32.Parse(loadLevel.text));
    }

    public void SetRedMovesText(int gelenMove)
    {
#if (!LEVEL_EDITOR)
        if (gelenMove == 0 )
        {
            redMovesImage.sprite = emptyMovesSprite;
            redMovesText.text = gelenMove.ToString();

            if(redMovesStart != 0)
            {
            Blink(BalloonColor.Red);
            }
        }
        else
        {

                Scale(BalloonColor.Red);

            redMovesImage.sprite = redMovesSprite;
            redMovesText.text = gelenMove.ToString();
        }
#else
redMovesText.text = gelenMove.ToString();
#endif
    }

    public void SetBlueMovesText(int gelenMove)
    {
#if (!LEVEL_EDITOR)
        if (gelenMove == 0 )
        {
            blueMovesImage.sprite = emptyMovesSprite;
            blueMovesText.text = gelenMove.ToString();
            if(blueMovesStart != 0)
            {
            Blink(BalloonColor.Blue);
            }
        }
        else
        {

                Scale(BalloonColor.Blue);
          
            blueMovesImage.sprite = blueMovesSprite;
            blueMovesText.text = gelenMove.ToString();
            
        }
#else
        blueMovesText.text = gelenMove.ToString();
#endif
    }

    public void SetGreenMovesText(int gelenMove)
    {
#if (!LEVEL_EDITOR)
        if (gelenMove == 0)
        {
            greenMovesImage.sprite = emptyMovesSprite;
            greenMovesText.text = gelenMove.ToString();
            if(greenMovesStart != 0 )
            {
            Blink(BalloonColor.Green);
            }
        }
        else
        {


            greenMovesImage.sprite = greenMovesSprite;
            greenMovesText.text = gelenMove.ToString();
        }
#else
        greenMovesText.text = gelenMove.ToString();
#endif
    }

    public void SetYellowMovesText(int gelenMove)
    {
#if (!LEVEL_EDITOR)
        if (gelenMove == 0)
        {
            yellowMovesImage.sprite = emptyMovesSprite;
            yellowMovesText.text = gelenMove.ToString();
            if(yellowMovesStart != 0)
            {
                Blink(BalloonColor.Yellow);
            }
            
        }
        else
        {

                Scale(BalloonColor.Yellow);

            yellowMovesImage.sprite = yellowMovesSprite;
            yellowMovesText.text = gelenMove.ToString();
        }
#else
        yellowMovesText.text = gelenMove.ToString();
#endif
    }

    public void StartUI()
    {

    }


        public void Blink(BalloonColor color)
        {
            switch (color)
            {
                case BalloonColor.Red:
                    redMovesImage.DOColor(redMovesColor, 0.3f).SetDelay(0.3f);
                    redMovesImage.DOColor(Color.white, 0.3f).SetDelay(0.6f);
                    redMovesImage.DOColor(redMovesColor, 0.3f).SetDelay(0.9f);
                    redMovesImage.DOColor(Color.white, 0.3f).SetDelay(1.2f);
                    break;
                case BalloonColor.Yellow:
                    yellowMovesImage.DOColor(yellowMovesColor, 0.3f).SetDelay(0.3f);
                    yellowMovesImage.DOColor(Color.white, 0.3f).SetDelay(0.6f);
                    yellowMovesImage.DOColor(yellowMovesColor, 0.3f).SetDelay(0.9f);
                    yellowMovesImage.DOColor(Color.white, 0.3f).SetDelay(1.2f);

                    break;
                case BalloonColor.Blue:
                    blueMovesImage.DOColor(blueMovesColor, 0.3f).SetDelay(0.3f);
                    blueMovesImage.DOColor(Color.white, 0.3f).SetDelay(0.6f);
                    blueMovesImage.DOColor(blueMovesColor, 0.3f).SetDelay(0.9f);
                    blueMovesImage.DOColor(Color.white, 0.3f).SetDelay(1.2f);
                    break;
                case BalloonColor.Green:
                    greenMovesImage.DOColor(greenMovesColor, 0.3f).SetDelay(0.3f);
                    greenMovesImage.DOColor(Color.white, 0.3f).SetDelay(0.6f);
                    greenMovesImage.DOColor(greenMovesColor, 0.3f).SetDelay(0.9f);
                    greenMovesImage.DOColor(Color.white, 0.3f).SetDelay(1.2f);
                    break;

            }
        }

        public void Scale(BalloonColor color)
        {
            switch (color)
            {
                case BalloonColor.Red:
                    redMovesGameObject.transform.DOScale(1.5f, 0.3f);
                    redMovesGameObject.transform.DOScale(1f, 0.3f).SetDelay(0.3f);

                    break;
                case BalloonColor.Yellow:
                    yellowMovesGameObject.transform.DOScale(1.5f, 0.3f);
                    yellowMovesGameObject.transform.DOScale(1f, 0.3f).SetDelay(0.3f);

                    break;
                case BalloonColor.Blue:
                    blueMovesGameObject.transform.DOScale(1.5f, 0.3f);
                    blueMovesGameObject.transform.DOScale(1f, 0.3f).SetDelay(0.3f);
                    break;
                case BalloonColor.Green:
                    greenMovesGameObject.transform.DOScale(1.5f, 0.3f);
                    greenMovesGameObject.transform.DOScale(1f, 0.3f).SetDelay(0.3f);
                    break;

            }
        }
    }



