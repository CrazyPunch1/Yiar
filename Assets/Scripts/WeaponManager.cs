using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Management")]
    public GameObject[] weapons;  // Array to hold all weapon GameObjects
    private int selectedWeapon = 0;  // Index of the currently selected weapon

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        // Slot 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            SelectWeapon();
        }
        // Slot 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length > 1)
        {
            selectedWeapon = 1;
            SelectWeapon();
        }
        // Slot 3
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length > 2)
        {
            selectedWeapon = 2;  // Set to index 2 for the third weapon slot
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        // Deactivate all weapons, then activate the selected one
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == selectedWeapon);
        }

        // Enable/disable the shooting functionality of the selected weapon
        if (weapons[selectedWeapon].TryGetComponent(out Weapon weaponScript))
        {
            weaponScript.EnableWeapon(true);  // Enable the weapon script for the selected weapon
        }

        // Disable shooting for all other weapons
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i != selectedWeapon && weapons[i].TryGetComponent(out Weapon otherWeaponScript))
            {
                otherWeaponScript.EnableWeapon(false);  // Disable shooting for non-selected weapons
            }
        }
    }
}
