using UnityEngine;
using UnityEngine.UI; // Si usas un componente de UI, como Text o Panel.

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox; // Caja de diálogo que aparecerá
    [SerializeField] private string targetTag = "Player"; // Etiqueta del jugador para detectar colisión
    [SerializeField] private float detectionRadius = 3f; // Radio de detección alrededor del objeto
    private Transform playerTransform; // Transform del jugador para medir la distancia

    void Start()
    {
        // Asegúrate de que la caja de diálogo esté oculta al inicio
        dialogueBox.SetActive(false);

        // Encuentra al jugador por su etiqueta (tag)
        GameObject player = GameObject.FindGameObjectWithTag(targetTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player' en la escena.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calcula la distancia entre el jugador y este GameObject
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Muestra la caja de diálogo si el jugador está dentro del radio de detección
            if (distance <= detectionRadius)
            {
                ShowDialogue();
            }
            else
            {
                HideDialogue();
            }
        }
    }

    void ShowDialogue()
    {
        // Activar la caja de diálogo
        dialogueBox.SetActive(true);
    }

    void HideDialogue()
    {
        // Desactivar la caja de diálogo
        dialogueBox.SetActive(false);
    }

    // Opcional: Método para cambiar el radio de detección dinámicamente
    public void SetDetectionRadius(float newRadius)
    {
        detectionRadius = newRadius;
    }
}
