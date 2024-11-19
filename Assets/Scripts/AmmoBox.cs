using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5; // Amount of ammo the box provides

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has an Ammunition component
        Ammunition playerAmmo = other.GetComponentInChildren<Ammunition>(true);
        print(playerAmmo.name);


        if (playerAmmo != null)
        {
            // Add ammo to the player's inventory
            playerAmmo.AddAmmo(ammoAmount);

            // Destroy the ammo box after use
            Destroy(gameObject);

            Debug.Log($"Player picked up {ammoAmount} ammo.");
        }
    }
}
