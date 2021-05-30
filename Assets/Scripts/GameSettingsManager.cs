using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : Singleton<GameSettingsManager>
{

    public Animator menuAnimator;

    public Image MusicButton;
    public Sprite musicOn;
    public Sprite musicOff;

    public bool isMusicOn;


    public Image SoundButton;
    public Sprite soundOn;
    public Sprite soundOff;

    public bool isSoundOn;


    public Image VibrationButton;
    public Sprite vibrationOn;
    public Sprite vibrationOff;

    public bool isVibrationOn;

    public Image ShowLivesButton;
    public Sprite showLivesOn;
    public Sprite showLivesOff;

    public bool isShowLivesOn;

    
    void Start()
    {
        UpdateSettingsUI();
    }

    public void MusicButtonClick()
    {
        if(PlayerPrefs.GetInt("isMusicOn",1) == 0)
        {
            PlayerPrefs.SetInt("isMusicOn",1);
            MusicButton.sprite = musicOn;
            isMusicOn = true;
            SoundManager.Instance.ToggleMenuMusic();
        }
        else
        {
            PlayerPrefs.SetInt("isMusicOn",0);
            MusicButton.sprite = musicOff;
            isMusicOn = false;
            SoundManager.Instance.ToggleMenuMusic();
        }    
    }

    public void SoundButtonClick()
    {
        if(PlayerPrefs.GetInt("isSoundOn",1) == 0)
        {
            PlayerPrefs.SetInt("isSoundOn",1);
            SoundButton.sprite = soundOn;
            isSoundOn = true;
            SoundManager.Instance.OpenEffects();
        }
        else
        {
            PlayerPrefs.SetInt("isSoundOn",0);
            SoundButton.sprite = soundOff;
            isSoundOn = false;
            SoundManager.Instance.CloseEffects();
        }
    }

    public void VibrationButtonClick()
    {
        if(PlayerPrefs.GetInt("isVibrationOn",1) == 0)
        {
            PlayerPrefs.SetInt("isVibrationOn",1);
            VibrationButton.sprite = vibrationOn;
            isVibrationOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("isVibrationOn",0);
            VibrationButton.sprite = vibrationOff;
            isVibrationOn = false;
        }
    }

    public void ShowLivesButtonClick()
    {
        if(PlayerPrefs.GetInt("isShowLivesOn",1) == 0)
        {
            PlayerPrefs.SetInt("isShowLivesOn",1);
            ShowLivesButton.sprite = showLivesOn;
            isShowLivesOn = true;
            UIManager.Instance.ShowHealthTexts();
        }
        else
        {
            PlayerPrefs.SetInt("isShowLivesOn",0);
            ShowLivesButton.sprite = showLivesOff;
            isShowLivesOn = false;
            UIManager.Instance.HideHealthText();
        }
    }

    public void GoPremiumClick()
    {

    }

    public void UpdateSettingsUI()
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
            isMusicOn = false;
        }
        else
        {
            SoundButton.sprite = musicOn;
            isMusicOn = true;
        }
    }

    public void SoundButtonUpdate()
    {
        if(PlayerPrefs.GetInt("isSoundOn",1) == 0)
        {
            SoundButton.sprite = soundOff;
            isSoundOn = false;
        }
        else
        {
            SoundButton.sprite = soundOn;
            isSoundOn = true;
        }
    }

    public void VibrationButtonUpdate()
    {
        if(PlayerPrefs.GetInt("isVibrationOn",1) == 0)
        {
            VibrationButton.sprite = vibrationOff;
            isVibrationOn = false;
        }
        else
        {
            VibrationButton.sprite = vibrationOn;
            isVibrationOn = true;
        }
    }

    public void ShowLivesButtonUpdate()
    {
        if(PlayerPrefs.GetInt("isShowLivesOn",1) == 0)
        {       
            ShowLivesButton.sprite = showLivesOff;
            isShowLivesOn = false;
            UIManager.Instance.HideHealthText();
        }
        else
        {
            ShowLivesButton.sprite = showLivesOn;
            isShowLivesOn = true;
            UIManager.Instance.ShowHealthTexts();
        }
    }

    public void MenuButtonClicked()
    {
        Debug.Log("Menu Basıldı");
        UpdateSettingsUI();
        menuAnimator.SetTrigger("OpenMenu");
        GameManager.Instance.PauseContinueTime();    
    }

    public void TutorialButtonClick()
    {


    }

    public void PremiumButtonClick()
    {

    }
}
