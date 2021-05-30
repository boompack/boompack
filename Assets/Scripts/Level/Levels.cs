using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MessagePack;
using UnityEngine;


[MessagePackObject]
public class Levels
{
    [Key(0)]
    public List<Level> levelsList = new List<Level>();
    [Key(1)]
    public Dictionary<int,int> statePoints = new Dictionary<int,int>();
    
    void PrintJson()
    {
        UnityEngine.Debug.Log(MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(this)));
    }
}
