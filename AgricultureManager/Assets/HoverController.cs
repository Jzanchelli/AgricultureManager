using UnityEngine;
using UnityEngine.UI;

public class HoverController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public Vector3 offset;
    public new Camera camera;
    public RectTransform BasisObject;

    //Text boxes to show
    public Text moneyText;
    public Text co2Text;
    public Text cowText;
    public Text grainText;

    public float fadeTime = 0.5f;

    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        Vector3 mousePosition = Input.mousePosition + offset;
        mousePosition.z = BasisObject.position.z;
        transform.position = camera.ScreenToWorldPoint(mousePosition);
    }


    // Change to lerp through
    public void Hide() {
        LeanTween.cancel(gameObject);
        
        LeanTween.value(gameObject, TweenCallback, canvasGroup.alpha, 0f, fadeTime);
    }

    public void Show(State state) {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, TweenCallback, canvasGroup.alpha, 1f, fadeTime);

        moneyText.text = $"Money: ${state.dollars}";
        co2Text.text = $"Co2: {state.co2Emissions}";
        cowText.text = $"Cows: {state.numCows}";
        grainText.text = $"Grains: {state.numGrains}";
    }

    void TweenCallback(float value) {
        canvasGroup.alpha = value;
    }
}
