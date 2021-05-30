using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip popRedClip;
    public AudioClip popRed2Clip;

    public AudioClip popYellowClip;
    public AudioClip popYellow2Clip;

    public AudioClip popBlueClip;
    public AudioClip popBlue2Clip;

    public AudioClip popGreenClip;
    public AudioClip popGreen2Clip;

    public AudioClip dontTouchSound;

    public AudioClip touchPopSound;

    public AudioClip sayacSound;
    public AudioClip sayac3saniye;



    public AudioClip reflectiveClip;
    public AudioClip levelStart;
    public AudioClip buttonSound;

    public AudioSource hoparlor;
    public AudioSource menuMusic;

    public AudioMixer mixer;

    public float playPopBarrier;

#if(!LEVEL_EDITOR)
    void Start()
    {

        if (PlayerPrefs.GetInt("isSoundOn", 1) == 0)
        {
            CloseEffects();

        }
        else
        {
            OpenEffects();
        }

        if (PlayerPrefs.GetInt("isMusicOn", 1) == 0)
        {
            menuMusic.mute = true;
        }
        else
        {
            menuMusic.mute = false;
        }
    }
#endif

    public IEnumerator BarrierCounter()
    {
        while (playPopBarrier > 0)
        {
            playPopBarrier -= Time.deltaTime;
            yield return 0;
        }
        Debug.Log("Coroutine Bitti");
    }


    public void PlayPop(BalloonColor balloonColor = BalloonColor.Red)
    {
#if (!LEVEL_EDITOR)
        if (GameSettingsManager.Instance.isSoundOn)
        {
            if (playPopBarrier <= 0)
            {

                switch (balloonColor)
                {
                    case BalloonColor.Red:


                        if (Random.Range(0f, 1f) > 0.5f)
                        {
                            hoparlor.PlayOneShot(popRedClip);
                        }
                        else
                        {
                            hoparlor.PlayOneShot(popRed2Clip);
                        }
                        break;


                    case BalloonColor.Yellow:

                        if (Random.Range(0f, 1f) > 0.5f)
                        {
                            hoparlor.PlayOneShot(popYellowClip);
                        }
                        else
                        {
                            hoparlor.PlayOneShot(popYellow2Clip);
                        }
                        break;


                    case BalloonColor.Blue:
                        if (Random.Range(0f, 1f) > 0.5f)
                        {
                            hoparlor.PlayOneShot(popBlueClip);
                        }
                        else
                        {
                            hoparlor.PlayOneShot(popBlue2Clip);
                        }
                        break;

                    case BalloonColor.Green:
                        if (Random.Range(0f, 1f) > 0.5f)
                        {
                            hoparlor.PlayOneShot(popGreenClip);
                        }
                        else
                        {
                            hoparlor.PlayOneShot(popGreen2Clip);
                        }
                        break;

                    default:
                        if (Random.Range(0f, 1f) > 0.5f)
                        {
                            hoparlor.PlayOneShot(popRedClip);
                        }
                        else
                        {
                            hoparlor.PlayOneShot(popRed2Clip);
                        }
                        break;


                }
                playPopBarrier = 0.15f;
                Debug.Log("Ses Çaldı");
                StartCoroutine("BarrierCounter");
            }
        }

        if (GameSettingsManager.Instance.isVibrationOn)
        {
            switch (Random.Range(0, 5))
            {
                case 0:
                    break;
                    MMVibrationManager.Haptic(HapticTypes.Warning, false, true, this);
                case 1:
                    MMVibrationManager.Haptic(HapticTypes.Success, false, true, this);
                    break;
                case 2:
                    MMVibrationManager.Haptic(HapticTypes.SoftImpact, false, true, this);
                    break;
                case 3:
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false, true, this);
                    break;
            }
        }
#endif
    }

    public void PlayDontTouch()
    {
        
        if (GameSettingsManager.Instance.isSoundOn)
        {
            hoparlor.PlayOneShot(dontTouchSound);
        }
    }

    public void PlayReflective()
    {
        if (GameSettingsManager.Instance.isSoundOn)
        {
            hoparlor.PlayOneShot(reflectiveClip);
        }
    }

    public void ToggleMenuMusic()
    {
        menuMusic.mute = !menuMusic.mute;
    }

    public void PlaySayac()
    {
        if (GameSettingsManager.Instance.isSoundOn)
        {
            hoparlor.PlayOneShot(sayacSound);
        }
    }

    public void PlaySayac3sn()
    {
        if (GameSettingsManager.Instance.isSoundOn)
        {
            hoparlor.PlayOneShot(sayac3saniye);
        }
    }

    public void ToggleEffects()
    {

    }

    public void LevelStartSound()
    {
        hoparlor.PlayOneShot(levelStart);
    }

    public void OpenEffects()
    {
#if (!LEVEL_EDITOR)
        mixer.SetFloat("EffectsVolume", 0f);
#endif
    }

    public void CloseEffects()
    {
#if (!LEVEL_EDITOR)
        mixer.SetFloat("EffectsVolume", -80f);
#endif
    }
}
