using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    private const string WEAPON_NAME = "AK 47";

    public Camera PlayerCamera;
    public ParticleSystem MuzzleFlash;
    public TextMeshProUGUI AmmoCountText;
    public bool UnlimitedAmmo = false;
    public string Name;
    public float Damage;
    public float FireRate;
    public float NextTimeToFire;
    public float Range;
    public float ReloadTime;
    public int TotalAmmunition;
    public int DefaultClipSize;
    public int ClipSize;
    public bool IsReloading;

    private WeaponBluePrint _weaponBluePrint;

    void Start()
    {
        var weaponDetails = new WeaponDetails();
        weaponDetails.Name = Name;
        weaponDetails.Damage = Damage;
        weaponDetails.FireRate= FireRate;
        weaponDetails.Range = Range;
        weaponDetails.ClipSize = ClipSize;
        weaponDetails.NextTimeToFire = NextTimeToFire;
        weaponDetails.ReloadTime = ReloadTime;
        weaponDetails.DefaultClipSize = DefaultClipSize;
        weaponDetails.TotalAmmunition = TotalAmmunition;
        weaponDetails.IsReloading = IsReloading;
        weaponDetails.UnlimitedAmmo = UnlimitedAmmo;
        weaponDetails.MuzzleFlash = MuzzleFlash;

        _weaponBluePrint = new WeaponBluePrint(weaponDetails);
    }

    void FixedUpdate()
    {
        _weaponBluePrint.UnlimitedAmmo = UnlimitedAmmo;

        AmmoCountText.text = $"Ammo {_weaponBluePrint.ClipSize }/{_weaponBluePrint.TotalAmmunition}";

        if (_weaponBluePrint.IsReloading)
        {
            return;
        }

        bool hasEnoughAmmoToReload = _weaponBluePrint.TotalAmmunition > 0;
        if (hasEnoughAmmoToReload)
        {
            bool pressingReloadButton = Input.GetKey(KeyCode.R);
            bool clipIsEmpty = _weaponBluePrint.ClipSize <= 0;
            bool tryingToReload = pressingReloadButton || clipIsEmpty;
            if (tryingToReload)
            {
                StartCoroutine(_weaponBluePrint.ReloadWeapon());
                return;
            }
        }

        bool pressingFireButton = Input.GetMouseButton(0);
        bool timeToFireNextBullet = Time.time >= _weaponBluePrint.NextTimeToFire;
        bool enoughAmmoInClipToFire = _weaponBluePrint.ClipSize > 0;
        bool tryingToFire = pressingFireButton && timeToFireNextBullet && enoughAmmoInClipToFire;
        if (tryingToFire)
        {
            Debug.Log("Firing");
            _weaponBluePrint.NextTimeToFire = Time.time + 1 / _weaponBluePrint.FireRate;
            _weaponBluePrint.FireWeapon(PlayerCamera);
        }
    }
}
