using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenManager : MonoBehaviour
{
    public Sprite yes;
    public Sprite no;

    public Image MusicButton;
    public Image MusicYesNo;
    public Sprite musicOn;
    public Sprite musicOff;
    public Text musicText;


    public Image SoundButton;
    public Image SoundYesNo;
    public Sprite soundOn;
    public Sprite soundOff;
    public Text soundText;

    public Image VibrationButton;
    public Image VibrationYesNo;
    public Sprite vibrationOn;
    public Sprite vibrationOff;
    public Text vibrationText;

    public Image LivesButton;
    public Image LivesYesNo;
    public Sprite livesOn;
    public Sprite livesOff;
    public Text livesText;
    
    public Color enabledColor;
    public Color disabledColor;
    
    void OnEnable()
    {
        UpdateGUI();
    }

    public void UpdateGUI()
    {
        MusicButtonUpdate();
        SoundButtonUpdate();
        VibrationButtonUpdate();
        ShowLivesButtonUpdate();
    }

    public void MusicButtonUpdate()
    {
        if(PlayerPrefs.GetInt("isMusicOn",1) == 0)
        {
            MusicButton.sprite = musicOff;
            MusicYesNo.sprite = no;
            musicText.color = disabledColor;
        }
        else
        {
            MusicButton.sprite = musicOn;
            MusicYesNo.sprite = yes;
            musicText.color = enabledColor;
        }
    }

    public void SoundButtonUpdate()
    {
        if(PlayerPrefs.GetInt("isSoundOn",1) == 0)
        {
            SoundButton.sprite = soundOff;
            SoundYesNo.sprite = no;
            soundText.color = disabledColor;
        }
        else
        {
            SoundButton.sprite = soundOn;
            SoundYesNo.sprite = yes;
            soundText.color = enabledColor;
        }
    }

    public void VibrationButtonUpdate()
    {
        if(PlayerPrefs.GetInt("isVibrationOn",1) == 0)
        {
            VibrationButton.sprite = vibrationOff;
            VibrationYesNo.sprite = no;
            vibrationText.color = disabledColor;
        }
        else
        {
            VibrationButton.sprite = vibrationOn;
            VibrationYesNo.sprite = yes;
            vibrationText.color = enabledColor;
        }
    }

    public void ShowLivesButtonUpdate()
    {
        if(PlayerPrefs.GetInt("isShowLivesOn",1) == 0)
        {
            LivesButton.sprite = livesOff;
            LivesYesNo.sprite = no;
            livesText.color = disabledColor;
        }
        else
        {
            LivesButton.sprite = livesOn;
            LivesYesNo.sprite = yes;
            livesText.color = enabledColor;
        }
    }

    public void MusicButtonClick()
    {
        if(PlayerPrefs.GetInt("isMusicOn",1) == 0)
        {
            PlayerPrefs.SetInt("isMusicOn",1);
            MusicButton.sprite = musicOn;
            MusicYesNo.sprite = yes;
            musicText.color = enabledColor;
            SoundManager.Instance.ToggleMenuMusic();
        }
        else
        {
            PlayerPrefs.SetInt("isMusicOn",0);
            MusicButton.sprite = musicOff;
            MusicYesNo.sprite = no;
            musicText.color = disabledColor;
            SoundManager.Instance.ToggleMenuMusic();
        }
    }

    public void SoundButtonClick()
    {
        if(PlayerPrefs.GetInt("isSoundOn",1) == 0)
        {
            PlayerPrefs.SetInt("isSoundOn",1);
            SoundButton.sprite = soundOn;
            SoundYesNo.sprite = yes;
            soundText.color = enabledColor;
            SoundManager.Instance.OpenEffects();
        }
        else
        {
            PlayerPrefs.SetInt("isSoundOn",0);
            SoundButton.sprite = soundOff;
            SoundYesNo.sprite = no;
            soundText.color = disabledColor;
            SoundManager.Instance.CloseEffects();
        }
    }

    public void VibrationButtonClick()
    {
        if(PlayerPrefs.GetInt("isVibrationOn",1) == 0)
        {
            PlayerPrefs.SetInt("isVibrationOn",1);
            VibrationButton.sprite = vibrationOn;
            VibrationYesNo.sprite = yes;
            vibrationText.color = enabledColor;
        }
        else
        {
            PlayerPrefs.SetInt("isVibrationOn",0);
            VibrationButton.sprite = vibrationOff;
            VibrationYesNo.sprite = no;
            vibrationText.color = disabledColor;
        }
    }

    public void ShowLivesButtonClick()
    {
        if(PlayerPrefs.GetInt("isShowLivesOn",1) == 0)
        {
            PlayerPrefs.SetInt("isShowLivesOn",1);
            LivesButton.sprite = livesOn;
            LivesYesNo.sprite = yes;
            livesText.color = enabledColor;
        }
        else
        {
            PlayerPrefs.SetInt("isShowLivesOn",0);
            LivesButton.sprite = livesOff;
            LivesYesNo.sprite = no;
            livesText.color = disabledColor;
        }
    }
}
