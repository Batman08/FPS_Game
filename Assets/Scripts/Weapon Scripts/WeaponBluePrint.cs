using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponBluePrint
{
    void SaveData();

    void FireWeapon(Camera camera);

    IEnumerator ReloadWeapon();
}

[SerializeField]
public class WeaponBluePrint : MonoBehaviour, IWeaponBluePrint
{
    private enum WeaponType
    {
        AssaultRiffles,
        SMGS,
        Shotguns,
        LMGS,
        MarksmanRifles,
        SniperRifles,
        Melee
    };

    public string Name { get; set; }
    public float Damage { get; set; }
    public float FireRate { get; set; }
    public float Range { get; set; }
    public int ClipSize { get; set; }
    //
    public float NextTimeToFire { get; set; }
    public float ReloadTime { get; set; }
    public int DefaultClipSize { get; set; }
    public int TotalAmmunition { get; set; }
    public bool IsReloading { get; set; }
    public bool UnlimitedAmmo { get; set; }
    public ParticleSystem MuzzleFlash { get; set; }

    public WeaponBluePrint(WeaponDetails weaponDetails)
    {
        Name = weaponDetails.Name;
        Damage = weaponDetails.Damage;
        FireRate = weaponDetails.FireRate;
        Range = weaponDetails.Range;
        ClipSize = weaponDetails.ClipSize;
        NextTimeToFire = weaponDetails.NextTimeToFire;
        ReloadTime = weaponDetails.ReloadTime;
        DefaultClipSize = weaponDetails.DefaultClipSize;
        TotalAmmunition = weaponDetails.TotalAmmunition;
        IsReloading = weaponDetails.IsReloading;
        UnlimitedAmmo = weaponDetails.UnlimitedAmmo;
        MuzzleFlash = weaponDetails.MuzzleFlash;
    }

    public void SaveData()
    {
        // FOR SAVING  Weapon weapon = new Weapon(AssaultRifleName, AssaultRifleDamage, AssaultRifleFireRate, AssaultRifleRange, AssaultRifleAmmunition, AsaultRifleClipSize);
    }

    public void FireWeapon(Camera camera)
    {
        MuzzleFlash.Play();

        //Checks if unlimited ammo is enabled
        if (!UnlimitedAmmo)
        {
            ClipSize--;
        }
        else
        {
            Debug.Log("UNLIMITED AMMO ENABLED");
        }

        RaycastHit hit;

        //creates raycast and checks if it hit an object with Target component
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            //if target still exists make it take damage
            bool targetExists = target != null;
            if (targetExists)
            {
                target.TargetTakeDamage(damage: Damage);
                Debug.Log("HITTING TARGET");
            }
        }
    }

    public IEnumerator ReloadWeapon()
    {
        IsReloading = true;

        Debug.Log("Reloading weapon.......");

        yield return new WaitForSeconds(ReloadTime);

        int onlyNeed = DefaultClipSize - ClipSize;

        bool hasEnoughBullets = (TotalAmmunition - onlyNeed) >= 0;
        bool cantHaveFullClip = (TotalAmmunition - onlyNeed) <= 0;
        //For times when still enough TotalAmmunition left when clip is re-filled
        if (hasEnoughBullets)
        {
            TotalAmmunition -= onlyNeed;
            ClipSize += onlyNeed;
        }
        //For times when no TotalAmmunition left when clip is re-filled
        else if (cantHaveFullClip)
        {
            ClipSize += TotalAmmunition;
            TotalAmmunition = 0;
        }
        //For times when no ammunition left
        else
        {
            Debug.Log("NO BULLETS LEFT");
        }

        IsReloading = false;
    }
}

