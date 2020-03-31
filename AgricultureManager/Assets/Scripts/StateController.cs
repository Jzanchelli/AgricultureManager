using System;
using UnityEngine;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    public string stateName;
    public Text stateText;
    public Image image;
    public Sprite sprite;
    private CoreGameController gameController;

    private State stateModel;

    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CoreGameController>();
        stateModel = LookupState();
    }

    private void OnMouseDown() {
        image.sprite = sprite;
        image.color = Color.white;
        gameController.activeState = stateModel;
        gameController.NewStateClicked();
        // If you want to set some other values based on the state selected (such as the current dollar amount),
        // you can access the value from the stateModel (stateModel.dollars)
    }

    private State LookupState() {
        switch (stateName) {
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
                Debug.Log($"Core game controller failed to lookup state: {stateName}");
                throw new Exception("Invalid state name");
        }
    }
}
