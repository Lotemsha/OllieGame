using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public UIManager uiManager;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseUI.SetActive(isPaused);

        uiManager.SetBlur(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
    }

    public void Resume()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        uiManager.SetBlur(false);
        Time.timeScale = 1;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
