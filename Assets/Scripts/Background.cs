using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public SpriteRenderer background;
    public SpriteRenderer tile;
    
    void Start()
    {
        StateColorManager.Instance.background = background;
        StateColorManager.Instance.backgroundTile = tile;       
    }
}
