using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CoreGameController : MonoBehaviour
{
    [Range(1, 3)]
    public float secondsToDismiss = 1.5f;

    [SerializeField] private Text yearText = null;
    [SerializeField] private Text stateText = null;
    [SerializeField] private Text co2Text = null;
    [SerializeField] private Text moneyText = null;
    [SerializeField] private Text cowText = null;
    [SerializeField] private Text grainText = null;
    [SerializeField] private Text notificationText = null;
    [SerializeField] private Text factText = null;

    private String[] factArray = { "Raising animals for food takes up half of all water used in the U.S." ,
        "Cow burps and farts make up 20 percent of U.S. methane-gas emissions.",
        "It takes more than 11 times as much fossil fuel to make one calorie from animal protein as it does to make one calorie from plant protein.",
        "Animal agriculture is a leading source of carbon-dioxide, nitrous-oxide, and methane emissions, the top three greenhouse gasses.",
        "If every American skipped one meal of chicken per week and ate vegan food instead, it would be like taking 500,000 cars off the road.",
        "On August 8, 2019, the IPCC released a summary of the 2019 special report which asserted that a shift towards plant-based diets would help to mitigate and adapt to climate change"
    };
    // The active state is updated in our "OnClick" event (see StateController), it should not be manually set in the inspector
    // https://stackoverflow.com/questions/5842339/how-to-trigger-event-when-a-variables-value-is-changed
    public State activeState { get { return _activeState; }
        set {
            _activeState = value;
            NewStateClicked();
        }
    }

    [SerializeField] private Spawn cowSpawner = null;
    [SerializeField] private Spawn grainSpawner = null;

    [SerializeField] private int cowCost = 10;
    [SerializeField] private int grainCost = 10;
    
    private State _activeState;

    void Start()
    {
        yearText.text = $"Year {DataManager.currentYear}/10";
        factText.text = getRandomText();
    }

    public string getRandomText()
    {
        int randnum = Random.Range(0, factArray.Length);
        return factArray[randnum];
    }
    // Called when purchased button is clicked
    public void UpdateState() {
        if(_activeState != null) {
            // Any changes made to this state will then affect the states below it
            // int cowsToBuy = (int)cowSlider.value;
            int cowsToBuy = cowSpawner.Count();
            int grainToBuy = grainSpawner.Count();

            // Don't show the purchase messages if nothing is going to happen
            if(cowsToBuy == 0 && grainToBuy == 0) {
                return;
            }

            // Check if able to buy this amount
            int cowDollars = cowsToBuy * cowCost;
            int grainDollars = grainToBuy * grainCost;

            if (cowDollars + grainDollars > _activeState.dollars) {
                // Unable to purchase. Alert the user in some way
                Debug.LogWarning("Unable to purchase this amount of grains/cows");
                ShowErrorMessage();
            } else {
                // Alert the user also that the purchase worked
                ShowSuccessMessage();
                cowSpawner.Clear();
                grainSpawner.Clear();
                _activeState.numCows += cowsToBuy;
                _activeState.numGrains += grainToBuy;
                _activeState.dollars -= (cowDollars + grainDollars);
            }

            // Refresh the states text fields
            NewStateClicked();
        }
        else {
            ShowWarningMessage();   
        }
    }

    public void NewStateClicked() {
        stateText.text = $"State: {_activeState.name}";
        co2Text.text = $"Methane: {_activeState.co2Emissions}";
        moneyText.text = $"Money: ${_activeState.dollars}";
        cowText.text = $"Cow count: {_activeState.numCows}";
        grainText.text = $"Grain count: {_activeState.numGrains}";
    }

    public void ShowWarningMessage() {
        StopAllCoroutines();

        notificationText.text = "Select a state first";
        notificationText.color = Color.yellow;
        notificationText.gameObject.SetActive(true);

        StartCoroutine(DismissPurchaseText());
    }

    private IEnumerator DismissPurchaseText() {
        yield return new WaitForSeconds(secondsToDismiss);

        notificationText.gameObject.SetActive(false);
    }

    private void ShowErrorMessage() {
        // Prevents dismiss purchase text from executing if it is doing so
        StopAllCoroutines();

        notificationText.text = "Not enough funds";
        notificationText.color = Color.red;

        notificationText.gameObject.SetActive(true);

        StartCoroutine(DismissPurchaseText());
    }

    private void ShowSuccessMessage() {
        StopAllCoroutines();

        notificationText.text = "Purchase successful";
        notificationText.color = Color.white;

        notificationText.gameObject.SetActive(true);

        StartCoroutine(DismissPurchaseText());
    }
}
