using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void Exit()
    {

        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
