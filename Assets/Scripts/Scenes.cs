using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ToInstructions()
    {
        SceneManager.LoadScene("InstructionsMenu", LoadSceneMode.Single);
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("CreditsMenu", LoadSceneMode.Single);
    }
}
