using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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

    private string Name { get; set; }
    private float Damage { get; set; }
    private float FireRate { get; set; }
    private int Ammunition { get; set; }
    private int ClipSize { get; set; }
}
