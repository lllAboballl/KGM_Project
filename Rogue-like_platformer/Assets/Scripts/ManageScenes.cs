using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Vector2 exitIndex;

    bool gamePaused = false;

    void Update()
    {
        if (gamePaused) PauseGame();
        else UnPauseGame();

        if (Input.GetButtonDown("Pause"))
            gamePaused = !gamePaused;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Room1");
        UnPauseGame();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        UnPauseGame();
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GameExited");
    }
    public void PauseGame()
    {
        if (pauseMenu == null) return;
        pauseMenu.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        if (pauseMenu == null) return;
        pauseMenu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    }


    //Room stuff

    void LoadRoom()
    {

    }

}
