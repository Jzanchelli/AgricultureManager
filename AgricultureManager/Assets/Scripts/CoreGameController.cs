﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameController : MonoBehaviour
{
    public Text yearText;
    public Text stateText;
    public Text co2Text;
    public Text moneyText;
    public Text cowText;
    public Text grainText;

    // The active state is updated in our "OnClick" event (see StateController), it should not be manually set in the inspector
    public State activeState;

    public Slider cowSlider;
    public Slider grainSlider;

    public int cowCost = 10;
    public int grainCost = 10;

    void Start()
    {
        yearText.text = $"Year {DataManager.currentYear}";
    }

    // Called when purchased button is clicked
    public void UpdateState() {
        // Any changes made to this state will then affect the states below it
        int cowsToBuy = (int)cowSlider.value;
        int grainToBuy = (int)grainSlider.value;

        // Check if able to buy this amount
        int cowDollars = cowsToBuy * cowCost;
        int grainDollars = grainToBuy * grainCost;
        
        if(cowDollars + grainDollars > activeState.dollars) {
            // Unable to purchase. Alert the user in some way
            Debug.LogWarning("Unable to purchase this amount of grains/cows");
        }
        else {
            // Alert the user also
            // Consider setting the slider values back to 0 after purchase? Just uncomment the lines below
            cowSlider.value = 0;
            grainSlider.value = 0;
            Debug.Log("Purchase successful");
            activeState.numCows += cowsToBuy;
            activeState.numGrains += grainToBuy;
            activeState.dollars -= (cowDollars + grainDollars);
        }

        NewStateClicked();
    }

    public void NewStateClicked() {
        stateText.text = $"State: {activeState.name}";
        co2Text.text = $"Yearly CO2: {activeState.co2Emissions}";
        moneyText.text = $"Money: ${activeState.dollars}";
        cowText.text = $"Cow count: {activeState.numCows}";
        grainText.text = $"Grain count: {activeState.numGrains}";
    }
}
