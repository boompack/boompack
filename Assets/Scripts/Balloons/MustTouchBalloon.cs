using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MustTouchBalloon : Balloon
{
    private void OnEnable()
    {
        GameManager.Instance.mustTouchBalloonsList.Add(this);
    }

    public async override void GetEffect(Wave wave, Balloon effector, int randomTime)
    {
        await Task.Delay(randomTime);
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
        CalculateHealthColor();
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
