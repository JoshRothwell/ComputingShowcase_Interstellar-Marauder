using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayButton : MonoBehaviour
{

    //Build number of scene to start when Start button is pressed.
    public int gameStartScene;

    public void StartTutorial()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}
