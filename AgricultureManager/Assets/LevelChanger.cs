using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void NextLevel() {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex + 1);
    }
}
