using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenLockScreenPanel : MonoBehaviour
{


    private void OnEnable()
    {
        UIManager.Instance.rewardedPanelMessage.text = "You can watch video for the new "+AdsManager.Instance.numberOfSectionsToOpen+" levels.";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
