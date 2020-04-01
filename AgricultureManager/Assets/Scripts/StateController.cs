using System;
using UnityEngine;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    public string stateName;
    private Image stateImageHolder;
    public Sprite sprite;
    private CoreGameController gameController;

    private State stateModel;

    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CoreGameController>();
        stateModel = LookupState();
        stateImageHolder = GameObject.FindGameObjectWithTag("StateImage").GetComponent<Image>();
    }

    private void OnMouseDown() {
        stateImageHolder.sprite = sprite;
        stateImageHolder.color = Color.white;
        gameController.activeState = stateModel;
        gameController.NewStateClicked();
        // If you want to set some other values based on the state selected (such as the current dollar amount),
        // you can access the value from the stateModel (stateModel.dollars)
    }

    private State LookupState() {
        switch (stateName) {
            case "Aqua":
                return DataManager.aquaState;
            case "Blue":
                return DataManager.blueState;
            case "Brown":
                return DataManager.brownState;
            case "DarkGreen":
                return DataManager.darkGreenState;
            case "Green":
                return DataManager.greenState;
            case "LightGreen":
                return DataManager.lightGreenState;
            case "Orange":
                return DataManager.orangeState;
            case "Yellow":
                return DataManager.yellowState;
            default:
                Debug.Log($"Core game controller failed to lookup state: {stateName}");
                throw new Exception("Invalid state name");
        }
    }
}
