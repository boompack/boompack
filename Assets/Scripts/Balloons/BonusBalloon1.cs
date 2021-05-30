using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BonusBalloon1 : Balloon
{
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

        if (!Barrier(effector))
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
        PlaySound();
    }


    public void CalculateHealthColor()
    {
        Color currentColor = GetComponent<SpriteRenderer>().color;

        if (health <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (health <= 25)
        {
            GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.20f);
        }
        else if (health <= 50)
        {
            GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.50f);
        }
        else if (health <= 75)
        {
            GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.75f);
        }

    }

    public override void PopBalloon()
    {
        GameManager.Instance.bonusUseCount += 1;
        base.PopBalloon();
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
