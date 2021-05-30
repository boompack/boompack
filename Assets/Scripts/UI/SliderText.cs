using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public Slider slider;
    public Text text;

    public void UpdateTexts()
    {
        text.text = Mathf.FloorToInt(slider.value).ToString();
    }

}
