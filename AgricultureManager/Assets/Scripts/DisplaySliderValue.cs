using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySliderValue : MonoBehaviour
{
    private Text text;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();    
    }
    // The slider is supposed to work like this: https://www.youtube.com/watch?v=b3S5a_ohZZ0
    // I couldn't get it, so I have to do it the hard way
    // This gets called by the slider when it changes value (see it's events)
    public void UpdateTextValue() {
        text.text = slider.value.ToString();
    }
}
