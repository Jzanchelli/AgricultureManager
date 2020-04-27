using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFade : MonoBehaviour
{
    [Range(0, 2)]
    public float fadeTime = 0.2f;

    private CanvasGroup canvas;
    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<CanvasGroup>();

        LeanTween.value(gameObject, 
            (val) => canvas.alpha = val, 
            0, 1, fadeTime);
    }
}
