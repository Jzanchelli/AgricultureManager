using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameController : MonoBehaviour
{
    public Text yearText;
    public string activeState;    

    void Start()
    {
        yearText.text = $"Year {DataManager.currentYear}";
    }

    public void UpdateState() {
        // Any changes made to this state will then affect the states below it
        State toUpdate = LookupState();

    }

    private State LookupState() {
        switch(activeState) {
            case "Blue":
                return DataManager.blueState;
            case "LightBlue":
                return DataManager.lightBlueState;
            case "Green":
                return DataManager.greenState;
            case "Red":
                return DataManager.redState;
            case "Yellow":
                return DataManager.yellowState;
            case "White":
                return DataManager.whiteState;
            case "DarkGreen":
                return DataManager.darkGreenState;
            case "Grey":
                return DataManager.greyState;
            default:
                Debug.Log($"Core game controller failed to lookup state: {activeState}");
                throw new Exception("Invalid state name");
        }
    }
}
