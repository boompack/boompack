using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Balloon : MonoBehaviour
{
    public int health = 100;
    public int lastHealth = 100;

    public int point = 100;

    public int wave1Lenght = 1;
    public int wave2Lenght = 1;
    public int wave3Lenght = 1;

    public BalloonState balloonState = BalloonState.Active;

    public Block onBlock;

    public bool isTouchable = true;

    public List<Balloon> isEffectedFromThisBalloon = new List<Balloon>();

    public Animator animator;

    public TextMesh balloonHealthText;

    public virtual void PopBalloon()
    {
        if (GameManager.Instance.continuedBalloonPopping != 0) return;

        if (!isTouchable)
        {
            Debug.Log("Balon Dokululamaz");
            SoundManager.Instance.PlayDontTouch();
            return;
        }


        if (health <= 0)
        {
            Debug.Log("Balon Canı 0'dan Az");
            return;
        }
        if (balloonState == BalloonState.Dead)
        {
            Debug.Log("Balon State Dead");
            return;
        }
        else if (balloonState == BalloonState.Popped)
        {
            Debug.Log("Balon State Popped");
            return;
        }

        //isEffectedThisRound = true;
        PointsManager.Instance.AddPoints(health);
        health = 0;
        balloonState = BalloonState.Dead;
        GameManager.Instance.PopBalloon(this);
        GetComponent<SpriteRenderer>().sprite = BalloonGraphicReferance.Instance.Patlak;
        SetHealthText();

    }

    /*
    public virtual void OnMouseDown()
    {
        if (balloonState == BalloonState.Active)
        {
            GameManager.Instance.poppedBalloonCountThisRound = 1;
            GameManager.Instance.AddPoints(health);
            Debug.Log("Balon Patlatıldı");
            PopBalloon();

        }
        else
        {
            Debug.Log("Balon Aktif Olmadığı İçin Patlatılamıyor.");
        }
    }
    */
    public virtual void TouchBalloon()
    {
        if (balloonState == BalloonState.Active && GameManager.Instance.balloonUseable)
        {
            GameManager.Instance.poppedBalloonCountThisRound = 1;
            Debug.Log("Balon Patlatıldı");
            StartCoroutine(SetBallonUseable(0.5f));
            StartCoroutine(SetBonusUseable(2));
            PopBalloon();
        }
        else
        {
            Debug.Log("Balon Aktif Olmadığı İçin Patlatılamıyor.");
        }
    }

    IEnumerator SetBonusUseable(int i)
    {
        GameManager.Instance.isBonusesUseble = false;
        Debug.Log("Bonus Kullanılamaz");

        yield return new WaitForSeconds(i);
        GameManager.Instance.isBonusesUseble = true;
        Debug.Log("Bonus Kullanılabilir");


    }

    IEnumerator SetBallonUseable(float i)
    {
        GameManager.Instance.balloonUseable = false;
        Debug.Log("Balon Kullanılamaz");

        yield return new WaitForSeconds(i);
        GameManager.Instance.balloonUseable = true;
        Debug.Log("Balon Kullanılabilir");


    }

    public async virtual void GetEffect(Wave wave, Balloon effector, int randomTime)
    {
        if (isEffectedFromThisBalloon.Contains(effector))
        {
            return;
        }
        isEffectedFromThisBalloon.Add(effector);
        Debug.Log("Bu Balon Etki Aldı:");
        switch (wave)
        {
            case Wave.Wave1:

                GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            case Wave.Wave2:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;

            case Wave.Wave3:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                break;

            default:
                break;
        }
    }

    public void CalculateState()
    {
        if (health <= 0)
        {
            GameManager.Instance.poppedBalloonCountThisRound++;
            balloonState = BalloonState.Dead;

        }
    }

    public void SetHealth(Wave wave)
    {
        lastHealth = health;
        switch (wave)
        {
            case Wave.Wave1:
                health -= (int)Wave.Wave1;
                break;
            case Wave.Wave2:
                health -= (int)Wave.Wave2;
                break;
            case Wave.Wave3:
                health -= (int)Wave.Wave3;
                break;
            default:
                break;
        }
        if (health <= 0)
        {
            PointsManager.Instance.AddPoints(lastHealth);
        }
        //SetHealthText();
    }

    public virtual void PlaySound()
    {
        SoundManager.Instance.PlayPop();
    }

    public void ShowHealthText()
    {
        if (health >= 100)
        {
            balloonHealthText.gameObject.SetActive(true);
            return;
        }

        if (health >= 75)
        {
            balloonHealthText.gameObject.SetActive(true);
            return;
        }

        if (health >= 50)
        {
            balloonHealthText.gameObject.SetActive(true);
            balloonHealthText.gameObject.transform.localPosition = new Vector3(0f, -0.02f, -0.1f);
            balloonHealthText.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            balloonHealthText.color = Color.black;
            return;
        }

        if (health > 0)
        {
            balloonHealthText.gameObject.SetActive(true);
            balloonHealthText.gameObject.SetActive(true);
            balloonHealthText.gameObject.transform.localPosition = new Vector3(0f, -0.02f, -0.1f);
            balloonHealthText.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            balloonHealthText.color = Color.black;
            return;
        }
    }

    public void HideHealthText()
    {
        balloonHealthText.gameObject.SetActive(false);
    }

    public void SetHealthText()
    {
        if (health <= 0)
        {
            balloonHealthText.gameObject.SetActive(false);
            return;
        }

        else if (health <= 25)
        {
            balloonHealthText.gameObject.transform.localPosition = new Vector3(0f, -0.02f, -0.1f);
            balloonHealthText.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            balloonHealthText.color = Color.black;
        }

        else if (health <= 50)
        {
            balloonHealthText.gameObject.transform.localPosition = new Vector3(0f, -0.02f, -0.1f);
            balloonHealthText.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            balloonHealthText.color = Color.black;
        }

        balloonHealthText.text = health.ToString();
    }
}

public enum BalloonState : byte
{
    Active,
    Popped,
    Dead,
    InActive
}