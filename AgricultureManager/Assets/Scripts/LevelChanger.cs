using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public int co2threshold = 1000;
    public RectTransform transitionBg;
    [Range(0.2f, 2)]
    public float transitionSpeed = 0.3f;

    void Start() {
        ResetTransitionPos();    
    }

    public void NextLevel() {
        Scene current = SceneManager.GetActiveScene();
        if (current.name == "RecapScene") {
            CheckForFinish();
        }
        else if(current.name == "SelectScene") {
            ShiftBackground(current.buildIndex + 1);
        }
        else if(current.name == "CoreScene") {
            ShiftBackgroundUp(current.buildIndex + 1);
        }
        else {
            // The main game scene is behind this one
            SceneManager.LoadScene(current.buildIndex + 1);
        }
    }

    void CheckForFinish() {
        // Check if fail conditions have been met
        if(DataManager.GetTotalCash() <= 0) {
            Loss();
        }
        else if(DataManager.GetTotalCo2() >= co2threshold) {
            Loss();
        }
        else if(DataManager.currentYear > 10) {
            Win();
        }
        else {
            ShiftBackground(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void Loss() {
        SceneManager.LoadScene("Scenes/LoseScene");
    }

    void Win() {
        // Loads the next scene in the build settings after this one. Make sure this is the winning page
        SceneManager.LoadScene("Scenes/WinScene");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();

        // The above doesn't close the game if it is in the Unity editor
        // This will simulate the above functionality but in the editor
        if (UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        
    }

    public void Reset() {
        DataManager.Reset();
    }

    private void ShiftBackground(int toLoadIndex) {
        Vector3 pos = transitionBg.position;

        LeanTween.value(gameObject,
            (val) => {
                transitionBg.localPosition = new Vector3(val, pos.y, pos.z);
            },
            pos.x, pos.x - 800, transitionSpeed)
            .setOnComplete(() => SceneManager.LoadScene(toLoadIndex));
    }

    private void ShiftBackgroundUp(int toLoadIndex) {
        Vector3 pos = transitionBg.position;

        LeanTween.value(gameObject,
            (val) => {
                transitionBg.localPosition = new Vector3(pos.x, val, pos.z);
            },
            pos.y, pos.y + 500, transitionSpeed)
            .setOnComplete(() => SceneManager.LoadScene(toLoadIndex));
    }

    private void ResetTransitionPos() {
        Scene current = SceneManager.GetActiveScene();
        if(current.name == "SelectScene" || current.name == "RecapScene") {
            transitionBg.position = new Vector3(800, 0, 0);
        }
        else if(current.name == "CoreScene") {
            transitionBg.position = new Vector3(0, -500, 0);
        }
    }
}
