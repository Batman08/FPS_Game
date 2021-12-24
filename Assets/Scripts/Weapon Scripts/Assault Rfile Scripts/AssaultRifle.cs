using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

[SerializeField]
public class AssaultRifle : MonoBehaviour, IWeapon
{
    public string AssaultRifleName;
    public float AssaultRifleDamage;
    public float AssaultRifleFireRate;
    public float AssaultRifleNextTimeToFire;
    public float AssaultRifleRange;
    public float AsaultRifleReloadTime;
    public int AssaultRifleTotalAmmunition;
    public int AsaultRifleDefaultClipSize;
    public int AsaultRifleClipSize;
    public bool IsReloading;
    public bool UnlimitedAmmo;
    public ParticleSystem MuzzleFlash;

    public void SaveDate()
    {
        // FOR SAVING  Weapon weapon = new Weapon(AssaultRifleName, AssaultRifleDamage, AssaultRifleFireRate, AssaultRifleRange, AssaultRifleAmmunition, AsaultRifleClipSize);
    }

    public void FireWeapon(Camera camera)
    {
        MuzzleFlash.Play();

        //Checks if unlimited ammo is enabled
        if (!UnlimitedAmmo)
        {
            AsaultRifleClipSize--;
        }
        else
        {
            Debug.Log("UNLIMITED AMMO ENABLED");
        }

        RaycastHit hit;

        //creates raycast and checks if it hit an object with Target component
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, AssaultRifleRange))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            //if target still exists make it take damage
            bool targetExists = target != null;
            if (targetExists)
            {
                target.TargetTakeDamage(damage: AssaultRifleDamage);
                Debug.Log("HITTING TARGET");
            }
        }
    }

    public IEnumerator ReloadWeapon()
    {
        IsReloading = true;

        Debug.Log("Reloading weapon.......");

        yield return new WaitForSeconds(AsaultRifleReloadTime);

        int onlyNeed = AsaultRifleDefaultClipSize - AsaultRifleClipSize;

        bool hasEnoughBullets = (AssaultRifleTotalAmmunition - onlyNeed) >= 0;
        bool cantHaveFullClip = (AssaultRifleTotalAmmunition - onlyNeed) <= 0;
        //For times when still enough TotalAmmunition left when clip is re-filled
        if (hasEnoughBullets)
        {
            AssaultRifleTotalAmmunition -= onlyNeed;
            AsaultRifleClipSize += onlyNeed;
        }
        //For times when no TotalAmmunition left when clip is re-filled
        else if (cantHaveFullClip)
        {
            AsaultRifleClipSize += AssaultRifleTotalAmmunition;
            AssaultRifleTotalAmmunition = 0;
        }
        //For times when no ammunition left
        else
        {
            Debug.Log("NO BULLETS LEFT");
        }

        IsReloading = false;
    }
}
