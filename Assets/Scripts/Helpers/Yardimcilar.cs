using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Yardimcilar
{
    public static Vector3 Derinlik(int x, int y, int z = 1)
    {
        return new Vector3(x, y, z);
    }

    public static Vector3 Derinlik(Vector3 vector3, int z = -1)
    {
        return new Vector3(vector3.x, vector3.y, z);
    }

    public static int GetLevelState(int levelID)
    {
        return ((levelID - 1) / 20) + 1;
    }
}
