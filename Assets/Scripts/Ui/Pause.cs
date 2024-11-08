using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    bool currentPauseState = false;
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!currentPauseState)
            {
                PauseGameTime();
            }
            else
            {
                UnPauseGameTime();
            }
        }
    }

    public void PauseGameTime()
    {
        currentPauseState = !currentPauseState;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        // Mostrar y desbloquear el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnPauseGameTime()
    {
        currentPauseState = !currentPauseState;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

        // Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnpauseAndRestart()
    {
        UnpauseAndLoad(SceneManager.GetActiveScene().name);
    }

    public void UnpauseAndLoad(string nextScene)
    {
        currentPauseState = !currentPauseState;
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }

    public void MenuPrincipal()
    {
        currentPauseState = !currentPauseState;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
