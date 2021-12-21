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
    public float AssaultRifleRange;
    public int AssaultRifleAmmunition;
    public int AsaultRifleClipSize;

    public void SaveDate()
    {
        // FOR SAVING  Weapon weapon = new Weapon(AssaultRifleName, AssaultRifleDamage, AssaultRifleFireRate, AssaultRifleRange, AssaultRifleAmmunition, AsaultRifleClipSize);
    }

    public void FireWeapon(Camera camera)
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, AssaultRifleRange))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            bool targetExists = target != null;
            if (targetExists)
            {
                target.TakeDamage(damage: AssaultRifleDamage);
            }
        }
    }
}
