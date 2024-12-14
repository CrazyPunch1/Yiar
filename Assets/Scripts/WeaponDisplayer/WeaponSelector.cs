using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    [Header("Weapon Canvases")]
    public GameObject weaponCanvas1; // Canvas del arma 1
    public GameObject weaponCanvas2; // Canvas del arma 2
    public GameObject weaponCanvas3; // Canvas del arma 3

    [Header("Settings")]
    public int defaultWeapon = 1; // Arma predeterminada activa al inicio (1, 2 o 3)

    private int currentWeapon; // Almacena qu� arma est� activa actualmente
    private ImageToggleOnClick[] toggleScripts; // Referencias a los scripts "ImageToggleOnClick"

    void Start()
    {
        // Buscar y almacenar los scripts "ImageToggleOnClick" en los Canvases
        toggleScripts = new ImageToggleOnClick[3];
        toggleScripts[0] = weaponCanvas1?.GetComponent<ImageToggleOnClick>();
        toggleScripts[1] = weaponCanvas2?.GetComponent<ImageToggleOnClick>();
        toggleScripts[2] = weaponCanvas3?.GetComponent<ImageToggleOnClick>();

        // Configurar el arma inicial seg�n la configuraci�n predeterminada
        ActivateWeapon(defaultWeapon);
    }

    void Update()
    {
        // Detectar teclas num�ricas para cambiar armas
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateWeapon(3);
        }
    }

    /// <summary>
    /// Activa el canvas del arma especificada y desactiva las dem�s.
    /// </summary>
    /// <param name="weaponIndex">El �ndice del arma a activar (1, 2 o 3).</param>
    private void ActivateWeapon(int weaponIndex)
    {
        // Evitar cambios si la misma arma ya est� activa
        if (weaponIndex == currentWeapon) return;

        // Desactivar todos los Canvases primero, excepto si el script "ImageToggleOnClick" est� en proceso
        for (int i = 0; i < toggleScripts.Length; i++)
        {
            if (toggleScripts[i] != null && toggleScripts[i].IsToggling)
            {
                // Si un Canvas est� en proceso de alternar im�genes, no lo desactivamos
                continue;
            }

            // Desactivar Canvas si est� asignado
            if (i == 0 && weaponCanvas1 != null) weaponCanvas1.SetActive(false);
            if (i == 1 && weaponCanvas2 != null) weaponCanvas2.SetActive(false);
            if (i == 2 && weaponCanvas3 != null) weaponCanvas3.SetActive(false);
        }

        // Activar el Canvas correspondiente seg�n el �ndice
        switch (weaponIndex)
        {
            case 1:
                if (weaponCanvas1 != null) weaponCanvas1.SetActive(true);
                break;
            case 2:
                if (weaponCanvas2 != null) weaponCanvas2.SetActive(true);
                break;
            case 3:
                if (weaponCanvas3 != null) weaponCanvas3.SetActive(true);
                break;
        }

        // Actualizar el arma actual
        currentWeapon = weaponIndex;
    }
}
