using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    public string stateName;
    public Text stateText;
    public Image image;
    public Sprite sprite;
    private CoreGameController gameController;

    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CoreGameController>();
    }

    private void OnMouseDown() {
        stateText.text = $"State: {stateName}";
        image.sprite = sprite;
        gameController.activeState = stateName;
    }
}
