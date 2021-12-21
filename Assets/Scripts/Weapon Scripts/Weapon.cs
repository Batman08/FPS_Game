using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
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
    private float Range { get; set; }
    private int Ammunition { get; set; }
    private int ClipSize { get; set; }

    public Weapon(string name, float damage, float fireRate, float range, int ammunition, int clipSize)
    {
        Name = name;
        Damage = damage;
        FireRate = fireRate;
        Range = range;
        Ammunition = ammunition;
        ClipSize = clipSize;
    }

    public interface IWeapon
    {
        void SaveDate();

        void FireWeapon(Camera camera);
    }
}
