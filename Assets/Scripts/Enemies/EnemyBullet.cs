using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifespan = 3f;  // Duración de la bala antes de destruirse

    void Start()
    {
        // Destruye la bala después de un tiempo para evitar que siga volando indefinidamente
        Destroy(gameObject, lifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Aquí puedes agregar efectos o reducir la vida del jugador si la bala lo golpea
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ejemplo de daño al jugador (suponiendo que el jugador tenga un script de salud)
            // collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);

            Destroy(gameObject);  // Destruye la bala al impactar
        }
    }
}
