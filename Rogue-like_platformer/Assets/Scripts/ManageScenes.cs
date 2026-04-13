using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    bool gamePaused = false;

    void Update()
    {
        if (pauseMenu == null) return;
        
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
       
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
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
        pauseMenu.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    }

}
