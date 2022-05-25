using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void Next()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
      //  UnityEditor.EditorApplication.isPlaying = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(3);
    }
}
