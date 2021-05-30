using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : Singleton<HighScoreManager>
{
    public int[] highscores;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<300;i++)
        {
            if (!PlayerPrefs.HasKey("LevelHighScore"+i))
            {
                PlayerPrefs.SetInt("LevelHighScore" + i, 0);
            }
            highscores[i] = PlayerPrefs.GetInt("LevelHighScore" + i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
