using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        // Desbloquear el cursor y hacerlo visible en el menú
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadNextScene(string nextScene)
    {
        if (!string.IsNullOrEmpty(nextScene) && SceneExists(nextScene))
        {
            SceneManager.LoadScene(nextScene);

            // Bloquear el cursor al cargar la nueva escena
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Debug.LogError("No se puede cargar la escena: nombre de escena no válido o la escena no se agregó a la configuración de compilación.");
        }
        Time.timeScale = 1;
    }
   

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPaused = !EditorApplication.isPaused;
#endif
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
