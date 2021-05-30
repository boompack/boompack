using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DoubleBalloon : Balloon
{

    public GameObject shield200;
    public GameObject shield175;
    public GameObject shield150;
    public GameObject shield125;



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
        SetHealthText();
        PlaySound();
        CalculateHealthColor();
    }


    public void CalculateHealthColor()
    {
        Color currentColor = GetComponent<SpriteRenderer>().color;

        if (health <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.DoubleBalloon0;
            shield125.SetActive(false);
            shield150.SetActive(false);
            shield175.SetActive(false);
            shield200.SetActive(false);
        }
        else if (health <= 25)
        {
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.DoubleBalloon25;
            shield125.SetActive(false);
            shield150.SetActive(false);
            shield175.SetActive(false);
            shield200.SetActive(false);
        }
        else if (health <= 50)
        {
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.DoubleBalloon50;
            shield125.SetActive(false);
            shield150.SetActive(false);
            shield175.SetActive(false);
            shield200.SetActive(false);
        }
        else if (health <= 75)
        {
            GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.DoubleBalloon75;
            shield125.SetActive(false);
            shield150.SetActive(false);
            shield175.SetActive(false);
            shield200.SetActive(false);
        }
        else if (health <= 100)
        {
            shield125.SetActive(false);
            shield150.SetActive(false);
            shield175.SetActive(false);
            shield200.SetActive(false);
        }
        else if (health <= 125)
        {
            shield150.SetActive(false);
            shield175.SetActive(false);
            shield200.SetActive(false);
        }
        else if (health <= 150)
        {
            shield150.SetActive(false);
            shield200.SetActive(false);
        }
        else if (health <= 175)
        {
            shield200.SetActive(false);
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
