using UnityEngine;
using System; // For System.Action

public class Ammunition : MonoBehaviour
{
    [SerializeField] int maxAmmo = 10; // Maximum ammo capacity in the magazine
    [SerializeField] int maxTotalAmmo = 100; // Maximum reserve ammo capacity
    [SerializeField] int currentTotalAmmo = 100; // Current reserve ammo count
    [SerializeField] int currentAmmo; // Current ammo in the magazine
    [SerializeField] float reloadTime = 2f; // Time to reload
    private bool isReloading = false;

    public bool IsReloading => isReloading;
    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;
    public int MaxTotalAmmo => maxTotalAmmo;
    public int CurrentTotalAmmo => currentTotalAmmo;

    // Events to notify UI or other systems
    public Action<int, int, int> OnAmmoChanged;
    public Action<bool> OnReloadingChanged;

    void Start()
    {
        currentAmmo = maxAmmo; // Initialize magazine to full
        NotifyAmmoChange();
    }

    /// <summary>
    /// Consumes one unit of ammo if available.
    /// </summary>
    /// <returns>True if ammo was consumed, false otherwise.</returns>
    public bool ConsumeAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            NotifyAmmoChange();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Starts the reload process if conditions are met.
    /// </summary>
    public void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo && currentTotalAmmo > 0)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    /// <summary>
    /// Adds ammo to the reserve, respecting the maximum capacity.
    /// </summary>
    /// <param name="amount">Amount of ammo to add.</param>
    public void AddAmmo(int amount)
    {
        currentTotalAmmo = Mathf.Clamp(currentTotalAmmo + amount, 0, maxTotalAmmo);
        NotifyAmmoChange();
    }

    /// <summary>
    /// Checks if the weapon's magazine is full.
    /// </summary>
    /// <returns>True if the magazine is full, false otherwise.</returns>
    public bool IsAmmoFull()
    {
        return currentAmmo == maxAmmo;
    }

    /// <summary>
    /// Handles the reload process over time.
    /// </summary>
    private System.Collections.IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        NotifyReloadingChange(true);

        yield return new WaitForSeconds(reloadTime);

        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, currentTotalAmmo);

        currentAmmo += ammoToReload;
        currentTotalAmmo -= ammoToReload;

        isReloading = false;
        NotifyAmmoChange();
        NotifyReloadingChange(false);
    }

    /// <summary>
    /// Notifies listeners about ammo changes.
    /// </summary>
    private void NotifyAmmoChange()
    {
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo, currentTotalAmmo);
    }

    /// <summary>
    /// Notifies listeners about reloading status.
    /// </summary>
    private void NotifyReloadingChange(bool reloading)
    {
        OnReloadingChanged?.Invoke(reloading);
    }
}
