using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameController : MonoBehaviour
{
    public Text yearText;
    // The active state is updated in our "OnClick" event, it should not be manually set in the inspector
    [HideInInspector]
    public string activeState;

    public Slider cowSlider;
    public Slider grainSlider;

    public int cowCost = 10;
    public int grainCost = 10;

    void Start()
    {
        yearText.text = $"Year {DataManager.currentYear}";
    }

    public void UpdateState() {
        // Any changes made to this state will then affect the states below it
        State toUpdate = LookupState();

        int cowsToBuy = (int)cowSlider.value;
        int grainToBuy = (int)grainSlider.value;

        // Check if able to buy this amount
        int cowDollars = cowsToBuy * cowCost;
        int grainDollars = grainToBuy * grainCost;
        
        if(cowDollars + grainDollars > toUpdate.dollars) {
            // Unable to purchase. Alert the user in some way
            Debug.LogWarning("Unable to purchase this amount of grains/cows");
        }
        else {
            // Alert the user also
            // Consider setting the slider values back to 0 after purchase? Just uncomment the lines below
            // cowSlider.value = 0;
            // grainSlider.value = 0;
            Debug.Log("Purchase successful");
            toUpdate.numCows += cowsToBuy;
            toUpdate.numGrains += grainToBuy;
            toUpdate.dollars -= (cowDollars + grainDollars);
        }

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
