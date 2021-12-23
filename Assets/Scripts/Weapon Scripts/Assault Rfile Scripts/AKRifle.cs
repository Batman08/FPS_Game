using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AKRifle : MonoBehaviour
{
    private const string WEAPON_NAME = "AK 47";

    public Camera PlayerCamera;
    public ParticleSystem AKRifleMuzzleFlash;
    public TextMeshProUGUI AmmoCountText;
    public bool UnlimitedAmmo = false;
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

    private AssaultRifle _assaultRifle;

    private void Awake()
    {
        _assaultRifle = new AssaultRifle();
        AssaultRifleName = WEAPON_NAME;
        _assaultRifle.AssaultRifleName = AssaultRifleName;
        _assaultRifle.AssaultRifleDamage = AssaultRifleDamage;
        _assaultRifle.AssaultRifleFireRate = AssaultRifleFireRate;
        _assaultRifle.AssaultRifleNextTimeToFire = AssaultRifleNextTimeToFire;
        _assaultRifle.AssaultRifleRange = AssaultRifleRange;
        _assaultRifle.AsaultRifleReloadTime = AsaultRifleReloadTime;
        _assaultRifle.AssaultRifleTotalAmmunition = AssaultRifleTotalAmmunition;
        _assaultRifle.AsaultRifleDefaultClipSize = AsaultRifleDefaultClipSize;
        AsaultRifleClipSize = AsaultRifleDefaultClipSize;
        _assaultRifle.AsaultRifleClipSize = AsaultRifleDefaultClipSize;
        _assaultRifle.IsReloading = false;
        _assaultRifle.MuzzleFlash = AKRifleMuzzleFlash;
    }


    private void FixedUpdate()
    {
        _assaultRifle.UnlimitedAmmo = UnlimitedAmmo;

        AmmoCountText.text = $"Ammo {_assaultRifle.AsaultRifleClipSize }/{_assaultRifle.AssaultRifleTotalAmmunition}";

        if (_assaultRifle.IsReloading)
        {
            return;
        }

        bool hasEnoughAmmoToReload = _assaultRifle.AssaultRifleTotalAmmunition > 0;
        if (hasEnoughAmmoToReload)
        {
            bool pressingReloadButton = Input.GetKey(KeyCode.R);
            bool clipIsEmpty = _assaultRifle.AsaultRifleClipSize <= 0;
            bool tryingToReload = pressingReloadButton || clipIsEmpty;
            if (tryingToReload)
            {
                StartCoroutine(_assaultRifle.ReloadWeapon());
                return;
            }
        }

        bool pressingFireButton = Input.GetMouseButton(0);
        bool timeToFireNextBullet = Time.time >= _assaultRifle.AssaultRifleNextTimeToFire;
        bool enoughAmmoInClipToFire = _assaultRifle.AsaultRifleClipSize > 0;
        bool tryingToFire = pressingFireButton && timeToFireNextBullet && enoughAmmoInClipToFire;
        if (tryingToFire)
        {
            Debug.Log("Firing");
            _assaultRifle.AssaultRifleNextTimeToFire = Time.time + 1 / _assaultRifle.AssaultRifleFireRate;
            _assaultRifle.FireWeapon(PlayerCamera);
        }
    }
}
