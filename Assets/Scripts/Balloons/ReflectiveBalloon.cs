using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ReflectiveBalloon : Balloon
{

    /*
    public override void OnMouseDown()
    {
        Debug.LogWarning("You can't pop untouchable balloons.");
    }
    */

    public async override void GetEffect(Wave wave, Balloon effector, int randomTime)
    {
        if (isEffectedFromThisBalloon.Contains(effector))
        {
            return;
        }
        isEffectedFromThisBalloon.Add(effector);
        if (health <= 0)
        {
            Debug.Log("Balloon Health is lower than 0.");
            return;
        }
        if (balloonState == BalloonState.Dead)
        {
            Debug.Log("Balloon State is Dead.");
            return;
        }
        else if (balloonState == BalloonState.Popped)
        {
            Debug.Log("Balon State is Popped.");
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

    public void CalculateState()
    {
        if (health <= 0)
        {
            GameManager.Instance.poppedBalloonCountThisRound++;
            base.balloonState = BalloonState.Popped;
            GameManager.Instance.PopBalloon(this, true, false);
        }
    }

    public void CalculateHealthColor()
    {
        Color currentColor = GetComponent<SpriteRenderer>().color;

        if (health <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.ReflectiveBalloon0;
        }
        else if (health <= 25)
        {
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.ReflectiveBalloon25;
        }
        else if (health <= 50)
        {
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.ReflectiveBalloon50;
        }
        else if (health <= 75)
        {
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.ReflectiveBalloon75;
        }
    }

    public override void PlaySound()
    {
        SoundManager.Instance.PlayReflective();
    }

    public bool Barrier(Balloon effector)
    {
        bool canPass = true;

        switch (effector)
        {
            default:
                canPass = true;
                break;
        }
        return canPass;
    }

}

