using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBalloonUIManager : Singleton<BonusBalloonUIManager>
{
    public GameObject bonusBalloon1UI;
    public GameObject bonusBalloon2UI;
    public GameObject bonusBalloon3UI;
    public GameObject bonusBalloon4UI;
    public GameObject bonusBalloon5UI;
    public GameObject bonusBalloon6UI;
    public GameObject bonusBalloon7UI;
    public GameObject bonusBalloon8UI;
    public GameObject bonusBalloon9UI;
    public GameObject bonusBalloon10UI;
    public GameObject bonusBalloon11UI;
    public GameObject bonusBalloon12UI;
    public GameObject bonusBalloon13UI;
    public GameObject bonusBalloon14UI;
    public GameObject bonusBalloon15UI;
    public GameObject bonusBalloon16UI;
    public GameObject bonusBalloon17UI;
    public GameObject bonusBalloon18UI;
    public GameObject bonusBalloon19UI;
    public GameObject bonusBalloon20UI;
    public GameObject bonusBalloon21UI;
    public GameObject bonusBalloon22UI;
    public GameObject bonusBalloon23UI;
    public GameObject bonusBalloon24UI;
    public GameObject bonusBalloon25UI;

    public GameObject bonusBalloon1TextImage;
    public GameObject bonusBalloon2TextImage;
    public GameObject bonusBalloon3TextImage;

    public Text bonusBalloon1CountText;
    public Text bonusBalloon2CountText;
    public Text bonusBalloon3CountText;
    public Text bonusBalloon4CountText;
    public Text bonusBalloon5CountText;
    public Text bonusBalloon6CountText;
    public Text bonusBalloon7CountText;
    public Text bonusBalloon8CountText;
    public Text bonusBalloon9CountText;
    public Text bonusBalloon10CountText;
    public Text bonusBalloon11CountText;
    public Text bonusBalloon12CountText;
    public Text bonusBalloon13CountText;
    public Text bonusBalloon14CountText;
    public Text bonusBalloon15CountText;
    public Text bonusBalloon16CountText;
    public Text bonusBalloon17CountText;
    public Text bonusBalloon18CountText;
    public Text bonusBalloon19CountText;
    public Text bonusBalloon20CountText;
    public Text bonusBalloon21CountText;
    public Text bonusBalloon22CountText;
    public Text bonusBalloon23CountText;
    public Text bonusBalloon24CountText;
    public Text bonusBalloon25CountText;



    public void RefleshCounts()
    {
        if (GameManager.Instance.bonusBalloon1Count == 0)
        {
            bonusBalloon1TextImage.SetActive(false);
            bonusBalloon1UI.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
        }
        else
        {
            bonusBalloon1TextImage.SetActive(true);
            if (GameManager.Instance.bonusBalloon1Count > 99)
            {
                bonusBalloon1CountText.text = "99+";
            }
            else
            {
                bonusBalloon1CountText.text = GameManager.Instance.bonusBalloon1Count.ToString();
            }
            bonusBalloon1UI.GetComponent<Image>().color = Color.white;
        }

        if (GameManager.Instance.bonusBalloon2Count == 0)
        {
            bonusBalloon2TextImage.SetActive(false);
            bonusBalloon2UI.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
        }
        else
        {
            bonusBalloon2TextImage.SetActive(true);
            if (GameManager.Instance.bonusBalloon2Count > 99)
            {
                bonusBalloon2CountText.text = "99+";
            }
            else
            {
                bonusBalloon2CountText.text = GameManager.Instance.bonusBalloon2Count.ToString();
            }
            bonusBalloon2UI.GetComponent<Image>().color = Color.white;
        }

        if (GameManager.Instance.bonusBalloon3Count == 0)
        {
            bonusBalloon3TextImage.SetActive(false);
            bonusBalloon3UI.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
        }
        else
        {
            bonusBalloon3TextImage.SetActive(true);
            if (GameManager.Instance.bonusBalloon3Count > 99)
            {
                bonusBalloon3CountText.text = "99+";
            }
            else
            {
                bonusBalloon3CountText.text = GameManager.Instance.bonusBalloon3Count.ToString();
            }
            bonusBalloon3UI.GetComponent<Image>().color = Color.white;
        }

        if (GameManager.Instance.bonusBalloon4Count == 0)
        {
            bonusBalloon4UI.SetActive(false);
        }
        else
        {
            bonusBalloon4UI.SetActive(true);
            bonusBalloon4CountText.text = GameManager.Instance.bonusBalloon4Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon5Count == 0)
        {
            bonusBalloon5UI.SetActive(false);
        }
        else
        {
            bonusBalloon5UI.SetActive(true);
            bonusBalloon5CountText.text = GameManager.Instance.bonusBalloon5Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon6Count == 0)
        {
            bonusBalloon6UI.SetActive(false);
        }
        else
        {
            bonusBalloon6UI.SetActive(true);
            bonusBalloon6CountText.text = GameManager.Instance.bonusBalloon6Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon7Count == 0)
        {
            bonusBalloon7UI.SetActive(false);
        }
        else
        {
            bonusBalloon7UI.SetActive(true);
            bonusBalloon7CountText.text = GameManager.Instance.bonusBalloon7Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon8Count == 0)
        {
            bonusBalloon8UI.SetActive(false);
        }
        else
        {
            bonusBalloon8UI.SetActive(true);
            bonusBalloon8CountText.text = GameManager.Instance.bonusBalloon8Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon9Count == 0)
        {
            bonusBalloon9UI.SetActive(false);
        }
        else
        {
            bonusBalloon9UI.SetActive(true);
            bonusBalloon9CountText.text = GameManager.Instance.bonusBalloon9Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon10Count == 0)
        {
            bonusBalloon10UI.SetActive(false);
        }
        else
        {
            bonusBalloon10UI.SetActive(true);
            bonusBalloon10CountText.text = GameManager.Instance.bonusBalloon10Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon11Count == 0)
        {
            bonusBalloon11UI.SetActive(false);
        }
        else
        {
            bonusBalloon11UI.SetActive(true);
            bonusBalloon11CountText.text = GameManager.Instance.bonusBalloon11Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon12Count == 0)
        {
            bonusBalloon12UI.SetActive(false);
        }
        else
        {
            bonusBalloon12UI.SetActive(true);
            bonusBalloon12CountText.text = GameManager.Instance.bonusBalloon12Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon13Count == 0)
        {
            bonusBalloon13UI.SetActive(false);
        }
        else
        {
            bonusBalloon13UI.SetActive(true);
            bonusBalloon13CountText.text = GameManager.Instance.bonusBalloon13Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon14Count == 0)
        {
            bonusBalloon14UI.SetActive(false);
        }
        else
        {
            bonusBalloon14UI.SetActive(true);
            bonusBalloon14CountText.text = GameManager.Instance.bonusBalloon14Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon15Count == 0)
        {
            bonusBalloon15UI.SetActive(false);
        }
        else
        {
            bonusBalloon15UI.SetActive(true);
            bonusBalloon15CountText.text = GameManager.Instance.bonusBalloon15Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon16Count == 0)
        {
            bonusBalloon16UI.SetActive(false);
        }
        else
        {
            bonusBalloon16UI.SetActive(true);
            bonusBalloon16CountText.text = GameManager.Instance.bonusBalloon16Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon17Count == 0)
        {
            bonusBalloon17UI.SetActive(false);
        }
        else
        {
            bonusBalloon17UI.SetActive(true);
            bonusBalloon17CountText.text = GameManager.Instance.bonusBalloon17Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon18Count == 0)
        {
            bonusBalloon18UI.SetActive(false);
        }
        else
        {
            bonusBalloon18UI.SetActive(true);
            bonusBalloon18CountText.text = GameManager.Instance.bonusBalloon18Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon19Count == 0)
        {
            bonusBalloon10UI.SetActive(false);
        }
        else
        {
            bonusBalloon19UI.SetActive(true);
            bonusBalloon19CountText.text = GameManager.Instance.bonusBalloon19Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon20Count == 0)
        {
            bonusBalloon20UI.SetActive(false);
        }
        else
        {
            bonusBalloon20UI.SetActive(true);
            bonusBalloon20CountText.text = GameManager.Instance.bonusBalloon20Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon21Count == 0)
        {
            bonusBalloon21UI.SetActive(false);
        }
        else
        {
            bonusBalloon21UI.SetActive(true);
            bonusBalloon21CountText.text = GameManager.Instance.bonusBalloon21Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon22Count == 0)
        {
            bonusBalloon22UI.SetActive(false);
        }
        else
        {
            bonusBalloon22UI.SetActive(true);
            bonusBalloon22CountText.text = GameManager.Instance.bonusBalloon22Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon23Count == 0)
        {
            bonusBalloon23UI.SetActive(false);
        }
        else
        {
            bonusBalloon23UI.SetActive(true);
            bonusBalloon23CountText.text = GameManager.Instance.bonusBalloon23Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon24Count == 0)
        {
            bonusBalloon24UI.SetActive(false);
        }
        else
        {
            bonusBalloon24UI.SetActive(true);
            bonusBalloon24CountText.text = GameManager.Instance.bonusBalloon24Count.ToString();
        }

        if (GameManager.Instance.bonusBalloon25Count == 0)
        {
            bonusBalloon25UI.SetActive(false);
        }
        else
        {
            bonusBalloon25UI.SetActive(true);
            bonusBalloon25CountText.text = GameManager.Instance.bonusBalloon25Count.ToString();
        }
    }
}
