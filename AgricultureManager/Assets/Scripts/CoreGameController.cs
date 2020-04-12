using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Called when purchased button is clicked
    public void UpdateState() {
        if(_activeState != null) {
            // Any changes made to this state will then affect the states below it
            // int cowsToBuy = (int)cowSlider.value;
            int cowsToBuy = cowSpawner.Count();
            int grainToBuy = grainSpawner.Count();

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
    }

    public void NewStateClicked() {
        stateText.text = $"State: {_activeState.name}";
        co2Text.text = $"Yearly CO2: {_activeState.co2Emissions}";
        moneyText.text = $"Money: ${_activeState.dollars}";
        cowText.text = $"Cow count: {_activeState.numCows}";
        grainText.text = $"Grain count: {_activeState.numGrains}";
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
