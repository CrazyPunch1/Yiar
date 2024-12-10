using UnityEngine;
using TMPro; // Required for TextMeshPro

public class MeleeWeapon : MonoBehaviour
{
    public float attackRange = 1.5f;  // Range of the attack
    public int attackDamage = 10;     // Damage dealt to enemies
    public LayerMask enemyLayers;     // Layers for enemy detection
    public Transform attackPoint;     // Point where we check for enemies

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI reloadText;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // Left mouse button or custom input
        {
            Attack();
        }

         // Initialize UI
        UpdateAmmoUI();
        
    }

    void Attack()
    {
        // Play an attack animation (if you have one)
        // Animator.SetTrigger("Attack");

        // Detect enemies in range of the attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Apply damage to each enemy
        foreach (Collider enemy in hitEnemies)
        {
            HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Optional: Visualize the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "";
        }
    }

    void UpdateReloadUI(bool isReloading)
    {
        if (reloadText != null)
        {
            reloadText.gameObject.SetActive(isReloading);
        }
    }
}
