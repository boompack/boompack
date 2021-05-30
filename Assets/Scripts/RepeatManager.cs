using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatManager : Singleton<RepeatManager>
{
    // Start is called before the first frame update
    public int repeatCount;


    public void ClearRepeatCount()
    {
        repeatCount = 0;
    }

    public void AddRepeat()
    {
        repeatCount ++;
    }
}
