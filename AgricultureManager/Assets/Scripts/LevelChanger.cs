using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public int co2threshold = 1000;

    public void NextLevel() {
        Scene current = SceneManager.GetActiveScene();

        if (current.name == "RecapScene") {
            CheckForFinish();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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

    public void LoadMainScene() {
        // TODO: Attempt to load the scene asyncly and then trigger the transition
        SceneManager.LoadSceneAsync("Scenes/CoreScene");
    }
}
