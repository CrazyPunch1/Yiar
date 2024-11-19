using UnityEngine;
using TMPro; // Required for TextMeshPro

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float fireRate = 0.5f;
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1f;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI reloadText;

    private Ammunition ammunition;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        ammunition = GetComponent<Ammunition>();
        if (ammunition == null)
        {
            Debug.LogError("No Ammunition component found!");
            return;
        }

        // Subscribe to ammo and reloading events
        ammunition.OnAmmoChanged += UpdateAmmoUI;
        ammunition.OnReloadingChanged += UpdateReloadUI;

        // Initialize UI
        UpdateAmmoUI(ammunition.CurrentAmmo, ammunition.MaxAmmo, ammunition.CurrentTotalAmmo);
        UpdateReloadUI(false);
    }

    void Update()
    {
        if (ammunition == null || ammunition.IsReloading)
            return;

        // Aim the gun in the direction of the mouse (or joystick)
        AimTowardsMouse();

        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            if (ammunition.ConsumeAmmo())
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("Out of ammo!");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ammunition.Reload();
        }
    }

    void AimTowardsMouse()
    {
        // Get the mouse position in the world
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Calculate direction from the firePoint to the hit point
            Vector3 direction = (hitInfo.point - firePoint.position).normalized;

            // Rotate the firePoint to face the target
            firePoint.forward = direction;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        Destroy(projectile, bulletLife);
    }

    void UpdateAmmoUI(int currentAmmo, int maxAmmo, int totalCurrentAmmo)
    {
        if (ammoText != null)
        {
            ammoText.text = $"Ammo: {currentAmmo} / {maxAmmo} / {totalCurrentAmmo}";
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
