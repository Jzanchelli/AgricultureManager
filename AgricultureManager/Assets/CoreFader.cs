using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreFader : MonoBehaviour
{
    private CanvasGroup canvas;
    [Range(0.2f, 2)]
    public float fadeTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 1;

        LeanTween.value(gameObject, 
            (val) => canvas.alpha = val, 
            1, 0, 0.5f);
    }
}
