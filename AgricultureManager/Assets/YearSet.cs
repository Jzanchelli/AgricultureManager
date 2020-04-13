using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YearSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // The year increments on the recap scene every time the button is pushed
        // To get an accurate number, use 2 less, since they didn't survive the previous year
        GetComponent<Text>().text = $"You survived {DataManager.currentYear - 2} years.";
    }
}
