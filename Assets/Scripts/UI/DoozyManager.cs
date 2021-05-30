using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;


public class DoozyManager : Singleton<DoozyManager>
{
    public void SendGameEvent(string gelenEvent)
    {
        #if (!LEVEL_EDITOR)
        GameEventMessage.SendEvent(gelenEvent);
        #endif
    }
}
