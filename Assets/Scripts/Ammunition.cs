using UnityEngine;
using System; // Para utilizar System.Action


public class Ammunition : MonoBehaviour
{
    [SerializeField] int maxAmmo = 10; // Maximum ammo capacity
    [SerializeField] int maxTotalAmmo = 100; // Maximum Total ammo capacity
    [SerializeField] int currentTotalAmmo = 100; // Maximum ammo capacity
    [SerializeField] int currentAmmo; // Current ammo count
    [SerializeField] float reloadTime = 2f; // Time to reload
    private bool isReloading = false;

    public bool IsReloading => isReloading;
    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;
    public int MaxTotalAmmo => maxTotalAmmo;
    public int CurrentTotalAmmo => currentTotalAmmo;

    public System.Action<int, int, int> OnAmmoChanged;
    public System.Action<bool> OnReloadingChanged;

    void Start()
    {
        currentAmmo = maxAmmo;
        NotifyAmmoChange();
    }

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

    public void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void AddAmmo(int amount)
    {
        // Add ammo without exceeding max ammo
        currentTotalAmmo = Mathf.Clamp(currentTotalAmmo + amount, 0, maxTotalAmmo);
        NotifyAmmoChange();
    }

    private System.Collections.IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        NotifyReloadingChange(true);

        yield return new WaitForSeconds(reloadTime);

        int recharge = currentTotalAmmo > maxAmmo ? maxAmmo : currentTotalAmmo;
        recharge = recharge - currentAmmo;
        currentTotalAmmo -= recharge;
        

        currentAmmo = currentAmmo + recharge;
        isReloading = false;

        NotifyAmmoChange();
        NotifyReloadingChange(false);
    }

    private void NotifyAmmoChange()
    {
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo, currentTotalAmmo);
    }

    private void NotifyReloadingChange(bool reloading)
    {
        OnReloadingChanged?.Invoke(reloading);
    }


}
