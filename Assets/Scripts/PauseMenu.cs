using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Checks inputs (can pause with escape)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pauses game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Resumes Game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Changes scene to main menu
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        isPaused = false;
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }

}
