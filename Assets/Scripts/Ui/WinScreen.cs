using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject uiElement; // Elemento de UI de victoria
    [SerializeField] private string targetTag = "Player"; // Tag del objeto que debe activar la UI
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    private void Start()
    {
        // Asegurarse de que el UI esté oculto al inicio
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el Collider tiene el tag correcto
        if (other.CompareTag(targetTag))
        {
            ActivateWinScreen();
            onTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ocultar la UI cuando el objeto sale del Collider
        if (other.CompareTag(targetTag))
        {
            DeactivateWinScreen();
            onTriggerExit?.Invoke();
        }
    }

    // Método para activar la pantalla de victoria
    private void ActivateWinScreen()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true); // Mostrar UI
            Time.timeScale = 0; // Pausar el tiempo
            Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor
            Cursor.visible = true; // Hacer visible el cursor
        }
    }

    // Método para desactivar la pantalla de victoria
    private void DeactivateWinScreen()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(false);
            Time.timeScale = 1; // Restaurar el tiempo
            Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor
            Cursor.visible = false;
        }
    }

    // Este método puede ser llamado al hacer clic en el botón "Siguiente nivel" o "Reiniciar"
    public void LoadNextScene(string sceneName)
    {
        Time.timeScale = 1; // Restaurar el tiempo antes de cargar la siguiente escena
        SceneManager.LoadScene(sceneName);
    }
}
