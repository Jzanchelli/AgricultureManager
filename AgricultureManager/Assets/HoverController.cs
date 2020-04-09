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
        canvasGroup.alpha = 0f;
    }

    public void Show(State state) {
        canvasGroup.alpha = 1f;

        moneyText.text = $"Money: ${state.dollars}";
        co2Text.text = $"Co2: {state.co2Emissions}";
        cowText.text = $"Cows: {state.numCows}";
        grainText.text = $"Grains: {state.numGrains}";
    }
}
