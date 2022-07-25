using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private bool _unlimitedAmmo = false;
    [SerializeField] private string _name;
    [SerializeField] private float _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _nextTimeToFire;
    [SerializeField] private float _range;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _totalAmmunition;
    [SerializeField] private int _defaultClipSize;
    [SerializeField] private int _clipSize;
    [SerializeField] private bool _isReloading;

    private WeaponBluePrint _weaponBluePrint;
    private WeaponDetails weaponDetails;

    private void Start()
    {
        weaponDetails = new WeaponDetails();
        weaponDetails.Name = _name;
        weaponDetails.Damage = _damage;
        weaponDetails.FireRate = _fireRate;
        weaponDetails.Range = _range;
        weaponDetails.ClipSize = _clipSize;
        weaponDetails.NextTimeToFire = _nextTimeToFire;
        weaponDetails.ReloadTime = _reloadTime;
        weaponDetails.DefaultClipSize = _defaultClipSize;
        weaponDetails.TotalAmmunition = _totalAmmunition;
        weaponDetails.IsReloading = _isReloading;
        weaponDetails.UnlimitedAmmo = _unlimitedAmmo;
        weaponDetails.MuzzleFlash = _muzzleFlash;

        _weaponBluePrint = new WeaponBluePrint(weaponDetails);
    }

    private void FixedUpdate()
    {
        CheckWeaponState();
    }

    private void CheckWeaponState() {
        _weaponBluePrint.UnlimitedAmmo = _unlimitedAmmo;

        UpdateAmmoUI();

        if (_weaponBluePrint.IsReloading)
        {
            return;
        }

        bool hasEnoughAmmoToReload = _weaponBluePrint.TotalAmmunition > 0;
        if (hasEnoughAmmoToReload)
        {
            //bool pressingReloadButton = Input.GetKey(KeyCode.R);
            //bool clipIsEmpty = _weaponBluePrint.ClipSize <= 0;
            //bool tryingToReload = pressingReloadButton || clipIsEmpty;
            if (TryingToReload())
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
            _weaponBluePrint.FireWeapon(_playerCamera);
        }
    }

    private string UpdateAmmoUI() {
         return _ammoCountText.text = $"Ammo {_weaponBluePrint.ClipSize }/{_weaponBluePrint.TotalAmmunition}";
    }

    private bool TryingToReload()
    {
        bool pressingReloadButton = Input.GetKey(KeyCode.R);
        bool clipIsEmpty = _weaponBluePrint.ClipSize <= 0;
        bool tryingToReload = pressingReloadButton || clipIsEmpty;
        return tryingToReload;
    }

    private void OnDisable()
    {
        if (_weaponBluePrint.IsReloading == true/*TryingToReload()*/)
        {
            StopCoroutine(_weaponBluePrint.ReloadWeapon());
            _weaponBluePrint.IsReloading = false;
        }
    }
}
