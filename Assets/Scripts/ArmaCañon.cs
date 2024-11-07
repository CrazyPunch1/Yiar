using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // El proyectil que se instanciará
    [SerializeField] Transform firePoint; // Punto desde donde se dispara el proyectil
    [SerializeField] float projectileSpeed = 20f; // Velocidad del proyectil
    [SerializeField] float fireRate = 0.5f; // Tiempo entre disparos
    [SerializeField] float bulletLife = 1;
    [SerializeField] float rocketJumpForce = 10f; // Fuerza aplicada para el rocket jump
    [SerializeField] LayerMask groundLayer; // Capa para definir qué es suelo
    [SerializeField] Transform groundCheck; // Referencia para el chequeo de suelo
    [SerializeField] float groundCheckRadius = 0.2f; // Radio para el chequeo de suelo
    [SerializeField] int maxAirShots = 2; // Máximo de disparos permitidos en el aire
    [SerializeField] AudioClip shootSound; // Sonido que se reproduce al disparar
    private AudioSource audioSource; // Componente para reproducir sonido

    private float nextFireTime = 0f;
    private Rigidbody playerRb;
    private bool isGrounded;
    private bool wasGrounded; // Para verificar si el jugador acaba de aterrizar
    private int airShotCounter = 0; // Cuenta los disparos realizados en el aire

    void Start()
    {
        // Obtener el componente Rigidbody del jugador
        playerRb = GetComponent<Rigidbody>();

        // Obtener o añadir el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        // Verificar si el jugador está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Restablecer el contador de disparos aéreos al aterrizar
        if (isGrounded)
        {
            airShotCounter = 0;
        }

        // Lógica de disparo
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && CanShoot())
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Crear un nuevo proyectil en la posición y rotación del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Obtener el componente Rigidbody del proyectil y aplicar fuerza
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        // Destruir el proyectil después de un tiempo definido
        Destroy(projectile, bulletLife);

        // Reproducir sonido de disparo
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Si el jugador no está en el suelo, aplicar fuerza para el rocket jump
        if (!isGrounded)
        {
            Vector3 rocketJumpDirection = -firePoint.forward;
            playerRb.AddForce(rocketJumpDirection * rocketJumpForce, ForceMode.Impulse);
            airShotCounter++; // Incrementar el contador de disparos aéreos
        }
    }

    bool CanShoot()
    {
        // Permitir disparar si el jugador está en el suelo o si quedan disparos aéreos
        return isGrounded || airShotCounter < maxAirShots;
    }
}
