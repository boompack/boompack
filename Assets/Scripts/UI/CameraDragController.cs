using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragController : MonoBehaviour
{
    LeanDragCamera ld;
    LeanPinchCamera lp;
    LeanMouseWheel lm;


    public Texture2D dragCursor;


    void Start()
    {
        ld = GetComponent<LeanDragCamera>();
        lp = GetComponent<LeanPinchCamera>();
        lm = GetComponent<LeanMouseWheel>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ld.enabled = true;
            lp.enabled = true;
            lm.enabled = true;
            SelectionManager.Instance.SelectionModeChange((int) SelectionMode.Camera);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ld.enabled = false;
            lp.enabled = false;
            lm.enabled = false;
            SelectionManager.Instance.ToOldSelection();
        }

        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LevelEditorUIManager.Instance.PreviousLevelButton();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LevelEditorUIManager.Instance.NextLevelButton();
        }

    }
}
