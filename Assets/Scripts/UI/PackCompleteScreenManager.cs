using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackCompleteScreenManager : MonoBehaviour
{
    public Text packCompleteText;
    public Text packCompleteButtonText;

    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            packCompleteText.text = $"Pack {(GameManager.Instance.levelLoader.loadedLevel.levelID / 20) } Completed!";
            packCompleteButtonText.text = $"Continue Pack {(GameManager.Instance.levelLoader.loadedLevel.levelID / 20)+1 }";
        }
    }
}
