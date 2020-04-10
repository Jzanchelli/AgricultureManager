using UnityEngine;
using UnityEngine.Events;

public class CharacterSelect : MonoBehaviour
{
    public new string name;
    public Vector3 offset = new Vector3(0, 0.2f, 0);
    public Color darkenColor;

    private Transform imageTransform;
    private bool mouseInside = false;

    public UnityEvent charSelectedEvent;

    void Start() {
        // The first entry is always the parent, everything else is a child
        imageTransform = GetComponentsInChildren<Transform>()[1];
        if(!imageTransform) {
            Debug.LogError("The character select script requires a child object to contain the image sprite.");
        }
    }

    // Move up slightly? Possibly yellow border
    void OnMouseEnter() {
        mouseInside = true;
        imageTransform.position += offset;
    }

    void OnMouseExit() {
        mouseInside = false;
        imageTransform.position -= offset;
        imageTransform.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Slightly depress
    void OnMouseDown() {
        imageTransform.GetComponent<SpriteRenderer>().color = darkenColor;       
    }

    void OnMouseUp() {
        // Added the extra boolean since it was still registering
        // mouse up when the mouse wasn't inside the collider anymore
        if(mouseInside) {
            DataManager.playerCharacter = name;

            charSelectedEvent?.Invoke();
        }
    }
}
