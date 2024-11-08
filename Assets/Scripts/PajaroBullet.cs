using UnityEngine;

public class PajaroBullet : MonoBehaviour
{
    public float lifespan = 5f;           // Tiempo antes de destruir la bala
    public int damage = 1;               // Cantidad de daño que inflige al jugador
    public AudioClip impactSound;        // Sonido que se reproduce al impactar
    public float pitchMin = 0.8f;        // Pitch mínimo del sonido
    public float pitchMax = 1.2f;        // Pitch máximo del sonido

    private AudioSource audioSource;     // Componente de AudioSource para reproducir sonido

    void Start()
    {
        // Destruye la bala después de un tiempo para evitar que se quede indefinidamente
        Destroy(gameObject, lifespan);

        // Si el objeto no tiene un AudioSource, lo agregamos dinámicamente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Si toca el suelo, destrúyela
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Si toca al jugador, aplica daño y destrúyela
            //collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Reproduce un sonido al impactar
            if (audioSource != null && impactSound != null)
            {
                audioSource.pitch = Random.Range(pitchMin, pitchMax); // Cambia el pitch aleatoriamente
                audioSource.PlayOneShot(impactSound);  // Reproduce el sonido
            }

            // Destruye la bala después de impactar
            Destroy(gameObject);
        }
    }
}
