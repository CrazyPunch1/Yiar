using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 30;         // Amount of ammo restored
    [SerializeField] private AudioClip pickupSound;       // Optional sound on pickup
    [SerializeField] private GameObject pickupEffect;     // Optional visual effect
    [SerializeField] private float destroyDelay = 0.5f;   // Delay before destroying the box

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("AmmoBox triggered by Player");

            // Retrieve the WeaponManager component from the player
            WeaponManager weaponManager = other.GetComponent<WeaponManager>();

            if (weaponManager != null)
            {
                // Get the current weapon
                GameObject currentWeapon = weaponManager.GetCurrentWeapon();
                if (currentWeapon != null)
                {
                    // Retrieve the Ammunition component from the current weapon
                    Ammunition ammunition = currentWeapon.GetComponent<Ammunition>();
                    if (ammunition != null)
                    {
                        // Check if ammo can be added
                        if (ammunition.CurrentTotalAmmo < ammunition.MaxTotalAmmo)
                        {
                            Debug.Log($"Adding {ammoAmount} ammo to the current weapon.");

                            // Add ammo to the weapon
                            ammunition.AddAmmo(ammoAmount);

                            // Play a pickup sound if assigned
                            if (pickupSound != null)
                                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                            // Instantiate a visual effect if assigned
                            if (pickupEffect != null)
                                Instantiate(pickupEffect, transform.position, Quaternion.identity);

                            // Destroy the ammo box after a short delay
                            Destroy(gameObject, destroyDelay);
                        }
                        else
                        {
                            Debug.Log("Ammo is already full.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No Ammunition component found on the current weapon.");
                    }
                }
                else
                {
                    Debug.LogWarning("No weapon equipped in the WeaponManager.");
                }
            }
            else
            {
                Debug.LogWarning("No WeaponManager component found on the player.");
            }
        }
    }
}
