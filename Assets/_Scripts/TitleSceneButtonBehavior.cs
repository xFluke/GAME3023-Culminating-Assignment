using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneButtonBehavior : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
