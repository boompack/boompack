using System;
using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

[MessagePackObject]
public class Level
{
    [Key("levelID")]
    public int levelID;
    [Key("sizeX")]
    public int levelSizeX;
    [Key("sizeY")]
    public int levelSizeY;
    [Key("items")]
    public List<Item> itemsList = new List<Item>();
    [Key("wallsL")]
    public List<Wall> wallsLList;
    [Key("wallsB")]
    public List<Wall> wallsBList;
    [Key("zones")]
    public List<Zone> zonesList;
    [Key("ropes")]
    public List<Rope> ropesList;
    [Key("redMoves")]
    public int redMoves;
    [Key("blueMoves")]
    public int blueMoves;
    [Key("yellowMoves")]
    public int yellowMoves;
    [Key("greenMoves")]
    public int greenMoves;
    [Key("timeLimit")]
    public int timeLimit;
    [Key("maxPoint")]
    public int maxPoint;
}

[MessagePackObject]
public class Item
{
    [Key("Type")]
    public ItemType itemType;
    [Key("X")]
    public int itemX;
    [Key("Y")]
    public int itemY;
}

[MessagePackObject]
public class Wall
{
    [Key("wallX")]
    public int wallX;
    [Key("wallY")]
    public int wallY;
}

[MessagePackObject]
public class Rope
{
    [Key("ropeConnectionList")]
    public List<Connection> ropeConnections = new List<Connection>();

    [IgnoreMember]
    public List<Block> ropedBlocks = new List<Block>();
    [IgnoreMember]
    public List<Balloon> isEffectedFromThisBalloon = new List<Balloon>();

}

[MessagePackObject]
public class Zone
{
    [Key("zoneConnectionList")]
    public List<Connection> zoneConnections = new List<Connection>();
}

[MessagePackObject]
public class Connection
{
    [Key("connX")]
    public int connX;
    [Key("connY")]
    public int connY;
}

public enum ItemType : byte
{
    StandartRedBalloon,
    StandartYellowBalloon,
    StandartGreenBalloon,
    StandartBlueBalloon,
    DontTouchBalloon,
    MustTouchBalloon,
    BonusBalloon1,
    BonusBalloon2,
    BonusBalloon3,
    DoubleBalloon,
    ReflectiveBalloon,
    Null
}


