using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEItemPrefab : MonoBehaviour
{
    public GameObject itemPrefab;

    public void SelectItem()
    {
        LevelEditorUIManager.Instance.selectedPrefab = itemPrefab;
    }

    public void SelectToggleItem(bool selected)
    {
        if(selected)
        {
        LevelEditorUIManager.Instance.selectedPrefab = itemPrefab;
        }
    }

}
