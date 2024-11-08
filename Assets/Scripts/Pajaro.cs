using UnityEngine;

public class Pajaro : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la bala del enemigo
    public Transform firePoint;      // Punto desde donde disparar
    public Transform player;         // Transform del jugador
    public float launchForce = 10f;  // Fuerza inicial del disparo
    public float fireRate = 2f;      // Frecuencia de disparo
    public int health = 3;           // Vida del enemigo

    private float nextFireTime = 0f;

    void Update()
    {
        // Verifica si es el momento de disparar
        if (Time.time >= nextFireTime)
        {
            FireAtPlayer();
            nextFireTime = Time.time + fireRate;  // Actualiza el tiempo del siguiente disparo
        }
    }

    void FireAtPlayer()
    {
        if (player != null)
        {
            // Calcula la dirección al jugador
            Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Aplica una fuerza en arco hacia el jugador
            Vector3 arcForce = directionToPlayer * launchForce + Vector3.up * (launchForce / 2);
            rb.AddForce(arcForce, ForceMode.Impulse);
        }
    }

    // Método para recibir daño
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            health--;

            // Destruye al enemigo si la vida llega a 0
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            // Destruye la bala del jugador al impactar
            Destroy(other.gameObject);
        }
    }
}
