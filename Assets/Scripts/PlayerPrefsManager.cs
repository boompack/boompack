using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Player Prefs Kontrolü Yapıldı");
        if(!PlayerPrefs.HasKey("isMusicOn"))
        {
            PlayerPrefs.SetInt("isMusicOn", 1);
        }

        if(!PlayerPrefs.HasKey("isSoundOn"))
        {
            PlayerPrefs.SetInt("isSoundOn", 1);
        }

        if(!PlayerPrefs.HasKey("isVibrationOn"))
        {
            PlayerPrefs.SetInt("isVibrationOn", 1);
        }

        if(!PlayerPrefs.HasKey("isShowLivesOn"))
        {
            PlayerPrefs.SetInt("isShowLivesOn", 1);
        }

        Destroy(gameObject);
    }
}
