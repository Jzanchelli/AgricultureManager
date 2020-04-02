using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameController : MonoBehaviour
{
    [SerializeField] private Text yearText = null;
    [SerializeField] private Text stateText = null;
    [SerializeField] private Text co2Text = null;
    [SerializeField] private Text moneyText = null;
    [SerializeField] private Text cowText = null;
    [SerializeField] private Text grainText = null;

    // The active state is updated in our "OnClick" event (see StateController), it should not be manually set in the inspector
    // https://stackoverflow.com/questions/5842339/how-to-trigger-event-when-a-variables-value-is-changed
    public State activeState { get { return _activeState; }
        set {
            _activeState = value;
            NewStateClicked();
        }
    }

    [SerializeField] private Slider cowSlider = null;
    [SerializeField] private Slider grainSlider = null;

    [SerializeField] private int cowCost = 10;
    [SerializeField] private int grainCost = 10;

    private State _activeState;

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
        
        if(cowDollars + grainDollars > _activeState.dollars) {
            // Unable to purchase. Alert the user in some way
            Debug.LogWarning("Unable to purchase this amount of grains/cows");
        }
        else {
            // Alert the user also
            // Consider setting the slider values back to 0 after purchase? Just uncomment the lines below
            cowSlider.value = 0;
            grainSlider.value = 0;
            Debug.Log("Purchase successful");
            _activeState.numCows += cowsToBuy;
            _activeState.numGrains += grainToBuy;
            _activeState.dollars -= (cowDollars + grainDollars);
        }

        NewStateClicked();
    }

    public void NewStateClicked() {
        stateText.text = $"State: {_activeState.name}";
        co2Text.text = $"Yearly CO2: {_activeState.co2Emissions}";
        moneyText.text = $"Money: ${_activeState.dollars}";
        cowText.text = $"Cow count: {_activeState.numCows}";
        grainText.text = $"Grain count: {_activeState.numGrains}";
    }
}
