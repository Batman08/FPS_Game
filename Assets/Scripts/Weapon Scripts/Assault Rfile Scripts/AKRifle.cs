using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKRifle : MonoBehaviour
{
    private const string WEAPON_NAME = "AK 47";

    public Camera PlayerCamera;

    private AssaultRifle _assaultRifle;

    private void Awake()
    {
        _assaultRifle = new AssaultRifle();
        _assaultRifle.AssaultRifleName = WEAPON_NAME;
        _assaultRifle.AssaultRifleDamage = 5f;
        _assaultRifle.AssaultRifleFireRate = 1.2f;
        _assaultRifle.AssaultRifleRange = 100f;
        _assaultRifle.AssaultRifleAmmunition = 1000;
        _assaultRifle.AsaultRifleClipSize = 50;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _assaultRifle.FireWeapon(PlayerCamera);
        }
    }
}
