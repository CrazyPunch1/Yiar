using UnityEngine;

public class WeaponManager : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
            SelectWeapon();
        }
        // Slot 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;  // Set to index 2 for the third weapon slot
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        // Activate the selected weapon, deactivate others
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == selectedWeapon);
        }
    }

    public GameObject GetCurrentWeapon(){
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].activeInHierarchy){
                return weapons[i];
            }
        }    
    
        return null; 
    }
}
