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
    private HoverController hoverController;

    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CoreGameController>();
        stateModel = LookupState();
        stateImageHolder = GameObject.FindGameObjectWithTag("StateImage").GetComponent<Image>();
        hoverController = GameObject.Find("HoverMessage").GetComponent<HoverController>();
    }

    private void OnMouseEnter() {
        hoverController.Show(stateModel);
    }

    private void OnMouseExit() {
        hoverController.Hide();
    }

    private void OnMouseDown() {
        stateImageHolder.sprite = sprite;
        // The color on the state image is completely transparent at the start, so the panel background is visible
        // This needs to change to white in order to see a state's image
        stateImageHolder.color = Color.white;

        gameController.activeState = stateModel;
    }

    private State LookupState() {
        switch (stateName) {
            case "Aqua":
                return DataManager.aquaState;
            case "Blue":
                return DataManager.blueState;
            case "Brown":
                return DataManager.brownState;
            case "Dark Green":
                return DataManager.darkGreenState;
            case "Green":
                return DataManager.greenState;
            case "Light Green":
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
