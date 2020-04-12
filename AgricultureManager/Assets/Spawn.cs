using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    [Range(0, 2)]
    public float xOffest = 0.5f;
    [Range(0, 1)]
    public float animationSpeed = 0.2f;
    
    private Stack<GameObject> items;
    private Quaternion upsideDown;

    private void Start() {
        items = new Stack<GameObject>();
        upsideDown = new Quaternion {
            eulerAngles = new Vector3(0, 0, 180)
        };
    }

    public void Create() {
        if(items.Count < 5) {
            var newObj = Instantiate(item, transform.position + new Vector3(xOffest, 0) * items.Count, upsideDown, transform);
            newObj.transform.localScale = Vector3.zero;
            // 360 since I want the object to rotate clockwise
            LeanTween.rotateZ(newObj, 360, animationSpeed);
            LeanTween.scale(newObj, Vector3.one, animationSpeed);
            items.Push(newObj);
        }
    }

    public void Delete() {
        if (items.Count > 0) {
            var obj = items.Pop();
            LeanTween.rotateZ(obj, 180, animationSpeed);
            LeanTween.scale(obj, Vector3.zero, animationSpeed).setOnComplete(() => Destroy(obj));
        }
    }

    public void Clear() {
        while (items.Count > 0) {
            Delete();
        }
    }

    public int Count() {
        return items.Count;
    }
}
