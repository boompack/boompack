using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StandartBalloon : Balloon
{
    public BalloonColor balloonColor;


    public async override void GetEffect(Wave wave, Balloon effector, int randomTime)
    {
        if (isEffectedFromThisBalloon.Contains(effector))
        {
            return;
        }

        isEffectedFromThisBalloon.Add(effector);

        if (health <= 0)
        {
            return;
        }
        if (balloonState == BalloonState.Dead)
        {
            return;
        }
        else if (balloonState == BalloonState.Popped)
        {
            return;
        }

        if (GameManager.Instance.colorLock && !Barrier(effector))
        {
            return;
        }

        SetHealth(wave);
        CalculateState();
        RopeConnection(wave, effector, randomTime);


        if (randomTime != 0)
        {
            await Task.Delay(randomTime);
        }
        CalculateHealthColor();
        SetHealthText();
        PlaySound();
    }

    public async void GetEffectRope(Wave wave, Balloon effector, int randomTime)
    {
        if (isEffectedFromThisBalloon.Contains(effector))
        {
            return;
        }

        isEffectedFromThisBalloon.Add(effector);

        if (health <= 0)
        {
            return;
        }
        if (balloonState == BalloonState.Dead)
        {
            return;
        }
        else if (balloonState == BalloonState.Popped)
        {
            return;
        }

        if (GameManager.Instance.colorLock && !Barrier(effector))
        {
            return;
        }

        SetHealth(wave);
        CalculateState();


        if (randomTime != 0)
        {
            await Task.Delay(randomTime);
        }
        CalculateHealthColor();
        SetHealthText();
        PlaySound();
    }

    public void RopeConnection(Wave wave, Balloon effectedBalloon, int randomTime)
    {
        /*
        if(onBlock.onRope != null)
        {
            if (!onBlock.onRope.isEffectedFromThisBalloon.Contains(effectedBalloon))
            {
                onBlock.onRope.isEffectedFromThisBalloon.Add(effectedBalloon);
                foreach (var item in onBlock.onRope.ropedBlocks)
                {
                    item.onBalloon.isEffectedFromThisBalloon.Add(effectedBalloon);
                    item.onBalloon.GetEffect(wave, effectedBalloon);
                }
            }
        }
        */
        if (onBlock.onRope != null)
        {
            foreach (var item in onBlock.onRope.ropedBlocks)
            {
                //item.onBalloon.isEffectedFromThisBalloon.Add(effectedBalloon);
                if (!item.onBalloon.isEffectedFromThisBalloon.Contains(effectedBalloon))
                {
                    Debug.Log("Başlatan ID: " + onBlock.placeX + "+" + onBlock.placeY);
                    float uzaklik = Vector3.Distance(effectedBalloon.onBlock.transform.position, item.transform.position);
                    float ekleme;

                    if (uzaklik <= 1.5f)
                    {
                        ekleme = 0;
                    }
                    else
                    {
                        ekleme = uzaklik;
                    }

                    ((StandartBalloon)item.onBalloon).GetEffectRope(wave, effectedBalloon, randomTime + Mathf.RoundToInt((ekleme * 40)));
                    Debug.Log("İplerin Uzakligi: " + uzaklik);
                    Debug.Log("Random Time: " + randomTime);
                    GameManager.Instance.AnimateEffect(item, effectedBalloon.onBlock, randomTime + Mathf.RoundToInt((ekleme * 30)), (int)uzaklik);
                }
            }
        }
    }

    public void CalculateHealthColor()
    {
        if (onBlock.onRope == null)
        {
            switch (balloonColor)
            {
                case BalloonColor.Red:
                    if (health <= 0)
                    {
                        //GetComponent<SpriteRenderer>().color = Color.grey;
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartRedBalloon0;
                    }
                    else if (health <= 25)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartRedBalloon25;
                    }
                    else if (health <= 50)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartRedBalloon50;
                    }
                    else if (health <= 75)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartRedBalloon75;
                    }
                    break;
                case BalloonColor.Blue:

                    if (health <= 0)
                    {
                        //GetComponent<SpriteRenderer>().color = Color.grey;
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartBlueBalloon0;
                    }
                    else if (health <= 25)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartBlueBalloon25;
                    }
                    else if (health <= 50)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartBlueBalloon50;
                    }
                    else if (health <= 75)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartBlueBalloon75;
                    }
                    break;

                case BalloonColor.Green:

                    if (health <= 0)
                    {
                        //GetComponent<SpriteRenderer>().color = Color.grey;
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartGreenBalloon0;
                    }
                    else if (health <= 25)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartGreenBalloon25;
                    }
                    else if (health <= 50)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartGreenBalloon50;
                    }
                    else if (health <= 75)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartGreenBalloon75;
                    }
                    break;

                case BalloonColor.Yellow:

                    if (health <= 0)
                    {
                        //GetComponent<SpriteRenderer>().color = Color.grey;
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartYellowBalloon0;
                    }
                    else if (health <= 25)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartYellowBalloon25;
                    }
                    else if (health <= 50)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartYellowBalloon50;
                    }
                    else if (health <= 75)
                    {
                        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.StandartYellowBalloon75;
                    }

                    break;
                default:
                    Debug.LogError("Uncalculated Color");
                    break;
            }
        }
        else
        {
            if (health <= 0)
            {
                //GetComponent<SpriteRenderer>().color = Color.grey;
                GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.RopeBalloon0;
            }
            else if (health <= 25)
            {
                GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.RopeBalloon25;
            }
            else if (health <= 50)
            {
                GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.RopeBalloon50;
            }
            else if (health <= 75)
            {
                GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.RopeBalloon75;
            }

        }

    }

    public override void PlaySound()
    {
        SoundManager.Instance.PlayPop(balloonColor);
    }

    public override void PopBalloon()
    {
        if (!GameManager.Instance.CalculateMoves(this))
        {
#if (!LEVEL_EDITOR)
            LevelDetailsUIManager.Instance.Blink(balloonColor);
            return;
#endif
        }
        base.PopBalloon();
    }

    public bool Barrier(Balloon effector)
    {
        bool canPass = true;

        switch (effector)
        {
            case StandartBalloon sb:
                if (balloonColor != sb.balloonColor)
                {
                    canPass = false;
                }
                break;
            default:
                canPass = true;
                break;
        }
        return canPass;
    }
}


public enum BalloonColor : byte
{
    Red,
    Yellow,
    Blue,
    Green,
    Rope
}


