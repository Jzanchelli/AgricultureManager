using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [Range(0, 2)]
    public float fadeTime = 0.5f;
    public bool increaseAlpha = true;

    private CanvasGroup canvas;
    void Start() {
        float startValue = increaseAlpha ? 0 : 1;
        float endValue = increaseAlpha ? 1 : 0;
        canvas = GetComponent<CanvasGroup>();

        canvas.alpha = startValue;

        LeanTween.value(gameObject,
            (val) => canvas.alpha = val,
            startValue, endValue, fadeTime);
    }
}
