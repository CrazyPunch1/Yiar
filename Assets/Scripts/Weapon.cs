using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Properties")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float fireRate = 0.5f;
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1f;

    [Header("Ammunition")]
    [SerializeField] int maxAmmo = 30;
    [SerializeField] int currentAmmo;
    [SerializeField] int totalAmmo = 100; // Total available ammo in inventory
    [SerializeField] TextMeshProUGUI ammoText;

    private bool isReloading = false;
    private bool isWeaponEnabled = false;  // Track if the weapon is enabled and should shoot

    void Start()
    {
        currentAmmo = maxAmmo; // Set current ammo to max at the start
        UpdateAmmoUI();
    }

    void Update()
    {
        if (!isWeaponEnabled || isReloading)  // If weapon is disabled, don't shoot
            return;

        // Handle fire input
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("Out of ammo!");
            }
        }

        // Handle reload input
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Apply velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        // Decrease ammo count
        currentAmmo--;
        UpdateAmmoUI();

        Destroy(projectile, bulletLife);
    }

    void Reload()
    {
        if (currentAmmo == maxAmmo || totalAmmo == 0)
            return;

        isReloading = true;
        Debug.Log("Reloading...");

        // Reload ammo after a short delay
        Invoke(nameof(FinishReload), 2f);
    }

    void FinishReload()
    {
        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo);

        currentAmmo += ammoToReload;
        totalAmmo -= ammoToReload;

        isReloading = false;
        UpdateAmmoUI();
        Debug.Log("Reload Complete!");
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Ammo: {currentAmmo} / {maxAmmo} / {totalAmmo}";
        }
    }

    public void EnableWeapon(bool enable)
    {
        isWeaponEnabled = enable;
    }
}
