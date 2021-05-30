using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Order
{
    public void Revert();
    public void Again();

}

public class BalloonChange : Order
{
    public ItemType newBalloon;
    public ItemType oldBalloon;

    public int placeX;
    public int placeY;

    public BalloonChange(ItemType newBalloon, ItemType oldBalloon, int placeX, int placeY)
    {
        this.newBalloon = newBalloon;
        this.oldBalloon = oldBalloon;
        this.placeX = placeX;
        this.placeY = placeY;
    }


    public void Again()
    {
        throw new System.NotImplementedException();
    }

    public void Revert()
    {
        ((LEBlock)LELevelLoader.Instance.blocksArray[placeX, placeY]).AddBalloon(oldBalloon);
    }
}

public class RopeChange : Order
{
    public int placeX;
    public int placeY;

    public int oldRopeNumber;
    public int newRopeNumber;

    public RopeChange(int oldRopeNumber, int newRopeNumber, int placeX, int placeY)
    {
        this.oldRopeNumber = oldRopeNumber;
        this.newRopeNumber = newRopeNumber;
        this.placeX = placeX;
        this.placeY = placeY;
    }


    public void Again()
    {
        throw new System.NotImplementedException();
    }

    public void Revert()
    {
        ((LEBlock)LELevelLoader.Instance.blocksArray[placeX, placeY]).SetupRopeID(oldRopeNumber);
    }
}

public class ZoneChange : Order
{
    public int placeX;
    public int placeY;

    public int oldZoneNumber;
    public int newZoneNumber;

    public ZoneChange(int oldZoneNumber, int newZoneNumber, int placeX, int placeY)
    {
        this.oldZoneNumber = oldZoneNumber;
        this.newZoneNumber = newZoneNumber;
        this.placeX = placeX;
        this.placeY = placeY;
    }


    public void Again()
    {
        throw new System.NotImplementedException();
    }

    public void Revert()
    {
        ((LEBlock)LELevelLoader.Instance.blocksArray[placeX, placeY]).SetupBorderID(oldZoneNumber);
    }
}

public class WallChange : Order
{
    public WallPlace wall;

    public WallChange(WallPlace wall)
    {
        this.wall = wall;
    }


    public void Again()
    {
        throw new System.NotImplementedException();
    }

    public void Revert()
    {
        if(wall.hasWall)
        {
            wall.DeleteWall();
        }
        else
        {
            wall.AddWall();
        }
    }
}
