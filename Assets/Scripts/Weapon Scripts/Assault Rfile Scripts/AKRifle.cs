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

    private AssaultRifle _assaultRifle;

    private void Awake()
    {
        _assaultRifle = new AssaultRifle();
        _assaultRifle.AssaultRifleName = WEAPON_NAME;
        _assaultRifle.AssaultRifleDamage = 0f;
        _assaultRifle.AssaultRifleFireRate = 13.5f;
        _assaultRifle.AssaultRifleNextTimeToFire = 0f;
        _assaultRifle.AssaultRifleRange = 100f;
        _assaultRifle.AsaultRifleReloadTime = 1f;
        _assaultRifle.AssaultRifleTotalAmmunition = 150;
        _assaultRifle.AsaultRifleDefaultClipSize = 50;
        _assaultRifle.AsaultRifleClipSize = _assaultRifle.AsaultRifleDefaultClipSize;
        _assaultRifle.IsReloading = false;
        _assaultRifle.MuzzleFlash = AKRifleMuzzleFlash;
    }


    private void FixedUpdate()
    {
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
