using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxInfoPanel : MonoBehaviour
{
    public LeanTooltipData lt;
    public Block block;
    private void Awake()
    {
        block = GetComponent<Block>();
        lt = GetComponent<LeanTooltipData>();
    }

    public void OnMouseEnter()
    {
        lt.Text = GetInfos(); 
    }

    public string GetInfos()
    {
        string str =  $"Block ID: {block.placeX}, {block.placeY} \n";

        if(block.onBalloon != null)
        {
            str += $"Balloon Type {block.onBalloon.GetType()} \n";
            str += $"Balloon Health {block.onBalloon.health}";
        }
        return str;
    }
}
