using System.Collections;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveDistance = 2f; // Distancia que recorrer� el NPC a izquierda y derecha
    public float moveSpeed = 2f;   // Velocidad de movimiento
    public float waitTime = 1f;    // Tiempo que espera antes de cambiar de direcci�n

    private Vector3 leftPosition;  // Posici�n a la izquierda
    private Vector3 rightPosition; // Posici�n a la derecha
    private bool movingRight = true; // Determina la direcci�n de movimiento

    void Start()
    {
        // Calcula las posiciones izquierda y derecha bas�ndose en la posici�n inicial
        leftPosition = transform.position - Vector3.right * moveDistance;
        rightPosition = transform.position + Vector3.right * moveDistance;

        // Inicia la rutina de movimiento
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            // Determina el destino basado en la direcci�n actual
            Vector3 targetPosition = movingRight ? rightPosition : leftPosition;

            // Mueve el NPC hacia la posici�n objetivo
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // Espera hasta el siguiente frame
            }

            // Cambia la direcci�n
            movingRight = !movingRight;

            // Espera antes de cambiar de direcci�n
            yield return new WaitForSeconds(waitTime);
        }
    }
}


