using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public Transform player;

    public LayerMask whatIsPlayer;

    public float health;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform shootPosition;
    public float shootSpeed = 32f;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange) AttackPlayer();
    }

    private void AttackPlayer()
    {
        // Ensure the enemy faces the player
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack code here
            GameObject go = Instantiate(projectile, shootPosition.position, Quaternion.identity);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shootSpeed, ForceMode.Impulse);

            // End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public void DestroyEnemy()
    {
        OnDeath();
        Destroy(gameObject);
    }

    private void OnDeath()
    {
        // Notify EnemyManager and destroy this enemy
        EnemyManager manager = FindObjectOfType<EnemyManager>();
        if (manager != null)
        {
            manager.EnemyDefeated(gameObject);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
