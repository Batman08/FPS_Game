using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetails : MonoBehaviour
{
    public string Name { get; set; }
    public float Damage { get; set; }
    public float FireRate { get; set; }
    public float Range { get; set; }
    public int ClipSize { get; set; }
    public float NextTimeToFire { get; set; }
    public float ReloadTime { get; set; }
    public int DefaultClipSize { get; set; }
    public int TotalAmmunition { get; set; }
    public bool IsReloading { get; set; }
    public bool UnlimitedAmmo { get; set; }
    public ParticleSystem MuzzleFlash { get; set; }
}
