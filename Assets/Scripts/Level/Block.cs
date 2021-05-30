using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IDropHandler
{

    public int placeX;
    public int placeY;

    public Rope onRope;
    public int borderID = 0;

    public Balloon onBalloon;


    // Start is called before the first frame update
    void Start()
    {
        AddYourSelfToGameManager();
    }

    public virtual void AddYourSelfToGameManager()
    {
        GameManager.Instance.blocksList.Add(this);
        GameManager.Instance.blocksArray[placeX, placeY] = this;
    }


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Buraya bir �ey b�rak�lmaya �al���ld�...");
        /*
        if (onBalloon != null)
        {
            if (onBalloon.balloonState == BalloonState.Active)
            {
                GameManager.Instance.balloonsList.Remove(onBalloon);
                Destroy(onBalloon.gameObject);

                CreateBonusBalloon(eventData.pointerDrag.gameObject);
                Debug.Log("Bonus Dropped on This Block");
            }
        }
        */
    }

    public void OnDropRaycast(GameObject gelenObje)
    {
        Debug.Log("OnDrop Raycast �al��t�...");
        if (onBalloon != null)
        {
            if (onBalloon.balloonState == BalloonState.Active)
            {
                CreateBonusBalloon(gelenObje);
                Debug.Log("Bonus Dropped on This Block");
            }
        }
    }

    public void CreateBonusBalloon(GameObject bonus)
    {
        switch (bonus.GetComponent<BonusBalloonItem>().bonusBalloonType)
        {
            case BonusBalloonType.bonusBalloon1:
                if (GameManager.Instance.bonusBalloon1Count > 0)
                {
                    GameManager.Instance.balloonsList.Remove(onBalloon);
                    Destroy(onBalloon.gameObject);
                    onBalloon = Instantiate(GameManager.Instance.bonusBalloon1, transform.position, Quaternion.identity).GetComponent<Balloon>();
                    GameManager.Instance.bonusBalloon1Count--;
                    BonusBalloonUIManager.Instance.RefleshCounts();
                    onBalloon.onBlock = this;
                    onBalloon.TouchBalloon();
                }
                break;
            case BonusBalloonType.bonusBalloon2:
                if (GameManager.Instance.bonusBalloon2Count > 0)
                {
                    GameManager.Instance.balloonsList.Remove(onBalloon);
                    Destroy(onBalloon.gameObject);
                    onBalloon = Instantiate(GameManager.Instance.bonusBalloon2, transform.position, Quaternion.identity).GetComponent<Balloon>();
                    GameManager.Instance.bonusBalloon2Count--;
                    BonusBalloonUIManager.Instance.RefleshCounts();
                    onBalloon.onBlock = this;
                    onBalloon.TouchBalloon();
                }
                break;
            case BonusBalloonType.bonusBalloon3:
                if (GameManager.Instance.bonusBalloon3Count > 0)
                {
                    GameManager.Instance.balloonsList.Remove(onBalloon);
                    Destroy(onBalloon.gameObject);
                    onBalloon = Instantiate(GameManager.Instance.bonusBalloon3, transform.position, Quaternion.identity).GetComponent<Balloon>();
                    GameManager.Instance.bonusBalloon3Count--;
                    BonusBalloonUIManager.Instance.RefleshCounts();
                    onBalloon.onBlock = this;
                    onBalloon.TouchBalloon();
                }
                break;
           
            default:
                break;
        }
    }
}
