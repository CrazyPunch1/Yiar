using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f; // Amount of damage dealt per collision
    [SerializeField] private float damageCooldown = 1f; // Cooldown time between damage applications
    private float lastDamageTime = -Mathf.Infinity; // Time when the last damage was applied

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a HealthSystem component
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null && Time.time >= lastDamageTime + damageCooldown)
        {
            // Apply damage
            healthSystem.TakeDamage(damageAmount);

            // Update last damage time
            lastDamageTime = Time.time;

            Debug.Log($"Player hit! Applied {damageAmount} damage.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger object has a HealthSystem component
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();

        if (healthSystem != null && Time.time >= lastDamageTime + damageCooldown)
        {
            // Apply damage
            healthSystem.TakeDamage(damageAmount);

            // Update last damage time
            lastDamageTime = Time.time;

            Debug.Log($"Player hit! Applied {damageAmount} damage.");
        }
    }
}
