using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            ToMenu();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Destroy(this.gameObject);
    }

    public void ToInstructions()
    {
        SceneManager.LoadScene("InstructionsMenu", LoadSceneMode.Single);
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("CreditsMenu", LoadSceneMode.Single);
    }

    public void ToGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
