﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueScript : MonoBehaviour
{
    [SerializeField]
    public Text stateText = null;
    public Image image = null;
    public Sprite sprite = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        stateText.text = "State: Blue";
        image.sprite = sprite;
    }
}
